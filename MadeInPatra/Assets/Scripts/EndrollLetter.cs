using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndrollLetter : MonoBehaviour
{
    [SerializeField]
    private Image image;
    Sprite[] loadLetters;
    int loadCount = 0;

    private void Start()
    {
        GetLetters();
        loadCount = 0;
    }
    private void GetLetters()
    {
        loadLetters = LoadEndrollLetter.Instance.LoadSprite;
        Debug.Log(loadLetters.ToString());
    }

    public void SetLetter()//letterchange時にAnimationから呼び出し、LoadするたびCountが進む
    {

        image.sprite = loadLetters[loadCount];
        image.SetNativeSize();
        loadCount++;
    }
}
