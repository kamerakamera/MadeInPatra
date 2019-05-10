using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManeger : MonoBehaviour
{
    [SerializeField]
    private Image[] stills;
    [SerializeField]
    private Button[] loadButton;//戦闘終了後にPlayerPrefにKey追加
    private void Awake()
    {
        for (int i = 0; i < StringProperty.stillNames.Length; i++)
        {
            if (PlayerPrefs.HasKey(StringProperty.stillNames[i]))
            {
                //Still表示
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
