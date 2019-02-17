using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EventState{
	scenario,battle
}	

public class EventManeger : MonoBehaviour {

	EventState NowState{get;set;}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeEventState(EventState s){
		NowState = s;
	}
}
