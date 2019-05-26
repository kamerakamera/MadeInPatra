﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationManeger : MonoBehaviour
{
    private int audioClipCount;
    string[] eventName;
    int[] eventLine, arrayNum;
    int actionCount, cgsCount;
    [SerializeField] private GameObject[] charactor;
    private Animator[] animator = new Animator[10];
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private Image stillView;
    [SerializeField] private Sprite[] stillPictures;
    [SerializeField] private TextBoxController textBoxController;
    private string[] splits = new string[5];//splitしたときの代入用配列
    public string animationFileName;//アニメーションなどの命令テキストファイル 
    private string[] nextOrder;
    private int nextOrderNum;
    private int[] layerNumList = new int[]{
        2,4,6
    };
    [SerializeField]
    BackImageManeger backImageManeger;
    [SerializeField]
    string nextSceneName;


    // Use this for initialization
    void Awake()
    {
        nextOrderNum = 0;
        GetAnimationOrder();
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
                else if (eventName[actionCount] == "Cgview")
                {//CG表示
                    stillView.sprite = stillPictures[arrayNum[actionCount]];
                    PlayerPrefs.SetString(stillPictures[arrayNum[actionCount]].name, stillPictures[arrayNum[actionCount]].name);
                    StartCoroutine("FadeIn");
                }
                else if (eventName[actionCount] == "Cgdel")
                {//CG非表示
                    StartCoroutine("FadeOut");
                }
                else if (eventName[actionCount] == "Cgmove")
                {
                    StartCoroutine(StillAnimFade(arrayNum[actionCount].ToString()));
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
                else if (eventName[actionCount] == "SwichPos")
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
                Debug.Log(actionCount);
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
        nextOrder = new string[eventName.Length];
        for (int i = 0; i < eventName.Length; i++)
        {
            splits = eventName[i].Split(new string[] { " " }, System.StringSplitOptions.None);//Split用配列に代入
            eventName[i] = splits[0];//event名代入
            eventLine[i] = int.Parse(splits[1]);//event行数
            arrayNum[i] = int.Parse(splits[2]);//charactor,画像,音声などを要素数で指定
            if (eventName[i] == "ChangePos" || eventName[i] == "ChangeSprite" || eventName[i] == "SwichPos") { nextOrder[nextOrderNum] = splits[3]; nextOrderNum++; }
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
            yield return null;
        }
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
            yield return null;
        }
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
