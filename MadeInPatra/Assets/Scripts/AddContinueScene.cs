using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddContinueScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name + "をloadできるようになりました！");
    }
}
