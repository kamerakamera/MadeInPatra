using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject startPanel, continuePanel, stillsViewPanel, messegeViewPanel;
    [SerializeField]
    private Animator stillViewsAnimator, startManuAnimator;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        continuePanel.SetActive(false);
        stillsViewPanel.SetActive(false);
        messegeViewPanel.SetActive(false);
        startPanel.SetActive(true);
        startManuAnimator.Play("StartAnimation");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DelAnim()
    {
        startManuAnimator.Play("StartPanelDel");
        Invoke("StartPanelDel", 1.0f);
    }

    public void OnStartButtonClick()
    {
        DelAnim();
    }

    public void OnContinueButtonClick()
    {
        DelAnim();
    }

    public void OnCGsViewButtonClick()
    {
        DelAnim();
        Invoke("StillView", 1.0f);
    }

    private void StillView()
    {
        stillsViewPanel.SetActive(true);
        stillViewsAnimator.Play("MoveStill");
    }

    public void OnMessagesButtonClick()
    {
        DelAnim();
    }

    public void OnReturnTitleButtonClick(Animator animator)
    {
        animator.Play("Del");
        Invoke("StartPanelView", 1.0f);
    }

    private void StartPanelView()
    {
        continuePanel.SetActive(false);
        stillsViewPanel.SetActive(false);
        messegeViewPanel.SetActive(false);
        foreach (var item in startPanel.GetComponentsInChildren<Image>())
        {
            item.enabled = true;
        }
        startManuAnimator.Play("StartAnimation");
    }

    private void StartPanelDel()
    {
        foreach (var item in startPanel.GetComponentsInChildren<Image>())
        {
            item.enabled = false;
        }
    }

    public void OnEndButtonClick()
    {

    }
}
