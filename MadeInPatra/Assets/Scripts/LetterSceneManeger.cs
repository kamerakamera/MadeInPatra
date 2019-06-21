using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LetterSceneManeger : MonoBehaviour
{
    [SerializeField]
    private Animation fadeScreen;
    [SerializeField]
    private AnimationClip[] fadeAnimation;
    List<Sprite> letters = new List<Sprite>();
    [SerializeField]
    private GameObject letterPanel, selectPanel;

    // Start is called before the first frame update

    void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.clip = fadeAnimation[0];
        fadeScreen.Play();
        SwitchFadeScreen();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwitchFadeScreen()
    {
        fadeScreen.gameObject.SetActive(!fadeScreen.gameObject.activeSelf);
    }

    public void OnMemberClick(string memberName)
    {
        int letterCount = LoadLetters.Instance.LetterForHoneyStrap[memberName].Length;
        for (int i = 0; i < letterCount; i++)
        {
            letters.Add(LoadLetters.Instance.LetterForHoneyStrap[memberName][i]);
        }
    }

    private void Switch(){
        
    }

    public void OnReturnTitleClick(string sceneName)
    {
        StartCoroutine(OnReturnTitleClickCor(sceneName));
    }

    private IEnumerator OnReturnTitleClickCor(string sceneName)
    {
        SwitchFadeScreen();
        fadeScreen.clip = fadeAnimation[1];
        fadeScreen.Play("FadeView");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }
}
