using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManeger : MonoBehaviour {
	public string loadFileName;
	private string[] scenarios;
	private int currentTextLine,textLine,currentIndex = 0;
	
	// Use this for initialization
	void Awake(){
		GetScenario();
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetScenario(){
		var scenarioText = Resources.Load<TextAsset>("Scenario/" + loadFileName);
		if(scenarioText == null){
			Debug.Log("load miss");
			return;
		}
		scenarios = scenarioText.text.Split(new string[]{"\n"},System.StringSplitOptions.None);
		Resources.UnloadAsset(scenarioText);
		textLine = scenarios.Length;
		currentIndex = 0;
		Debug.Log(textLine);
	}

	public int GetTextLine(){
		return textLine;
	}
	public string GetCurrentText(int num){
		if(scenarios[num] == null){
			Debug.Log("TextDateNull");
			return null;
		}else{
			return scenarios[num];
		}
	}

}
