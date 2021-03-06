﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManeger : MonoBehaviour
{
    [SerializeField]
    Charactor[] charactors;
    Sprite[] sprites = new Sprite[StringProperty.expressionName.Length];
    void Start()
    {
        for (int i = 0; i < charactors.Length; i++)
        {
            for (int j = 0; j < StringProperty.expressionName.Length; j++)
            {
                sprites[j] = Resources.Load<Sprite>("Charactors/" + charactors[i].gameObject.name + "/" + StringProperty.expressionName[j]);
                if (sprites[j] == null)
                {
                    //Debug.Log(charactors[i].gameObject.name + "には" + StringProperty.expressionName[j] + "の画像がありません");
                    continue;
                }
                Resources.UnloadAsset(sprites[j]);
            }
            charactors[i].SetAnimSprite(sprites);
            sprites = new Sprite[StringProperty.expressionName.Length];
        }
    }

}
