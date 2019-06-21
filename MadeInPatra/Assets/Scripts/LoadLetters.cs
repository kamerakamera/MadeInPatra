using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLetters : Singleton<LoadLetters>
{
    public Dictionary<string, Sprite[]> LetterForHoneyStrap { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        LetterForHoneyStrap = new Dictionary<string, Sprite[]>()
        {
            {"patra",Resources.LoadAll<Sprite>("Letters/patra")},
            {"eli",Resources.LoadAll<Sprite>("Letters/eli")},
            {"mico",Resources.LoadAll<Sprite>("Letters/mico")},
            {"charlotte",Resources.LoadAll<Sprite>("Letters/charlotte")},
            {"mary",Resources.LoadAll<Sprite>("Letters/mary")}
        };
        foreach (var item in LetterForHoneyStrap)
        {
            if (item.Value.Length <= 0)
            {
                Debug.Log("LetterLoadErrer " + item.Key);
                continue;
            }
            Resources.UnloadUnusedAssets();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
