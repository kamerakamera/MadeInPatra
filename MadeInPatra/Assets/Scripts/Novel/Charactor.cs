using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public static Vector3 center = new Vector3(400f, 150f, 1000f), sideLeft = new Vector3(-500f, 150f, 1000f), sideRight = new Vector3(1300f, 150f, 1000f);
    public static Vector3 twoPosRight = new Vector3(-50f, 150f, 1000f), twoPosLeft = new Vector3(850f, 150f, 1000f);
    private float enterDist = 150f, enterTime = 1f;
    private float hopDist = 100f, hopTime = 0.04f;//パラメータ調整頑張って
    private Vector3 myPos;
    [SerializeField]
    private string firstPosName;
    public bool IsAnim { get; private set; }
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //test用
        SwichPos(firstPosName);
        SkipAnim();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwichPos(string posName)
    {
        if (posName == "center")
        {
            myPos = center;
        }
        if (posName == "sideLeft")
        {
            myPos = sideLeft;
        }
        if (posName == "sideRight")
        {
            myPos = sideRight;
        }
        if (posName == "right")
        {
            myPos = twoPosRight;
        }
        if (posName == "left")
        {
            myPos = twoPosLeft;
        }
    }

    public void SkipAnim()//Animationスキップ
    {
        Debug.Log(gameObject);
        if (IsAnim)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(info.fullPathHash, 0, 1);
            IsAnim = false;
        }
        transform.position = myPos;
    }

    public void Enter()//出現Animation
    {
        IsAnim = true;
        animator.SetTrigger("Enter");
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
                yield break;
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
        StartCoroutine(HopCor());
    }

    private IEnumerator HopCor()
    {
        int hopCount = 0;
        bool up = true, down = false;
        while (hopCount < 2)
        {
            while (up)
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
            while (down)
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