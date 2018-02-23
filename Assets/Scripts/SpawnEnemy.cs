using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

   
    public GameObject[] Entities = new GameObject[0];
    public GameObject[] InanimateEntities = new GameObject[0];

    public GameObject[] SpawnerEntities = new GameObject[0];
    public GameObject[] SpawnerInanimateEntities = new GameObject[0];
    private Vector2 position;

    private float randomx;
    private float randomy;
    private float randomPickEntities;
    private float randomPickInanimateEntities;
    private float PosRoomx;
    private float PosRoomy;

	// Use this for initialization
	void Start () {
        Spawn();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Random()
    {
        /*randomx = UnityEngine.Random.Range(-1f,1f);
        randomy = UnityEngine.Random.Range(-0.8f, 0.8f);*/

        randomPickEntities = UnityEngine.Random.Range(0f, Entities.Length);
        randomPickInanimateEntities = UnityEngine.Random.Range(0f, InanimateEntities.Length);
    }


    void Spawn()
    {
        PosRoomx = this.transform.position.x;
        PosRoomy = this.transform.position.y;

        for (int i = 0; i < SpawnerEntities.Length; i++)
        {
            Random();
            position.x = SpawnerEntities[i].transform.position.x;
            position.y = SpawnerEntities[i].transform.position.y;

            Instantiate(Entities[(int)randomPickEntities], position, Quaternion.identity);
        }

        for (int i = 0; i < SpawnerInanimateEntities.Length; i++)
        {
            Random();
            position.x = SpawnerInanimateEntities[i].transform.position.x;
            position.y = SpawnerInanimateEntities[i].transform.position.y;

            Instantiate(Entities[(int)randomPickInanimateEntities], position, Quaternion.identity);
        }
    }
}
