using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationManeger : Singleton<AnimationManeger>
{
    private int audioClipCount;
    string[] eventName;
    int[] eventLine, arrayNum;
    int actionCount, cgsCount;
    [SerializeField] private GameObject[] charactor;
    private Animator[] animator = new Animator[20];
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private Image stillView, changeStillView;
    [SerializeField] private Sprite[] stillPictures;
    [SerializeField] private TextBoxController textBoxController;
    private string[] splits = new string[5];//splitしたときの代入用配列
    private string[] nextOrder;
    private int nextOrderNum;
    private int[] layerNumList = new int[]{
        2,4,6
    };
    [SerializeField]
    BackImageManeger backImageManeger;
    [SerializeField]
    string nextSceneName;
    bool stillAnimSkip;


    // Use this for initialization
    void Awake()
    {
        stillAnimSkip = false;
        nextOrderNum = 0;
        for (int i = 0; i < charactor.Length; i++)
        {
            animator[i] = charactor[i].GetComponent<Animator>();
        }
        actionCount = 0;
        audioClipCount = 0;
        cgsCount = 0;
    }

    void Start()
    {
        stillView.gameObject.SetActive(false);
        changeStillView.color = new Color(1, 1, 1, 0);
    }
    public void ExecuteAnimation(int textLineNum)
    {
        for (int i = 0; i < charactor.Length; i++)//再生中のほかのキャラクターのAnimationをスキップ
        {
            charactor[i].GetComponent<Charactor>().SkipAnim();
        }
        for (int i = actionCount; i < eventLine.Length; i++)
        {
            if (eventLine[i] == textLineNum)
            {
                if (eventName[actionCount] == "")
                {
                    actionCount++;
                    continue;
                }
                else if (eventName[actionCount] == "InvokeAnim")
                {//時間指定でAnimation命令

                    StartCoroutine(InvokeExecuteAnimation(arrayNum[actionCount], textLineNum));
                    TextController.TextControl = false;
                    eventName[actionCount] = "";
                    break;
                }
                else if (eventName[actionCount] == "Sound")
                {//音声再生
                    audioSource.clip = audioClip[arrayNum[actionCount]];
                    audioSource.Play();
                }
                else if (eventName[actionCount] == "CgView")
                {//CG表示
                    stillView.sprite = stillPictures[arrayNum[actionCount]];
                    Debug.Log(stillView.sprite.name.Length - 1);
                    string removeString = stillView.sprite.name.Remove(stillView.sprite.name.Length - 1, 1);//差分の枚数が9枚までしか作れないクソ仕様
                    if (!PlayerPrefs.HasKey(removeString))
                    {
                        PlayerPrefs.SetInt(removeString, 1);
                    }
                    StartCoroutine("FadeIn");
                }
                else if (eventName[actionCount] == "CgDel")
                {//CG非表示
                    StartCoroutine("FadeOut");
                }
                else if (eventName[actionCount] == "CgMove")
                {
                    StartCoroutine(StillAnimFade(arrayNum[actionCount].ToString()));
                }
                else if (eventName[actionCount] == "CgChange")
                {
                    string removeString = stillView.sprite.name.Remove(stillView.sprite.name.Length - 1, 1);
                    Debug.Log(removeString);
                    PlayerPrefs.SetInt(removeString, PlayerPrefs.GetInt(removeString, PlayerPrefs.GetInt(removeString, 0)) + 1);
                    StartCoroutine(StillChange(arrayNum[actionCount]));
                }
                else if (eventName[actionCount] == "TextBoxFade")
                {//Textbox表示切り替え
                    textBoxController.StartCoroutine("TextBoxFade", arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "SetBackImage")
                {//背景変更
                    backImageManeger.SetBackGroundImage(arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "SetFadeColor")
                {//背景のFadeColorを変更:0は黒,１は白
                    backImageManeger.SetFadeColor(arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "BackFadeView")
                {//背景Fadeしながら表示
                    backImageManeger.FadeView(arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "BackFadeOut")
                {//背景Fadeしながら非表示
                    backImageManeger.FadeOut(arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "FadeView")
                {//背景Fadeしながら表示,TextBoxも
                    backImageManeger.FadeView(arrayNum[actionCount]);
                    textBoxController.StartCoroutine("TextBoxFade", arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "FadeOut")
                {//背景Fadeしながら非表示,TextBoxも
                    backImageManeger.FadeOut(arrayNum[actionCount]);
                    textBoxController.StartCoroutine("TextBoxFade", arrayNum[actionCount]);
                }
                else if (eventName[actionCount] == "SwitchPos")//綴りミスるな
                {
                    charactor[arrayNum[actionCount]].GetComponent<Charactor>().SwichPos(nextOrder[nextOrderNum]);
                    nextOrderNum++;
                }
                else if (eventName[actionCount] == "ChangeSprite")
                {
                    charactor[arrayNum[actionCount]].GetComponent<Charactor>().StartCoroutine("ChangeSpriteCor", nextOrder[nextOrderNum]);
                    SortCharactorLayer();
                    nextOrderNum++;
                }
                else if (eventName[actionCount] == "End")
                {
                    textBoxController.StartCoroutine("TextBoxFade", arrayNum[actionCount]);
                    backImageManeger.FadeOut(arrayNum[actionCount]);
                    Invoke("LoadScene", arrayNum[actionCount] * 0.1f);
                }
                else
                {//Charactorアニメーション再生
                    if (eventName[actionCount] == "ChangePos")//Charactorの位置チェンジ
                    {
                        charactor[arrayNum[actionCount]].GetComponent<Charactor>().StartCoroutine("ChangePosCor", nextOrder[nextOrderNum]);
                        nextOrderNum++;
                    }
                    else
                    {
                        charactor[arrayNum[actionCount]].GetComponent<Charactor>().Invoke(eventName[actionCount], 0);
                        SortCharactorLayer();
                    }
                }
                actionCount++;//次のアクションへ
            }
        }
    }

    public IEnumerator InvokeExecuteAnimation(float waitTime, int textLineNum)
    {
        yield return new WaitForSeconds(waitTime * 0.1f);
        TextController.TextControl = true;
        ExecuteAnimation(textLineNum);
        yield break;
    }
    public void GetAnimationOrder(string[] orderText, int[] orderLine)
    {
        eventName = orderText;
        eventLine = orderLine;//eventの数だけ要素数確保
        arrayNum = new int[eventName.Length];//上と同じ
        nextOrder = new string[eventName.Length];
        for (int i = 0; i < eventName.Length; i++)
        {
            splits = eventName[i].Split(new string[] { " " }, System.StringSplitOptions.None);//Split用配列に代入
            eventName[i] = splits[0];//event名代入
            arrayNum[i] = int.Parse(splits[1]);//charactor,画像,音声などを要素数で指定
            if (eventName[i] == "ChangePos" || eventName[i] == "ChangeSprite" || eventName[i] == "SwitchPos")
            {
                nextOrder[nextOrderNum] = splits[2].Split(new string[] { "\r\n" }, System.StringSplitOptions.None)[0];
                nextOrderNum++;
            }
            for (int j = 0; j < splits.Length; j++)
            {
                splits[j] = string.Empty;
            }
        }
        nextOrderNum = 0;
    }

    private IEnumerator FadeIn()
    {
        float fadeVal = 0;
        float fadeTime = 1.6f;
        stillView.gameObject.SetActive(true);
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        while (true)
        {
            fadeVal += Time.deltaTime;
            stillView.color = new Color(1, 1, 1, fadeVal / fadeTime);
            if (fadeVal >= fadeTime)
            {
                break;
            }
            yield return null;
        }
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        yield break;
    }

    private IEnumerator FadeOut()
    {
        float fadeVal = 0;
        float fadeTime = 1.6f;
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        while (true)
        {
            fadeVal += Time.deltaTime;
            stillView.color = new Color(1, 1, 1, 1 - fadeVal / fadeTime);
            if (fadeVal >= fadeTime)
            {
                break;
            }
            yield return null;
        }
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        stillView.gameObject.GetComponent<Animator>().Play("0");
        stillView.gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator StillAnimFade(string animName)
    {
        float fadeVal = 0;
        float fadeTime = 1.0f;
        if (stillAnimSkip) yield break;
        bool del = true, view = true;
        while (del)
        {
            fadeVal += Time.deltaTime;
            stillView.color = new Color(1 - fadeVal / fadeTime, 1 - fadeVal / fadeTime, 1 - fadeVal / fadeTime, 1);
            if (fadeVal >= fadeTime)
            {
                del = false;
                break;
            }
            if (stillAnimSkip)
            {
                stillView.color = new Color(1, 1, 1, 1);
                view = false;
                break;
            }
            yield return null;
        }
        if (stillAnimSkip) yield break;
        stillView.gameObject.GetComponent<Animator>().Play(animName);
        fadeVal = 0;
        while (view)
        {
            fadeVal += Time.deltaTime;
            stillView.color = new Color(fadeVal / fadeTime, fadeVal / fadeTime, fadeVal / fadeTime, 1);
            if (fadeVal >= fadeTime)
            {
                view = false;
                break;
            }
            if (stillAnimSkip)
            {
                stillView.color = new Color(1, 1, 1, 1);
                stillAnimSkip = false;
                view = false;
                break;
            }
            yield return null;
        }
    }

    private IEnumerator StillChange(int stillNum)
    {
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        stillView.gameObject.GetComponent<Animator>().Play("0");
        stillAnimSkip = true;
        changeStillView.sprite = stillView.sprite;
        stillView.sprite = stillPictures[stillNum];
        changeStillView.color = new Color(1, 1, 1, 1);
        stillView.color = new Color(1, 1, 1, 1);
        float fadeTime = 0.5f, count = 0;
        while (count <= fadeTime)
        {
            count += Time.deltaTime;
            changeStillView.color = new Color(1, 1, 1, 1 - count / fadeTime);
            yield return null;
        }
        changeStillView.color = new Color(1, 1, 1, 0);
        stillView.color = new Color(1, 1, 1, 1);
        textBoxController.ViewCGs();
        textBoxController.SwitchTextBox();
        yield break;
    }

    private void SortCharactorLayer()
    {
        foreach (var item in charactor)
        {
            if (item.GetComponent<Charactor>().GetViewBool())
            {
                item.GetComponent<Charactor>().SetLayerNum(layerNumList[0]);
            }
        }
        charactor[arrayNum[actionCount]].GetComponent<Charactor>().SetLayerNum(layerNumList[2]);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
