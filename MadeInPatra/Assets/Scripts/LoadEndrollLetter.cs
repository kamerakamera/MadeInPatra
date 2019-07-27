using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadEndrollLetter : Singleton<LoadEndrollLetter>
{
    public Sprite[] LoadSprite { get; private set; } = new Sprite[8];

    private void Awake()
    {
        int randNum = Random.Range(0, StringProperty.letterList.GetLength(0));
        int loadCount = 0;
        for (int i = 0; i < StringProperty.letterList[randNum].Length; i++)
        {
            for (int j = 1; j < StringProperty.letterCount[StringProperty.letterList[randNum][i]] + 1; j++)
            {
                var letter = Resources.Load<Sprite>("Letters/patra/letter" + StringProperty.letterList[randNum][i] + "-" + j);
                if (letter == null)
                {
                    Debug.Log(StringProperty.letterList[randNum][i] + "-" + j + "loadError");
                    break;
                }
                Debug.Log(letter.name + "loaded!");
                LoadSprite[loadCount] = letter;
                Resources.UnloadAsset(letter);
                loadCount++;
            }
        }
    }
}
