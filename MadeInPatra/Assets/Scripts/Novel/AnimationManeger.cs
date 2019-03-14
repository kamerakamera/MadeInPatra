using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimationManeger : MonoBehaviour {
	//[SerializeField]int eventAmount;
	private int audioClipCount;
	string[] eventName;
	int[] eventLine,charactorNum;
	int actionCount,cgsCount;
	float fadeVal,fadeTime = 1.6f;
	//[SerializeField]SpriteAnimationController[] spriteAnimationController;
	[SerializeField]private Animator[] animator;
	[SerializeField]private AudioSource audioSource;
	[SerializeField]private AudioClip[] audioClip;
	[SerializeField]private Image stillView;
	[SerializeField]private Sprite[] stillPictures;
	[SerializeField]private TextBoxController textBoxController;
	private string[] splits = new string[3];//splitしたときの代入用配列
	public string animationFileName;//アニメーションなどの命令テキストファイル
	//[SerializeField]private GameObject textBox;
	// Use this for initialization
	void Awake() {
		GetAnimationOrder();
	}
	
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
					audioSource.clip = audioClip[charactorNum[actionCount]];
					audioSource.Play();
					//audioClipCount++;
				}else if(eventName[actionCount] == "Cgview"){//CG表示
					stillView.sprite = stillPictures[charactorNum[actionCount]];
					StartCoroutine("FadeIn");
					//cgsCount++;
				}else if(eventName[actionCount] == "Cgdel"){//CG非表示
					StartCoroutine("FadeOut");
				}else{//Charactorアニメーション再生
					animator[charactorNum[actionCount]].SetTrigger(eventName[actionCount]);
				}
				actionCount++;//次のアクションへ
			}
		}
	}

	private void GetAnimationOrder(){
		var orderText = Resources.Load<TextAsset>("Scenario/" + animationFileName);//ordertextには命令 行数 (charactornum)で記述
		if(orderText == null){
			Debug.Log("orderText load error");
			return;
		}
		eventName = orderText.text.Split(new string[]{"\n"},System.StringSplitOptions.None);//それぞれの命令は改行で区切る
		eventLine = new int[eventName.Length];//eventの数だけ要素数確保
		charactorNum = new int[eventName.Length];//上と同じ
		for(int i = 0;i < eventName.Length;i++){
			splits = eventName[i].Split(new string[]{" "},System.StringSplitOptions.None);//Split用配列に代入
			eventName[i] = splits[0];//event名代入
			eventLine[i] = int.Parse(splits[1]);//event行数
			charactorNum[i] = int.Parse(splits[2]);//charactor,画像,音声などを要素数で指定
			for(int j = 0;j < 3;j++){
				splits[j] = string.Empty;
			}
		}
	}

	private IEnumerator FadeIn(){
		fadeVal = 0;
		stillView.gameObject.SetActive(true);
		textBoxController.ViewCGs();
		textBoxController.SwitchTextBox();
		while(true){
			fadeVal += Time.deltaTime;
			stillView.color = new Color(1,1,1,fadeVal/fadeTime);
			if(fadeVal >= fadeTime){
				break;
			}
			yield return null;
		}
		textBoxController.ViewCGs();
		textBoxController.SwitchTextBox();
		yield break;
	}

	private IEnumerator FadeOut(){
		fadeVal = 0;
		textBoxController.ViewCGs();
		textBoxController.SwitchTextBox();
		while(true){
			fadeVal += Time.deltaTime;
			stillView.color = new Color(1,1,1,1 - fadeVal/fadeTime);
			if(fadeVal >= fadeTime){
				break;
			}
			yield return null;
		}
		textBoxController.ViewCGs();
		textBoxController.SwitchTextBox();
		stillView.gameObject.SetActive(false);
		yield break;
	}
}
