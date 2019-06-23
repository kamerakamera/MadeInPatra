using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapLetter : Singleton<SwapLetter>
{
    [SerializeField]
    private Image viewLetter, underLetter;
    private Sprite swapSp;
    private bool isAnim;
    [SerializeField]
    public Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        isAnim = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown("a") && !isAnim)
        {
            animator.Play("TakeLeftLetter");
        }
    }

    private void StartSwap()
    {
        isAnim = true;
    }
    private void ViewFront()
    {
        Debug.Log("changefront");
        underLetter.gameObject.transform.SetAsLastSibling();
    }

    private void ViewBack()
    {
        underLetter.gameObject.transform.SetAsFirstSibling();
    }

    private void ChangeImage()
    {
        swapSp = viewLetter.sprite;
        viewLetter.sprite = underLetter.sprite;
        underLetter.sprite = swapSp;
        isAnim = false;
    }
}
