using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

   
    public GameObject[] Entities = new GameObject[0];
    public GameObject[] InanimateEntities = new GameObject[0];

    public GameObject[] SpawnerEntities = new GameObject[0];
    public GameObject[] SpawnerInanimateEntities = new GameObject[0];
    private Vector3 position;

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
                position.z = -0.2f;

                var lel = Instantiate(Entities[(int)randomPickEntities], position, Quaternion.identity);
                lel.SetActive(false);
            }


        for (int i = 0; i < SpawnerInanimateEntities.Length; i++)
        {
            Random();
            position.x = SpawnerInanimateEntities[i].transform.position.x;
            position.y = SpawnerInanimateEntities[i].transform.position.y;
            position.z = -0.2f;

            Instantiate(Entities[(int)randomPickInanimateEntities], position, Quaternion.identity);
        }
    }
}
