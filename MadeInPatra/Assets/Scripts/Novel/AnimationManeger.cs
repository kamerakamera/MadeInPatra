﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
                else if (eventName[actionCount] == "TextDel")
                {//Textbox非表示
                    textBoxController.StartCoroutine("TextBoxFade", "del");
                }
                else if (eventName[actionCount] == "TextView")
                {//Textbox表示
                    textBoxController.StartCoroutine("TextBoxFade", "view");
                }


                else if (eventName[actionCount] == "SwichPos")
                {
                    charactor[arrayNum[actionCount]].GetComponent<Charactor>().SwichPos(charaOrder[charaOrderNum]);
                    charaOrderNum++;
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
                    }
                }
                actionCount++;//次のアクションへ
            }
        }
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
        charaOrder = new string[eventName.Length];
        for (int i = 0; i < eventName.Length; i++)
        {
            splits = eventName[i].Split(new string[] { " " }, System.StringSplitOptions.None);//Split用配列に代入
            eventName[i] = splits[0];//event名代入
            eventLine[i] = int.Parse(splits[1]);//event行数
            arrayNum[i] = int.Parse(splits[2]);//charactor,画像,音声などを要素数で指定
            if (eventName[i] == "ChangePos" || eventName[i] == "ChangeSprite" || eventName[i] == "SwichPos") { charaOrder[charaOrderNum] = splits[3]; charaOrderNum++; }
            for (int j = 0; j < splits.Length; j++)
            {
                splits[j] = string.Empty;
            }
        }
        charaOrderNum = 0;
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
        stillView.gameObject.SetActive(false);
        yield break;
    }
}
