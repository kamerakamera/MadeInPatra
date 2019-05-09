using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationManeger : MonoBehaviour
{
    //[SerializeField]int eventAmount;
    private int audioClipCount;
    string[] eventName;
    int[] eventLine, arrayNum;
    int actionCount, cgsCount;
    float fadeVal, fadeTime = 1.6f;
    //[SerializeField]SpriteAnimationController[] spriteAnimationController;
    [SerializeField] private GameObject[] charactor;
    private Animator[] animator = new Animator[10];
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private Image stillView;
    [SerializeField] private Sprite[] stillPictures;
    [SerializeField] private TextBoxController textBoxController;
    private string[] splits = new string[5];//splitしたときの代入用配列
    public string animationFileName;//アニメーションなどの命令テキストファイル 
    private string[] charaOrder;
    private int charaOrderNum;

    // Use this for initialization
    void Awake()
    {
        charaOrderNum = 0;
        GetAnimationOrder();
        for (int i = 0; i < charactor.Length; i++)
        {
            animator[i] = charactor[i].GetComponent<Animator>();
        }
    }

    void Start()
    {
        actionCount = 0;
        audioClipCount = 0;
        cgsCount = 0;
        fadeVal = 0;
        stillView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExecuteAnimation(int textLineNum)
    {
        for (int i = 0; i < charactor.Length; i++)//再生中のほかのキャラクターのAnimationをスキップ
        {
            //if (i == arrayNum[actionCount]) { continue; }
            charactor[i].GetComponent<Charactor>().SkipAnim();
        }
        foreach (int num in eventLine)
        {
            if (num == textLineNum)
            {
                if (eventName[actionCount] == "Sound")
                {//音声再生
                    audioSource.clip = audioClip[arrayNum[actionCount]];
                    audioSource.Play();
                    //audioClipCount++;
                }
                else if (eventName[actionCount] == "Cgview")
                {//CG表示
                    stillView.sprite = stillPictures[arrayNum[actionCount]];
                    StartCoroutine("FadeIn");
                    //cgsCount++;
                }
                else if (eventName[actionCount] == "Cgdel")
                {//CG非表示
                    StartCoroutine("FadeOut");
                }
                else if (eventName[actionCount] == "ChangeSprite")
                {
                    charactor[arrayNum[actionCount]].GetComponent<Charactor>().StartCoroutine("ChangeSpriteCor", charaOrder[charaOrderNum]);
                    charaOrderNum++;
                }
                else
                {//Charactorアニメーション再生
                    if (eventName[actionCount] == "ChangePos")//Charactorの位置チェンジ
                    {
                        charactor[arrayNum[actionCount]].GetComponent<Charactor>().StartCoroutine("ChangePosCor", charaOrder[charaOrderNum]);
                        charaOrderNum++;
                    }
                    else
                    {
                        charactor[arrayNum[actionCount]].GetComponent<Charactor>().Invoke(eventName[actionCount], 0);
                        Debug.Log(arrayNum[actionCount]);
                    }
                }
                actionCount++;//次のアクションへ
            }
        }
    }
    /*
        public void SkipAnimation(int textLineNum)
        {
            foreach (int num in eventLine)
            {
                if (num == textLineNum)
                {
                    if (eventName[actionCount] != "Sound" || eventName[actionCount] != "Cgview" || eventName[actionCount] != "Cgdel")
                    {
                        animator[arrayNum[actionCount]].Play(animator[arrayNum[actionCount]].GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 1);//これやばそう
                    }
                }
            }
        }

        public void ResetAnimationSpeed()
        {
            foreach (Animator item in animator)
            {
                if (item == null)
                {
                    return;
                }
                item.speed = 1;
            }
        }
    */
    private void GetAnimationOrder()
    {
        var orderText = Resources.Load<TextAsset>("Scenario/" + animationFileName);//ordertextには命令 行数 (arrayNum)で記述
        if (orderText == null)
        {
            Debug.Log("orderText load error");
            return;
        }
        eventName = orderText.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);//それぞれの命令は改行で区切る
        eventLine = new int[eventName.Length];//eventの数だけ要素数確保
        arrayNum = new int[eventName.Length];//上と同じ
        charaOrder = new string[eventName.Length];
        for (int i = 0; i < eventName.Length; i++)
        {
            splits = eventName[i].Split(new string[] { " " }, System.StringSplitOptions.None);//Split用配列に代入
            eventName[i] = splits[0];//event名代入
            eventLine[i] = int.Parse(splits[1]);//event行数
            arrayNum[i] = int.Parse(splits[2]);//charactor,画像,音声などを要素数で指定
            if (eventName[i] == "ChangePos" || eventName[i] == "ChangeSprite") { charaOrder[charaOrderNum] = splits[3]; charaOrderNum++; }
            for (int j = 0; j < splits.Length; j++)
            {
                splits[j] = string.Empty;
            }
        }
        charaOrderNum = 0;
    }

    private IEnumerator FadeIn()
    {
        fadeVal = 0;
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
        fadeVal = 0;
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
        stillView.gameObject.SetActive(false);
        yield break;
    }
}
