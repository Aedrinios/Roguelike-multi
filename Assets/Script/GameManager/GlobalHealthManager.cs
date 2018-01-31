using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalHealthManager : MonoBehaviour {

    public int globalHealth;
    public Text globalHealthNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        globalHealthNumber.text = "x" +globalHealth;
		
	}
}
