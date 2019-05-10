using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject continuePanel, stillsViewPanel;
    [SerializeField]
    private Animator stillViewsAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {

    }

    public void OnContinueButtonClick()
    {

    }

    public void OnStillsViewButtonClick()
    {
        stillViewsAnimator.Play("MoveStill");
    }

    public void OnMessagesButtonClick()
    {

    }

    public void OnReturnTitleButtonClick()
    {

    }

    public void EndButtonClick()
    {

    }
}
