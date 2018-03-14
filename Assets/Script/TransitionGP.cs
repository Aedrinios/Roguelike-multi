using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionGP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("TestGeneration");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("BastienTest");
        }
	}
}
