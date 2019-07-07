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
    private int LetterIndex;
    // Start is called before the first frame update
    private void Start()//初期化
    {
        isAnim = false;
        viewLetter.sprite = LoadLetters.Instance.GetNextLetter("right");
        SetLetterSize();
    }

    private void StartSwap(string direction)
    {
        isAnim = true;
        underLetter.sprite = LoadLetters.instance.GetNextLetter(direction);
        SetLetterSize();
    }
    private void ViewFront()
    {
        underLetter.gameObject.transform.SetAsLastSibling();
    }

    private void ViewBack()
    {
        underLetter.gameObject.transform.SetAsFirstSibling();
    }

    private void ViewLetterFront()
    {
        viewLetter.gameObject.transform.SetAsLastSibling();
    }

    private void ViewLetterBack()
    {
        viewLetter.gameObject.transform.SetAsFirstSibling();
    }

    private void ChangeImage()
    {
        swapSp = viewLetter.sprite;
        viewLetter.sprite = underLetter.sprite;
        underLetter.sprite = swapSp;
        isAnim = false;
        SetLetterSize();
    }

    private void SetLetterSize()
    {
        viewLetter.SetNativeSize();
        underLetter.SetNativeSize();
    }
}
