using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationManeger : MonoBehaviour {
	//[SerializeField]int eventAmount;
	private int audioClipCount;
	[SerializeField]string[] eventName;
	[SerializeField]int[] eventLine,charactorNum;
	int actionCount,cgsCount;
	float fadeVal,fadeTime = 1.6f;
	//[SerializeField]SpriteAnimationController[] spriteAnimationController;
	[SerializeField]private Animator[] animator;
	[SerializeField]private AudioSource audioSource;
	[SerializeField]private AudioClip[] audioClip;
	[SerializeField]private Image stillView;
	[SerializeField]private Sprite[] stillPictures;
	//[SerializeField]private GameObject textBox;
	// Use this for initialization
	void Start () {
		actionCount = 0;
		audioClipCount = 0;
		cgsCount = 0;
		fadeVal = 0;
		stillView.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ExecuteAnimation(int textLineNum){
		foreach(int num in eventLine){
			if(num == textLineNum){
				if(eventName[actionCount] == "Sound"){//音声再生
					audioSource.clip = audioClip[audioClipCount];
					audioSource.Play();
					audioClipCount++;
				}else if(eventName[actionCount] == "Cgview"){
					stillView.sprite = stillPictures[cgsCount];
					StartCoroutine("FadeIn");
					cgsCount++;
				}else if(eventName[actionCount] == "Cgdel"){
					StartCoroutine("FadeOut");
				}else{//Charactorアニメーション再生
					animator[charactorNum[actionCount]].SetTrigger(eventName[actionCount]);
				}
				actionCount++;
			}
		}
	}

	private IEnumerator FadeIn(){
		fadeVal = 0;
		stillView.gameObject.SetActive(true);
		while(true){
			fadeVal += Time.deltaTime;
			stillView.color = new Color(1,1,1,fadeVal/fadeTime);
			if(fadeVal >= fadeTime){
				break;
			}
			yield return null;
		}
		yield break;
	}

	private IEnumerator FadeOut(){
		fadeVal = 0;
		while(true){
			fadeVal += Time.deltaTime;
			stillView.color = new Color(1,1,1,1 - fadeVal/fadeTime);
			if(fadeVal >= fadeTime){
				break;
			}
			yield return null;
		}
		stillView.gameObject.SetActive(false);
		yield break;
	}
}
