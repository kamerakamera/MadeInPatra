using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScenarioManeger : Singleton<GetScenarioManeger>
{
    [SerializeField]
    private string scenarioFileName;
    private string[] ScenarioText { get; set; }
    private List<string> OrderText { get; set; } = new List<string>();
    private int loadLine;
    private List<int> eventLine = new List<int>();
    private void Awake()
    {
        GetScenario();
    }

    private void GetScenario()
    {
        var loadText = Resources.Load<TextAsset>("Scenario/" + scenarioFileName);//ordertextには命令 行数 (arrayNum)で記述
        if (loadText == null)
        {
            Debug.Log("scenarioText load error");
            return;
        }
        ScenarioText = loadText.text.Split(new string[] { ">" }, System.StringSplitOptions.None);//Scenario文を'>'でスプリット(Scenario文とAnimationOrderは一緒)
        loadLine = ScenarioText.Length;
        for (int i = 0; i < loadLine; i++)
        {
            string[] splitOrderText = ScenarioText[i].Split(new string[] { "~" }, System.StringSplitOptions.None);
            ScenarioText[i] = splitOrderText[0];
            for (int j = 1; j < splitOrderText.Length; j++)
            {
                eventLine.Add(i);
                OrderText.Add(splitOrderText[j]);
            }
            //Order文は半角の~で区切る,Indexの1番目がOrder文
        }
        if (OrderText.Count <= 0)
        {
            Debug.Log("orderText load error");
            return;
        }
        Resources.UnloadAsset(loadText);
        ScenarioManeger.Instance.GetScenario(ScenarioText);
        AnimationManeger.Instance.GetAnimationOrder(OrderText.ToArray(), eventLine.ToArray());
    }
}
