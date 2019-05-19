using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLogManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject logWindow;
    [SerializeField]
    private GameObject logObjectPre;
    [SerializeField]
    private Transform logContent;
    private GameObject logObj;
    [SerializeField]
    private ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in logContent)
        {
            Destroy(child.gameObject);
        }
        SwitchLogWindow();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("a"))
        {
            SwitchLogWindow();
        } */
    }

    public void CreateLogObj(string str)
    {
        logObj = Instantiate(logObjectPre, logContent);
        Text logText = logObj.GetComponent<Text>();
        logText.text = str;
        logText.rectTransform.sizeDelta = new Vector2(logText.preferredWidth, logText.preferredHeight);//logobjのサイズを設定
        //logObj.gameObject.transform.SetParent(logContent);//logContentを親objに設定
    }

    private void SwitchLogWindow()
    {
        if (!logWindow.gameObject.activeSelf)
        {
            logWindow.SetActive(true);
            scrollRect.verticalNormalizedPosition = 0;//logwindowの位置初期化
        }
        else
        {
            logWindow.SetActive(false);
        }
    }

    public void OnClick(){
        SwitchLogWindow();
    }
}
