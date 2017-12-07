using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private GameState gameState;
	public int maxPlayer = 8;
	public Dictionary<string, GameObject> players;
	public GameObject playerPrefab;


	void Start () {
		instance = this;
		SetGameState(this.GetComponent<SelectCharState>());
		players = new Dictionary<string, GameObject>();
	}

	public void SetGameState (GameState state){
		if( gameState != null)
			gameState.End();
		gameState = state;
		state.Begin();
	}
}
