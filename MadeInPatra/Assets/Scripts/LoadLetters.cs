using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLetters : Singleton<LoadLetters>
{
    //public Dictionary<string, Sprite[]> LetterForHoneyStrap { get; private set; }
    public List<Sprite> LetterForHoneyStrap { get; private set; } = new List<Sprite>();
    private Dictionary<int, string> members = new Dictionary<int, string>()
    {
        {0,"patra"},
        {1,"eli"},
        {2,"mico"},
        {3,"charlotte"},
        {4,"mary"}
    };
    private int viewIndex = -1;//最初の呼び出しがrightなので+1されていい感じになる
    void Awake()//手紙の画像読み込み処理
    {
        for (int i = 0; i < members.Count; i++)
        {
            var loadSprites = Resources.LoadAll<Sprite>("Letters/" + members[i]);
            if (loadSprites.Length <= 0)
            {
                Debug.Log("LetterLoadErrer " + members[i]);
                continue;
            }
            for (int j = 0; j < loadSprites.Length; j++)
            {
                LetterForHoneyStrap.Add(loadSprites[j]);
            }
            foreach (var item in loadSprites)
            {
                Resources.UnloadAsset(item);
            }
            loadSprites = null;
        }
        if (LetterForHoneyStrap.Count <= 0)
        {
            Debug.Log("LoadError Nothing Letters");
        }
    }


    public Sprite GetNextLetter(string direction)//手紙の画像呼び出し
    {
        if (direction == "right")
        {
            ViewIndexCounter(1);
            Debug.Log(viewIndex);
            return LetterForHoneyStrap[viewIndex];
        }
        else if (direction == "left")
        {
            ViewIndexCounter(-1);
            Debug.Log(viewIndex);
            return LetterForHoneyStrap[viewIndex];
        }
        return null;
    }

    void ViewIndexCounter(int num)//手紙の遷移のループ
    {
        viewIndex += num;
        if (viewIndex > LetterForHoneyStrap.Count - 1)
        {
            viewIndex = viewIndex - LetterForHoneyStrap.Count;
        }
        else if (viewIndex < 0)
        {
            viewIndex = LetterForHoneyStrap.Count + viewIndex;
        }
    }
}
