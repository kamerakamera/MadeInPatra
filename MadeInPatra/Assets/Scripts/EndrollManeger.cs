using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class EndrollManeger : MonoBehaviour
{
    [SerializeField]
    private float startTime, endTime;
    private float fadeValue;
    [SerializeField]
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartTimeLine", startTime);
        Invoke("EndScene", endTime);
        Camera.main.backgroundColor = Color.white;
    }

    private void Update()
    {
        if (Time.time <= 1.0f)
        {
            Camera.main.backgroundColor = Color.Lerp(Color.white, Color.black, Time.time);
        }
    }

    void StartTimeLine()
    {
        playableDirector.Play();
    }

    void EndScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
