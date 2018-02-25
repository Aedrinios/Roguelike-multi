﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntHealthBar : MonoBehaviour {

    public GameObject hp;

    private float startScaleX;
    private Ent ent;

	// Use this for initialization
	void Start () {
        ent = FindObjectOfType<Ent>();
        startScaleX = hp.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        hp.transform.localScale = new Vector2(startScaleX / (ent.getStartHealth() / (float) ent.health), hp.transform.localScale.y);
        Debug.Log(startScaleX / (ent.getStartHealth() / (float) 150));
    }
}
