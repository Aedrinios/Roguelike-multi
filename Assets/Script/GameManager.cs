using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int maxPlayer = 8;
	public Dictionary<string, GameObject> players;
	public GameObject playerPrefab;

	private ControllerDelivery controllerDelivery ;

	void Start () {
		instance = this;
		players = new Dictionary<string, GameObject>();
		controllerDelivery = this.GetComponent<ControllerDelivery>();
	}

	public void ReceiveUpdatedControllers (ArrayList modifiedInputs) {
		Stack<InputSet> playersToAdd = new Stack<InputSet> ();
		foreach ( InputSet inputSet in modifiedInputs )
			if ( inputSet.isActive )
				playersToAdd.Push(inputSet);
			else
				RemoveCharacter (inputSet);   
		AddAllCharacters(playersToAdd);
	}

	private void RemoveCharacter (InputSet inputSet) {
		GameObject playerToRemove = players[inputSet.GetName()];
		players.Remove(inputSet.GetName());
		Destroy(playerToRemove);
	}

	private void AddAllCharacters (Stack<InputSet> playersToAdd) {
		foreach (InputSet inputSet in playersToAdd) {
			GameObject player = Instantiate (playerPrefab) as GameObject;
			CharController playerController = player.GetComponent<CharController>();
			playerController.SetInputs(inputSet);
			players.Add(inputSet.GetName(), player);
			Debug.Log("Player " + inputSet.GetName() + " added");
		}
	}



	
}
