using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : AnimateEntity {

    public float timeBetweenHeals;

    private int startHealth;
    private float timeCounter;
    private AudioSource audioSource;
    private bool done = false;

    //variables d'états
    private bool isPhase1;
    private bool isPhase2;
    private bool isPhase3;
    private bool isPhase4;

    // Use this for initialization
    void Start () {
        base.Start();
        scaleMultiplier = 2f;
        startHealth = health;
        setActivePhase(1);
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        
        timeCounter = timeBetweenHeals;
    }
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;


        if (isPhase1)
        {

            //CHANGEMENT DE PHASE
            if (health <= startHealth * 0.75)
            {
                setActivePhase(2);
            }
        }
        else if (isPhase2)
        {

            //CHANGEMENT DE PHASE
            if (health <= startHealth * 0.5)
            {
                setActivePhase(3);
            }
        }
        else if (isPhase3)
        {

            //CHANGEMENT DE PHASE
            if (health <= startHealth * 0.25)
            {
                setActivePhase(4);
            }
        }
        else if (isPhase4)
        {

            //CHANGEMENT DE PHASE
            if (health <= 0)
            {
                //Mort
                Die();
            }
        }
    }

    public void healEnt()
    {
        if (!done)
        {
            audioSource.Play();
            done = true;
        }

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

    void setActivePhase(int i)
    {
        switch (i)
        {
            case 1:
                isPhase1 = true;
                isPhase2 = false;
                isPhase3 = false;
                isPhase4 = false;

                animator.SetBool("isPhase1", true);
                break;
            case 2:
                isPhase1 = false;
                isPhase2 = true;
                isPhase3 = false;
                isPhase4 = false;

                animator.SetBool("isPhase2", true);
                animator.SetBool("isPhase1", false);
                break;
            case 3:
                isPhase1 = false;
                isPhase2 = false;
                isPhase3 = true;
                isPhase4 = false;

                animator.SetBool("isPhase3", true);
                break;
            case 4:
                isPhase1 = false;
                isPhase2 = false;
                isPhase3 = false;
                isPhase4 = true;

                animator.SetBool("isPhase4", true);
                break;

        }
    }

    public void stopPlaySound()
    {
        audioSource.Stop();
        done = false;
    }
}
