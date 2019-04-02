using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private Text uiText, nameText;
    [SerializeField] private ScenarioManeger scenarioManeger;
    [SerializeField] private AnimationManeger animationManeger;
    private float textUpdateInterval = 0.1f, textUpdateTime = 0;
    private string currentText;
    private int textCount = -1, scenarioIndex = 0;
    [SerializeField] private Animator cursorAnim;
    [SerializeField] private TextBoxController textBoxController;
    //[SerializeField] private Text textLog;
    [SerializeField] private TextLogManeger textLogManeger;
    // Use this for initialization
    void Start()
    {
        //textLog.text = null;
        SetNextText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!textBoxController.TextBox.activeSelf)
        {
            textUpdateTime += Time.deltaTime; //TextBox非表示の時にテキストの更新時間を遅延させる
        }
        if (Input.GetMouseButtonDown(1) && !textBoxController.View)
        {
            textBoxController.SwitchTextBox();
        }
        if (textBoxController.TextBox.activeSelf)
        {
            if (currentText.Length > textCount)
            {
                cursorAnim.SetBool("wait", true);
                if (textUpdateInterval == 0)
                {
                    textCount = currentText.Length;
                    uiText.text = currentText.Substring(0, currentText.Length);
                }
                else
                {
                    textCount = (int)((Time.time - textUpdateTime) / textUpdateInterval);
                    if (textCount > currentText.Length)
                    {//クリックで経過した時間がテキストの長さの経過時間を超えた時
                        uiText.text = currentText.Substring(0, currentText.Length);
                        textUpdateInterval = 0;
                    }
                    else
                    {
                        uiText.text = currentText.Substring(0, textCount);
                    }
                }
            }
            else
            {
                cursorAnim.SetBool("wait", false);
            }
        }
    }

    void SetNextText()
    {
        if (scenarioIndex >= scenarioManeger.GetTextLine())
        {
            Debug.Log("end");
            //テキスト終了
        }
        else
        {
            currentText = scenarioManeger.GetCurrentText(scenarioIndex);//現在の行のテキスト代入
            if (currentText[0] == '*')
            {
                nameText.text = currentText.Split(new string[] { "*" }, System.StringSplitOptions.None)[1];//テキストデータのuの文字の前にcharactorの名前を記載しているのでその取得
                currentText = currentText.Substring(nameText.text.Length + 2);//charactorNameと判別のためのｃを削除
            }
            else
            {
                nameText.text = null;
            }
            animationManeger.ExecuteAnimation(scenarioIndex);//animation呼び出し
            scenarioIndex++;
        }

        if (nameText.text != "")
        {
            textLogManeger.CreateLogObj(nameText.text + "\n" + currentText);
        }
        else
        {
            textLogManeger.CreateLogObj(currentText);
        }
        //log機能過去版
        /*if (textLog.text == null)
        {
            if (nameText.text != null)
            {
                textLog.text += nameText.text + "\n";
            }
            textLog.text += currentText;
        }
        else
        {
            if (nameText.text != null)
            {
                textLog.text += nameText.text + "\n";
            }
            textLog.text += currentText;
        }
        textLog.text += "\n\n";*/
        textCount = 0;
        textUpdateTime = Time.time;
        textUpdateInterval = 0.1f;
    }

    public void OnClick()
    {
        if (textBoxController.TextBox.activeSelf)
        {
            if (currentText.Length <= textCount)
            {
                SetNextText();
            }
            else if (currentText.Length > textCount)
            {
                textUpdateInterval = 0;
            }
        }
    }
}
