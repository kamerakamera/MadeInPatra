﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {
	[SerializeField] private Text uiText;
	[SerializeField] private ScenarioManeger scenarioManeger;
	[SerializeField]private AnimationManeger animationManeger;
	private float textUpdateInterval = 0.1f,textUpdateTime = 0;
	private string currentText;
	private int textCount = -1,scenarioIndex = 0;
	[SerializeField]private Animator cursorAnim;
	[SerializeField]private GameObject textBox;
	// Use this for initialization
	void Start () {
		SetNextText();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			if(textBox.activeSelf){
				textBox.SetActive(false);
			}
			else if(!textBox.activeSelf){
				textBox.SetActive(true);
			}
		}
		if(textBox.activeSelf){
			if(Input.GetMouseButtonDown(0)){
				if(currentText.Length <= textCount){
					SetNextText();
				}else if(currentText.Length > textCount){
					textUpdateInterval = 0;
				}
			}
			if(currentText.Length > textCount){
				cursorAnim.SetBool("wait",true);
				if(textUpdateInterval == 0){
					textCount = currentText.Length;
					uiText.text = currentText.Substring(0,currentText.Length);
					cursorAnim.SetBool("wait",false);
				}else{
					textCount = (int)((Time.time - textUpdateTime)/textUpdateInterval);
					if(textCount > currentText.Length){//クリックで経過した時間がテキストの長さの経過時間を超えた時
						uiText.text = currentText.Substring(0,currentText.Length);
						textUpdateInterval = 0;
					}else{
						uiText.text = currentText.Substring(0,textCount);
					}
				}
			}
		}
	}


	void SetNextText(){
		if(scenarioIndex >= scenarioManeger.GetTextLine()){
			Debug.Log("end");
			//テキスト終了
		}else{
			currentText = scenarioManeger.GetCurrentText(scenarioIndex);
			animationManeger.ExecuteAnimation(scenarioIndex);//animation呼び出し
			scenarioIndex++;
		}
		textCount = 0;
		textUpdateTime = Time.time;
		textUpdateInterval = 0.1f;
	}
}
