using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackImageManeger : MonoBehaviour
{
    [SerializeField]
    private Image backGround, whiteImage;
    [SerializeField]
    private Sprite[] backGroundImageList;
    // Start is called before the first frame update
    void Start()
    {
        whiteImage.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBackGroundImage(int num)
    {
        backGround.sprite = backGroundImageList[num];
    }

    public void SetFadeColor(int num)
    {
        if (num == 0) { whiteImage.color = new Color(0, 0, 0, 0); }
        else { whiteImage.color = new Color(1, 1, 1, 0); }
    }

    public void FadeView(int time)
    {
        StartCoroutine(FadeViewCor(time));
    }

    private IEnumerator FadeViewCor(int time)
    {
        float count = 0, floatTime = time * 0.1f;
        while (count <= floatTime)
        {
            count += Time.deltaTime;
            whiteImage.color = new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b, 1 - count / floatTime);
            yield return null;
        }
        yield break;
    }

    public void FadeOut(int time)
    {
        StartCoroutine(FadeOutCor(time));
    }
    private IEnumerator FadeOutCor(int time)
    {
        float count = 0, floatTime = time * 0.1f;
        while (count <= floatTime)
        {
            Debug.Log(count);
            count += Time.deltaTime;
            whiteImage.color = new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b, count / floatTime);
            yield return null;
        }
        Debug.Log("end");
        yield break;
    }
}
