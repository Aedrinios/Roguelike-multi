using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDetectionZone : MonoBehaviour {

	public ArrayList playersInZone = new ArrayList();
	public Warrior warrior;
	
	void Update(){
		GameObject closestPlayer = null;
		foreach (GameObject player in playersInZone){
			if (closestPlayer == null || IsFirstFurtherThanSecond(closestPlayer, player))
				closestPlayer = player;
		}
		warrior.target = closestPlayer;
	}

	bool IsFirstFurtherThanSecond(GameObject first, GameObject second){
		float distanceA = (first.transform.position - warrior.gameObject.transform.position).magnitude;
		float distanceB = (second.transform.position - warrior.gameObject.transform.position).magnitude;
		return distanceA > distanceB;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			playersInZone.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		playersInZone.Remove(other.gameObject);
	}

	public void PlayerDied (GameObject deadPlayer){
		playersInZone.Remove(deadPlayer);
		if (playersInZone.Count != 0){
			warrior.target = (GameObject) playersInZone[0];
		}
	}

}
