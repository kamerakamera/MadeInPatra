using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    Dictionary<string, Vector3> charactorPos = new Dictionary<string, Vector3>{
        {"center",new Vector3(400f, 150f, 1000f)},
        {"sideLeft",new Vector3(-500f, 150f, 1000f)},
        {"sideRight",new Vector3(1300f, 150f, 1000f)},
        {"right",new Vector3(-50f, 150f, 1000f)},
        {"left",new Vector3(850f, 150f, 1000f)},
        {"out",new Vector3(1000f,1000f,1000f)}
    };
    public static Vector3 center = new Vector3(400f, 150f, 1000f), sideLeft = new Vector3(-500f, 150f, 1000f), sideRight = new Vector3(1300f, 150f, 1000f);
    public static Vector3 twoPosRight = new Vector3(-50f, 150f, 1000f), twoPosLeft = new Vector3(850f, 150f, 1000f);
    private float enterDist = 150f, enterTime = 1f;
    private float hopDist = 100f, hopTime = 0.04f;//パラメータ調整頑張って
    private float changePosTime = 1f;
    private Vector3 myPos;
    [SerializeField]
    private string firstPosName;
    public bool IsAnim { get; private set; }
    [SerializeField]
    private Animator animator;
    [SerializeField]
    SpriteRenderer nowSprite, beforeSprite;
    [SerializeField]
    Sprite[] spriteList = new Sprite[14];
    string[] expressionName = new string[]{
        "normal"/*通常*/,"smile"/*笑顔*/,"closeEyes"/*目をつむる*/,"sumg"/*どや顔*/,"impatience"/*焦り*/,"surprise"/*驚き*/,"troubled"/*困り顔*/,"sadness"/*悲しみ*/,"anger"/*怒り*/,"doubt"/*疑問*/,"beforeButtle"/*戦闘前*/,"damned"/*呆れ顔*/,"grin"/*にやけ顔*/,"ashamed"/*恥じらい*/
    };
    Dictionary<string, Sprite> changeSprite = new Dictionary<string, Sprite>();


    // Start is called before the first frame update
    void Start()
    {
        animator.Play("Wait");
        SkipAnim();
        for (int i = 0; i < expressionName.Length; i++)
        {
            if (spriteList[i] == null) continue;
            changeSprite.Add(expressionName[i], spriteList[i]);
        }
        nowSprite.sprite = changeSprite[expressionName[0]];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAnimSprite(Sprite[] sprites)
    {
        spriteList = sprites;
    }

    public void SwichPos(string posName)
    {
        myPos = charactorPos[posName];
    }

    public void SkipAnim()//Animationスキップ
    {
        if (IsAnim)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
            {
                animator.Play("Idle");
            }
            IsAnim = false;
        }
        transform.position = myPos;
    }

    public IEnumerator ChangeSpriteCor(string nextExpression)
    {
        beforeSprite.sprite = nowSprite.sprite;
        nowSprite.sprite = changeSprite[nextExpression];
        animator.Play("ChangeSprite");
        yield break;
    }

    public IEnumerator ChangePosCor(string targetString)
    {
        SwichPos(targetString);
        float moveDistance = myPos.x - transform.position.x;
        IsAnim = true;
        while (IsAnim)
        {
            if (moveDistance < 0)//moveDistanceが正の時
            {
                if (transform.position.x + moveDistance * Time.deltaTime / changePosTime <= myPos.x)
                {
                    break;
                }
                else
                {
                    transform.position += new Vector3(moveDistance * Time.deltaTime / changePosTime, 0, 0);
                }
            }
            else//moveDistanceが負の時
            {
                if (transform.position.x + moveDistance * Time.deltaTime / changePosTime >= myPos.x)
                {
                    break;
                }
                else
                {
                    transform.position += new Vector3(moveDistance * Time.deltaTime / changePosTime, 0, 0);
                }
            }
            yield return null;
        }
        SkipAnim(); yield break;
    }

    public void Enter()//出現Animation
    {
        IsAnim = true;
        animator.Play("Enter");
        StartCoroutine(EnterCor());
    }

    public IEnumerator EnterCor()
    {
        transform.position = myPos + new Vector3(enterDist, 0, 0);
        while (IsAnim)
        {
            if (transform.position.x - enterDist * Time.deltaTime / enterTime <= myPos.x)
            {
                SkipAnim();
                break;
            }
            else
            {
                transform.position -= new Vector3(enterDist * Time.deltaTime / enterTime, 0, 0);
            }
            yield return null;
        }
        yield break;
    }

    public void Hop()//ジャンプAnimation
    {
        IsAnim = true;
        StartCoroutine(HopCor());
    }

    private IEnumerator HopCor()
    {
        int hopCount = 0;
        bool up = true, down = false;
        while (hopCount < 2 && IsAnim)
        {
            while (up && IsAnim)
            {
                if (transform.position.y + hopDist * Time.deltaTime / hopTime / 4 <= myPos.y + hopDist)
                {
                    transform.position += new Vector3(0, hopDist * Time.deltaTime / hopTime / 4, 0);
                    yield return null;
                }
                else
                {
                    up = false;
                    down = true;
                    break;
                }

            }
            while (down && IsAnim)
            {
                if (transform.position.y - hopDist * Time.deltaTime / hopTime / 4 >= myPos.y)
                {
                    transform.position -= new Vector3(0, hopDist * Time.deltaTime / hopTime / 4, 0);
                    yield return null;
                }
                else
                {
                    down = false;
                    up = true;
                    hopCount++;
                    break;
                }
            }
        }
        SkipAnim(); hopCount = 0; yield break;
    }
}