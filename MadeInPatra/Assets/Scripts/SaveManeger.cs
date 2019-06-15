using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stillObj = new GameObject[StringProperty.stillNames.Length];//Still表示関数内にPlayerPrefにKey追加
    private Still[] still = new Still[20];
    [SerializeField]
    private GameObject[] loadButton = new GameObject[StringProperty.loadSceneName.Length];//Scene開始時にPlayerPrefに追加
    int checkStillNum, checkContinueNum;
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(StringProperty.stillNames[4], 7);
        PlayerPrefs.SetInt(StringProperty.stillNames[5], 1);
        /*foreach (var item in StringProperty.loadSceneName)
        {
            PlayerPrefs.SetString(item, item);
        }
        foreach (var item in StringProperty.stillNames)
        {
            PlayerPrefs.SetString(item, item);
        } */
        //PlayerPrefs.SetString(StringProperty.loadSceneName[5], StringProperty.loadSceneName[5]);
        //PlayerPrefs.SetString(StringProperty.stillNames[5], StringProperty.stillNames[5]);
        for (int i = 0; i < stillObj.Length; i++)
        {
            still[i] = stillObj[i].GetComponent<Still>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        checkStillNum = 0;
        //Stillの所持チェック→PlayerPrefからInt参照にして枚数を確認
        while (checkStillNum < StringProperty.stillNames.Length)
        {
            if (PlayerPrefs.HasKey(StringProperty.stillNames[checkStillNum]))
            {
                //StillViewの変数に開放枚数を代入
                stillObj[checkStillNum].SetActive(true);
                still[checkStillNum].ViewCount = PlayerPrefs.GetInt(StringProperty.stillNames[checkStillNum], 0);
                Debug.Log(StringProperty.stillNames[checkStillNum] + "のStilを所持しています！");
            }
            else
            {
                stillObj[checkStillNum].SetActive(false);
                still[checkStillNum].ViewCount = 0;
                Debug.Log(StringProperty.stillNames[checkStillNum] + "のStilを所持していません！");
            }
            checkStillNum++;
        }/*
        checkContinueNum = 0;
        while (checkContinueNum < StringProperty.loadSceneName.Length)
        {
            if (PlayerPrefs.HasKey(StringProperty.loadSceneName[checkContinueNum]))
            {
                //LoadButton表示
                foreach (var item in loadButton[checkContinueNum].GetComponentsInChildren<Button>())
                {
                    item.enabled = true;
                }
                foreach (var item in loadButton[checkContinueNum].GetComponentsInChildren<Image>())
                {
                    item.enabled = true;
                }
                Debug.Log(StringProperty.stillNames[checkContinueNum] + "のデータを所持しています！");
            }
            else
            {
                foreach (var item in loadButton[checkContinueNum].GetComponentsInChildren<Button>())
                {
                    item.enabled = false;
                }
                foreach (var item in loadButton[checkContinueNum].GetComponentsInChildren<Image>())
                {
                    item.enabled = false;
                }
                Debug.Log(StringProperty.loadSceneName[checkContinueNum] + "のデータを所持していません！");
            }
            checkContinueNum++;
        } */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
