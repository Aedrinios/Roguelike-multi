using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int numberOfPlayers = 4;
	public Character[] players;
	public GameObject playerPrefab;


	void Start () {
		players = new Character [numberOfPlayers];
		for (int i = 0; i < numberOfPlayers; ++i) {
			GameObject player = Instantiate (playerPrefab, new Vector3 (2, 0, 0) * (i), Quaternion.identity) as GameObject;
			players [i] =
				player.GetComponent<Character> ();
			player.GetComponent<CharController>().SetInputs( new InputSet ("Joystick " + (i +1) ) );
		}
	}
}
