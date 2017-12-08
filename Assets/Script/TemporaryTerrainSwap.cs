using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryTerrainSwap : MonoBehaviour {

	public DirectionX directionX = DirectionX.AUCUNE;
	public DirectionY directionY = DirectionY.AUCUNE;
	public TemporaryTerrainSwap linkedTerrain; 
	private ArrayList players= new ArrayList();
	private bool playersJustSwapped = false;
	private static PlayState gameState;
	Vector3 movement;

	void Start () {
		movement = new Vector3((float) directionX, (float) directionY, 0);
		if (gameState == null && GameManager.instance.GetComponent<PlayState>() != null){
			gameState = GameManager.instance.GetComponent<PlayState>();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			players.Add(other.gameObject);
			if (gameState.isActiveAndEnabled && !playersJustSwapped && players.Count == GameManager.instance.players.Count){
				SwapTerrain();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player"){
			players.Remove(other.gameObject);
			playersJustSwapped = false;
		}
	}

	void SwapTerrain () {
		linkedTerrain.playersJustSwapped = true;
		gameState.walls.transform.position += movement;
		gameState.captureCamera.transform.position += movement;
		foreach (GameObject player in players)
			player.transform.position = linkedTerrain.gameObject.transform.position;
	}

}

public enum DirectionX {
	EST = 32,
	OUEST = -32,
	AUCUNE = 0
}

public enum DirectionY {
	NORD = 18,
	SUD = -18,
	AUCUNE = 0
}
