using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LetterSceneManeger : Singleton<LetterSceneManeger>
{
    [SerializeField]
    private Animation fadeScreen;
    [SerializeField]
    private AnimationClip[] fadeAnimation;
    List<Sprite> letters = new List<Sprite>();
    [SerializeField]
    private GameObject letterPanel;
    [SerializeField]
    private Animator[] lettersAnimator;
    [SerializeField]
    private CanvasGroup buttonMask;

    // Start is called before the first frame update

    void Start()
    {
        MaskButton();
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.clip = fadeAnimation[0];
        fadeScreen.Play();
        Invoke("LettersOpen", 1.0f);
        Invoke("DownLetters", 2.0f);
        Invoke("IgnoreMaskButton", 3.0f);
    }

    // Update is called once per frame
    public void RightSwap()
    {
        MaskButton();
        SwapLetter.Instance.animator.Play("TakeRightLetter");
        Invoke("IgnoreMaskButton", 1.05f);
    }

    public void LeftSwap()
    {
        MaskButton();
        SwapLetter.Instance.animator.Play("TakeLeftLetter");
        Invoke("IgnoreMaskButton", 1.05f);
    }

    private void MaskButton()
    {
        buttonMask.blocksRaycasts = true;
    }

    private void IgnoreMaskButton()
    {
        buttonMask.blocksRaycasts = false;
    }

    public void OnReturnTitleClick(string sceneName)
    {
        StartCoroutine(OnReturnTitleClickCor(sceneName));
    }

    private void LettersOpen()
    {
        foreach (var item in lettersAnimator)
        {
            item.Play("Open");
        }
    }

    private void DownLetters()
    {
        SwapLetter.Instance.animator.Play("DownLetters");
    }

    private void UpLetters()
    {
        SwapLetter.Instance.animator.Play("UpLetters");
    }

    private void LettersClose()
    {
        foreach (var item in lettersAnimator)
        {
            item.Play("Close");
        }
    }

    private IEnumerator OnReturnTitleClickCor(string sceneName)
    {
        MaskButton();
        UpLetters();
        yield return new WaitForSeconds(1.0f);
        LettersClose();
        fadeScreen.clip = fadeAnimation[1];
        fadeScreen.Play("FadeView");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }
}
