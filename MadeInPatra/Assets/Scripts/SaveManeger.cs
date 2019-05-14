using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stills = new GameObject[StringProperty.stillNames.Length];//Still表示関数内にPlayerPrefにKey追加
    [SerializeField]
    private Button[] loadButton = new Button[StringProperty.loadSceneName.Length];//Scene開始時にPlayerPrefに追加
    int checkStillNum, checkContinueNum;
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetString(StringProperty.stillNames[5], StringProperty.stillNames[5]);
        //PlayerPrefs.Save();
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
                    //Still表示
                    //loadButton[checkContinueNum].enabled = true;
                    Debug.Log(StringProperty.stillNames[checkContinueNum] + "のデータを所持しています！");
                }
                else
                {
                    //loadButton[checkContinueNum].enabled = false;
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
