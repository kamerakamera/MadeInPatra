using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManeger : MonoBehaviour
{
    [SerializeField]
    Charactor[] charactors;
    [SerializeField]
    AnimationManeger animationManeger;
    Sprite[] sprites = new Sprite[StringProperty.expressionName.Length];
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        for (int i = 0; i < charactors.Length; i++)
        {
            for (int j = 0; j < StringProperty.expressionName.Length; j++)
            {
                sprites[j] = Resources.Load<Sprite>("Charactors/" + charactors[i].gameObject.name + "/" + charactors[i].gameObject.name + "_" + StringProperty.expressionName[j]);
                if (sprites[j] == null)
                {
                    Debug.Log(charactors[i].gameObject.name + "には" + StringProperty.expressionName[j] + "の画像がありません");
                    continue;
                }
                Resources.UnloadAsset(sprites[j]);
            }
            charactors[i].SetAnimSprite(sprites);
            sprites = new Sprite[StringProperty.expressionName.Length];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
