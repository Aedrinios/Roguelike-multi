using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionDummy : MonoBehaviour {

	public GameObject[] availablePlayers;
	public Sprite defaultSprite;
	//UI elements here 
	private int selectedPlayer = 0;
	public int SelectedPlayer {
		get {
			return selectedPlayer;
		}
		set {
			selectedPlayer = (value < 0) ? value + availablePlayers.Length : value % availablePlayers.Length;
			UpdatePlayerInformations();
		}
	}

	private void UpdatePlayerInformations(){
		this.GetComponent<SpriteRenderer>().sprite =
		 availablePlayers[selectedPlayer].GetComponent<SpriteRenderer>().sprite;

		
		this.GetComponent<SpriteRenderer>().color =
		 availablePlayers[selectedPlayer].GetComponent<SpriteRenderer>().color;

		UpdateUI();
	}

	private void UpdateUI(){
		// TODO
		// Change hearts
		// Change Strength
		// Change Speed
	}

	void Start(){
		Reset();
	}

	public void Reset(){
		this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
		RemoveUI();
	}

	private void RemoveUI(){
		// TODO
		// remove hearts
		// remove Strength
		// remove Speed
	}

	public void Possess (){
		//Posession animation?
		UpdatePlayerInformations();
	}

	public GameObject GetSelectedPlayer(){
		return availablePlayers[selectedPlayer];
	}
	

}
