using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : AnimateEntity {

    public float timeBetweenHeals;

    private float timeCounter;

	// Use this for initialization
	void Start () {
        base.Start();
        timeCounter = timeBetweenHeals;
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;

        //Si la vie de l'arbsiseau tombe en dessous de zero
        if (health<=0)
        {
            //Mort
            Die();
        }
	}

    public void healEnt()
    {
        //TODO : Faire l'animator du shrub en incluant un état "healing" et un paramètre booleen "isHealing"
        //animator.SetBool("isHealing", true);
        if (timeCounter >= timeBetweenHeals && transform.parent.gameObject.GetComponent<Ent>().health < 200)
        {
            Debug.Log(name + " : Healing ent");
            transform.parent.gameObject.GetComponent<Ent>().health += 1;

            timeCounter = 0;
        }
    }

    void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
