using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndrollLetter : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private Sprite imageList;
    private int letterCount, viewLetterNum = 8;//手紙の枚数

    private void Start()
    {

    }

    private void SetLetter()
    {
        var letter = Resources.Load<Sprite>("Letter/patra/" + "letter" + Random.Range(0, letterCount));//手紙の名前を"letter" + "0~手紙の枚数文"に名前指定
        if (letter == null)
        {
            Debug.Log("nothing letter");
            return;
        }
        image.sprite = letter;
        Resources.UnloadAsset(letter);
    }

}
