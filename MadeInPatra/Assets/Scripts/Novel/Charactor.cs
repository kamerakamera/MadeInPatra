using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    Dictionary<string, Vector3> charactorPos = new Dictionary<string, Vector3>{
        {"center",new Vector3(400f, 150f, 1000f)},
        {"sideRight",new Vector3(-500f, 150f, 1000f)},
        {"sideLeft",new Vector3(1300f, 150f, 1000f)},
        {"right",new Vector3(-50f, 150f, 1000f)},
        {"left",new Vector3(850f, 150f, 1000f)},
        {"out",new Vector3(1000f,1000f,1000f)}
    };
    //public static Vector3 center = new Vector3(400f, 150f, 1000f), sideLeft = new Vector3(-500f, 150f, 1000f), sideRight = new Vector3(1300f, 150f, 1000f);
    //public static Vector3 twoPosRight = new Vector3(-50f, 150f, 1000f), twoPosLeft = new Vector3(850f, 150f, 1000f);
    private float enterDist = 150f, enterTime = 1f;
    private float hopDist = 100f, hopTime = 0.04f;//パラメータ調整頑張って
    private float changePosTime = 1f;
    private Vector3 myPos = new Vector3(400f, 150f, 1000f);
    [SerializeField]
    private string firstPosName;
    public bool IsAnim { get; private set; }
    [SerializeField]
    private Animator animator;
    [SerializeField]
    SpriteRenderer nowSprite, beforeSprite;
    Sprite[] spriteList = new Sprite[StringProperty.expressionName.Length];

    Dictionary<string, Sprite> changeSprite = new Dictionary<string, Sprite>();


    // Start is called before the first frame update
    void Start()
    {
        //myPos = charactorPos["center"];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLayerNum(int num)
    {
        nowSprite.sortingOrder = num;
        beforeSprite.sortingOrder = num;
    }

    public bool GetViewBool()
    {
        if (nowSprite.color.a == 0)
        {
            return false;
        }
        return true;
    }

    public void SetSprite()
    {
        animator.Play("Wait");
        SkipAnim();
        for (int i = 0; i < StringProperty.expressionName.Length; i++)
        {
            if (spriteList[i] == null) continue;
            changeSprite.Add(StringProperty.expressionName[i], spriteList[i]);
        }
        nowSprite.sprite = changeSprite[StringProperty.expressionName[0]];
    }

    public void SetAnimSprite(Sprite[] sprites)
    {
        spriteList = sprites;
        SetSprite();
    }

    public void SwichPos(string posName)
    {
        myPos = charactorPos[posName];
    }

    public void SkipAnim()//Animationスキップ
    {
        if (IsAnim)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Wait") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
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
        Debug.Log(nextExpression);
        nowSprite.sprite = changeSprite[nextExpression];
        animator.Play("ChangeSprite");
        yield break;
    }

    public IEnumerator ChangePosCor(string targetString)
    {
        SwichPos(targetString);
        Debug.Log(myPos);
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

    public void MoveEnter()//出現Animation
    {
        IsAnim = true;
        animator.Play("Enter");
        Debug.Log(myPos);
        StartCoroutine(EnterCor());
    }

    public void Enter()
    {
        IsAnim = true;
        animator.Play("Enter");
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

    public void Exit()
    {
        IsAnim = true;
        animator.Play("Exit");
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