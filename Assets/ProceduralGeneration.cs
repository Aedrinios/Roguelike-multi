using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour {

    public GameObject LevelGenerate;

    private int randomdirection;
    private float directionx;
    private float directiony;
    private List<GameObject> ListLevel = new List<GameObject>();
    private int tailleList;
    private List<int> ListRandom = new List<int>();
    private int TailleListRandom;
    private GameObject Lastelement;
    private float positionx = 0.0f;
    private float positiony = 0.0f;

    // Use this for initialization
    void Start () {
        GenerateMap();
    }

	void Update () {
    }

    void GenerateMap()
    {
        
        ListLevel.Add(Instantiate(LevelGenerate, new Vector3(directionx + positionx, directiony + positiony, 0.0f), Quaternion.identity));
        tailleList = ListLevel.Count;
        ListRandom.Add(5);
        for (int i = 0; i < 5; i++)
        {
            
            randomdirection = Random.Range(0, 4);
            ListRandom.Add(randomdirection);
            TailleListRandom = ListRandom.Count;

            if (ListRandom[TailleListRandom - 1] == ListRandom[TailleListRandom - 2])
            {
                randomdirection = Random.Range(0, 4);
            }
            else {

               // Debug.Log("dernier élément de la list des randoms :"  + ListRandom[TailleListRandom-1]);
                if (randomdirection == 0)
                {
                    directionx = 1.5f;
                    directiony = 0.0f;
                }
                if (randomdirection == 1)
                {
                    directionx = -1.5f;
                    directiony = 0.0f;
                }
                if (randomdirection == 2)
                {
                    directiony = 1.5f;
                    directionx = 0.0f;
                }
                if (randomdirection == 3)
                {
                    directiony = -1.5f;
                    directionx = 0.0f;
                }
                Lastelement = ListLevel[tailleList - 1];
                positionx = Lastelement.transform.position.x;
                positiony = Lastelement.transform.position.y;
                ListLevel.Add(Instantiate(LevelGenerate, new Vector3(directionx + positionx, directiony + positiony, 0.0f), Quaternion.identity));
                tailleList = ListLevel.Count;
            }
        }
    }
}
