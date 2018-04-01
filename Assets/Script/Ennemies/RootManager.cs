using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour {

    private Animator animator;
    private float timeCounter = 0;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        timeCounter += Time.deltaTime;

        if (timeCounter >= 0.8)
        {
            animator.SetBool("hasGrow", true);
        }
		
	}
}
