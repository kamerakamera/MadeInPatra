using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Still : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images = new Sprite[10];
    private int viewNum = 0;
    public int ViewCount { get; set; }

    public int Next()
    {
        if (viewNum + 1 == ViewCount)
        {
            viewNum = 0;
            return 0;
        }
        viewNum++;
        return viewNum;
    }

    public Sprite GetSprite(int num)
    {
        return images[num];
    }
}
