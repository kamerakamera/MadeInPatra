using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpriteManeger : MonoBehaviour
{
    [SerializeField]
    Charactor[] charactors;
    [SerializeField]
    AnimationManeger animationManeger;
    string[] expressionName = new string[]{
        "normal"/*通常*/,"smile"/*笑顔*/,"closeEyes"/*目をつむる*/,"sumg"/*どや顔*/,"impatience"/*焦り*/,"surprise"/*驚き*/,"troubled"/*困り顔*/,"sadness"/*悲しみ*/,"anger"/*怒り*/,"doubt"/*疑問*/,"beforeButtle"/*戦闘前*/,"damned"/*呆れ顔*/,"grin"/*にやけ顔*/,"ashamed"/*恥じらい*/
    };
    Sprite[] sprites = new Sprite[14];
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < charactors.Length; i++)
        {
            for (int j = 0; j < expressionName.Length; j++)
            {
                sprites[j] = Resources.Load<Sprite>("Charactors/" + charactors[i].gameObject.name + "/" + charactors[i].gameObject.name + "_" + expressionName[j]);
                if (sprites[j] == null)
                {
                    Debug.Log("このCharactorには" + expressionName[j] + "の画像がありません");
                    continue;
                }
                Resources.UnloadAsset(sprites[j]);
            }
            charactors[i].SetAnimSprite(sprites);
            sprites = new Sprite[expressionName.Length];
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
