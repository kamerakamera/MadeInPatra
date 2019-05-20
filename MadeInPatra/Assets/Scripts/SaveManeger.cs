using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stills = new GameObject[StringProperty.stillNames.Length];//Still表示関数内にPlayerPrefにKey追加
    [SerializeField]
    private GameObject[] loadButton = new GameObject[StringProperty.loadSceneName.Length];//Scene開始時にPlayerPrefに追加
    int checkStillNum, checkContinueNum;
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetString(StringProperty.loadSceneName[5], StringProperty.loadSceneName[5]);
        //PlayerPrefs.SetString(StringProperty.stillNames[5], StringProperty.stillNames[5]);
    }
    // Start is called before the first frame update
    void Start()
    {
        checkStillNum = 0;
        checkContinueNum = 0;
        while (checkStillNum < StringProperty.stillNames.Length || checkContinueNum < StringProperty.loadSceneName.Length)
        {
            if (checkStillNum < StringProperty.stillNames.Length)
            {
                if (PlayerPrefs.HasKey(StringProperty.stillNames[checkStillNum]))
                {
                    //Still表示
                    stills[checkStillNum].SetActive(true);
                    Debug.Log(StringProperty.stillNames[checkStillNum] + "のStilを所持しています！");
                }
                else
                {
                    stills[checkStillNum].SetActive(false);
                    Debug.Log(StringProperty.stillNames[checkStillNum] + "のStilを所持していません！");
                }
                checkStillNum++;
            }

            if (checkContinueNum < StringProperty.loadSceneName.Length)
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
                    //loadButton[checkContinueNum].GetComponent<Button>().enabled = true;
                    //loadButton[checkContinueNum].GetComponent<Image>().enabled = true;
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
                    //loadButton[checkContinueNum].GetComponent<Button>().enabled = false;
                    //loadButton[checkContinueNum].GetComponent<Image>().enabled = false;
                    Debug.Log(StringProperty.stillNames[checkContinueNum] + "のデータを所持していません！");
                }
                checkContinueNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
