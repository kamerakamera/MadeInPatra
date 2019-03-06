using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManeger : MonoBehaviour {
	//[SerializeField]int eventAmount;
	private int audioClipCount;
	[SerializeField]string[] eventName;
	[SerializeField]int[] eventLine,charactorNum;
	int actionCount;
	//[SerializeField]SpriteAnimationController[] spriteAnimationController;
	[SerializeField]private Animator[] animator;
	[SerializeField]private AudioSource audioSource;
	[SerializeField]private AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
		actionCount = 0;
		audioClipCount = 0;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExecuteAnimation(int textLineNum){
		foreach(int num in eventLine){
			if(num == textLineNum){
				if(eventName[actionCount] =="Sound"){//音声再生
					audioSource.clip = audioClip[audioClipCount];
					audioSource.Play();
					audioClipCount++;
				}else{//Charactorアニメーション再生
					animator[charactorNum[actionCount]].SetTrigger(eventName[actionCount]);
				}
				actionCount++;
			}
		}
	}
}
