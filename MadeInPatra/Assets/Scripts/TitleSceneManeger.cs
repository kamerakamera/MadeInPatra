using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject startPanel, continuePanel, stillsViewPanel, messegeViewPanel, stillView;
    [SerializeField]
    private Animator stillViewsAnimator, startManuAnimator, continuePanelAnimator;
    [SerializeField]
    private Animation fadeScreen;
    private Still viewStillClass;
    private static bool clearFlag;
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
        stillView.GetComponent<Image>().enabled = false;
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
        StartCoroutine(LoadScene("Beginning"));
    }

    public void OnContinueButtonClick()
    {
        DelAnim();
        Invoke("ContinueView", 1.0f);
    }

    private void ContinueView()
    {
        continuePanel.SetActive(true);
        continuePanelAnimator.Play("PanelView");
    }

    public void SelectPanel(GameObject panel){
        panel.SetActive(true);
    }

    public void BackLoadPanel(GameObject panel){
        panel.SetActive(false);
    }

    public void OnContinueClick(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
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

    public void SelectStill(Still still)
    {//選択したButtonに対応するStillクラスにアクセスしそのcountを参照
        if (still.ViewCount > 0)
        {
            stillView.GetComponent<Image>().sprite = still.GetSprite(0);
            stillView.GetComponent<Image>().enabled = true;
            viewStillClass = still;
        }
    }

    public void StillViewButton()
    {
        int nextNum = viewStillClass.Next();
        if (nextNum == 0)
        {
            stillView.GetComponent<Image>().enabled = false;
            viewStillClass = null;
            return;
        }
        stillView.GetComponent<Image>().sprite = viewStillClass.GetSprite(nextNum);
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

    private IEnumerator LoadScene(string sceneName)
    {
        fadeScreen.Play();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
        yield break;
    }

    public void OnEndButtonClick()
    {
        fadeScreen.Play();
        Invoke("GameOver", 1.0f);
    }

    private void GameOver()
    {
        Application.Quit();
    }
}
