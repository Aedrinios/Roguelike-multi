using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private GameState gameState;

	[HideInInspector]
	public List<GameObject> players;


	void Awake () {
		instance = this;
		SetGameState(this.GetComponent<SelectCharState>());
	}

	public void SetGameState (GameState state){
		if( gameState != null)
			gameState.End();
		gameState = state;
		state.Begin();
	}
}
