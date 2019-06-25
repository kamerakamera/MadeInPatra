using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManeger : Singleton<ScenarioManeger>
{
    public string loadFileName;
    private string[] scenarios;
    private int currentTextLine, textLine, currentIndex = 0, textLength = 30;

    // Use this for initialization
    void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetScenario(string[] loadScenarioText)
    {
        scenarios = loadScenarioText;
        for (int i = 0; i < scenarios.Length; i++)
        {
            if (scenarios[i].Length / textLength >= 1)
            {
                for (int j = scenarios[i].Length / textLength; j > 0; j--)
                {
                    scenarios[i] = scenarios[i].Insert(j * textLength, "\r\n");
                }
            }
        }
        textLine = scenarios.Length;
        currentIndex = 0;
        Debug.Log(textLine);
    }

    public int GetTextLine()
    {
        return textLine;
    }
    public string GetCurrentText(int num)
    {
        if (scenarios[num] == null)
        {
            Debug.Log("TextDateNull");
            return null;
        }
        else
        {
            return scenarios[num];
        }
    }

}
