using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    public GameObject TextBox
    {
        get { return textBox; }
    }
    [SerializeField] public bool View { get; private set; }
    private List<float> imagesAlpfa = new List<float>(), textsAlpha = new List<float>();
    [SerializeField]
    private Image textboxWindow;
    Image[] textBoxImage;
    Text[] textBoxText;
    bool fadeDir;

    // Start is called before the first frame update
    void Start()
    {
        View = false;
        fadeDir = true;
        textBoxImage = textBox.GetComponentsInChildren<Image>();
        textBoxText = textBox.GetComponentsInChildren<Text>();
        foreach (var item in textBoxImage)
        {
            imagesAlpfa.Add(item.color.a);
        }
        foreach (var item in textBoxText)
        {
            textsAlpha.Add(item.color.a);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchTextBox()
    {
        if (TextBox.activeSelf)
        {
            TextBox.SetActive(false);
        }
        else if (!TextBox.activeSelf)
        {
            TextBox.SetActive(true);
        }
    }
    public void ViewCGs()
    {
        if (View) View = false;
        else View = true;
    }

    public IEnumerator TextBoxFade(int time)
    {
        float fadeVal = 0;
        float fadeTime = time;
        TextController.TextControl = false;
        while (true)
        {
            fadeVal += Time.deltaTime;
            if (fadeDir)
            {
                //textboxWindow.color = new Color(textboxWindow.color.r, textboxWindow.color.g, textboxWindow.color.b, (fadeVal / fadeTime * windowAlpfa));
                for (int i = 0; i < textBoxImage.Length; i++)
                {
                    textBoxImage[i].color = new Color(textBoxImage[i].color.r, textBoxImage[i].color.g, textBoxImage[i].color.b, (fadeVal / fadeTime * imagesAlpfa[i]));
                }
                for (int i = 0; i < textBoxText.Length; i++)
                {
                    textBoxText[i].color = new Color(textBoxText[i].color.r, textBoxText[i].color.g, textBoxText[i].color.b, (fadeVal / fadeTime * textsAlpha[i]));
                }
            }
            if (!fadeDir)
            {
                //textboxWindow.color = new Color(textboxWindow.color.r, textboxWindow.color.g, textboxWindow.color.b, windowAlpfa - (fadeVal / fadeTime * windowAlpfa));
                for (int i = 0; i < textBoxImage.Length; i++)
                {
                    textBoxImage[i].color = new Color(textBoxImage[i].color.r, textBoxImage[i].color.g, textBoxImage[i].color.b, imagesAlpfa[i] - (fadeVal / fadeTime * imagesAlpfa[i]));
                }
                for (int i = 0; i < textBoxText.Length; i++)
                {
                    textBoxText[i].color = new Color(textBoxText[i].color.r, textBoxText[i].color.g, textBoxText[i].color.b, imagesAlpfa[i] - (fadeVal / fadeTime * textsAlpha[i]));
                }
            }
            if (fadeVal >= fadeTime)
            {
                break;
            }
            yield return null;
        }
        if (fadeDir)
        {
            TextController.TextControl = true;
        }
        fadeDir = (fadeDir) ? false : true;
        yield break;
    }
}
