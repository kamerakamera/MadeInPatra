using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenuPanel, continuePanel, stillsViewPanel;
    [SerializeField]
    private Animator stillViewsAnimator, startManuAnimator;
    // Start is called before the first frame update

    private void Awake()
    {
        continuePanel.SetActive(false);
        stillsViewPanel.SetActive(false);
    }
    void Start()
    {
        stillViewsAnimator.Play("MoveStill");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {
        Debug.Log("");
    }

    public void OnContinueButtonClick()
    {
        Debug.Log("");
    }

    public void OnCGsViewButtonClick()
    {
        stillViewsAnimator.Play("MoveStill");
        Debug.Log("");
    }

    public void OnMessagesButtonClick()
    {
        Debug.Log("");
    }

    public void OnReturnTitleButtonClick()
    {

    }

    public void OnEndButtonClick()
    {
        Debug.Log("");
    }
}
