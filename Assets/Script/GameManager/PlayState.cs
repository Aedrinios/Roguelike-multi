using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState {

	private static PlayState instance;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void End () {
		
	}

	public static PlayState GetInstance(){
		return (instance != null) ? instance : instance = new PlayState();
	}
}
