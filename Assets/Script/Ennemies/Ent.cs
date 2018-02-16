using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : MonoBehaviour {
    private GameObject currentTarget;
    private List<GameObject> players;

	// Use this for initialization
	void Start () {
        players = GameManager.instance.players;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void chooseNewTarget()
    {
        int rdm = Mathf.RoundToInt(Random.Range(0, players.Count));
        currentTarget = players[rdm];
        Debug.Log(currentTarget);
    }
}
