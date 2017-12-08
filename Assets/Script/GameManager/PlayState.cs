using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState {

	public GameObject walls, captureCamera;
	private static PlayState instance;

	void Start () {
		
	}

	public override void Begin (){
		base.Begin();
	}
	
	void Update () {
		
	}

	public override void End () {
		base.End();
	}

	public static PlayState GetInstance(){
		return (instance != null) ? instance : instance = new PlayState();
	}
}
