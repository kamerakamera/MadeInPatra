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
    private bool isContinuePanelFade, clearMode = false;
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
        stillView.GetComponent<Image>().enabled = false;
        StartManuAnim("Start");
        if (clearMode = SaveManeger.Instance.ClearFlag)
        {
            if (!PlayerPrefs.HasKey("GameClear") && clearMode)
            {
                Debug.Log(SaveManeger.Instance.ClearFlag);
                StartManuAnim("ChangeClearMode");
                PlayerPrefs.SetInt("GameClear", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DelAnim()
    {
        StartManuAnim("PanelDel");
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

    public void SelectPanel(GameObject panel)
    {
        if (!isContinuePanelFade) StartCoroutine(FadeCor(panel, 0.2f, true));
    }

    IEnumerator FadeCor(GameObject obj, float fadeTime, bool direction)
    {
        isContinuePanelFade = true;
        if (direction) obj.SetActive(true);
        Image[] images = obj.GetComponentsInChildren<Image>();
        float[] alphas = new float[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            alphas[i] = images[i].color.a;//A値の初期値を保存
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);//ImagesのColorのAだけを0にする
        }

        float count = 0;
        while (count <= fadeTime)
        {
            for (int i = 0; i < images.Length; i++)
            {
                if (direction) images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alphas[i] * count / fadeTime);
                else images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alphas[i] - alphas[i] * count / fadeTime);
            }
            count += Time.deltaTime;
            yield return null;
        }
        isContinuePanelFade = false;
        if (!direction) obj.SetActive(false);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alphas[i]);
        }
        yield break;
    }

    public void BackLoadPanel(GameObject panel)
    {
        if (!isContinuePanelFade) StartCoroutine(FadeCor(panel, 0.2f, false));
    }

    public void OnContinueClick(string sceneName)
    {
        if (!isContinuePanelFade) StartCoroutine(LoadScene(sceneName));
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

    public void OnMessagesButtonClick(string loadSceneName)
    {
        DelAnim();
        StartCoroutine(LoadScene(loadSceneName));
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
        StartManuAnim("Start");
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

    private void StartManuAnim(string animName)
    {
        if (animName == "Start")
        {
            if (clearMode)
            {
                startManuAnimator.Play("ClearStartPanelView");
            }
            else
            {
                startManuAnimator.Play("StartAnimation");
            }
        }
        if (animName == "PanelDel")
        {
            if (clearMode)
            {
                startManuAnimator.Play("ClearPanelDel");
            }
            else
            {
                startManuAnimator.Play("StartPanelDel");
            }
        }
        if (animName == "ChangeClearMode")
        {
            startManuAnimator.Play("StartAnimation");
            startManuAnimator.SetBool("change", true);
        }
    }

    private void GameOver()
    {
        Application.Quit();
    }
}
