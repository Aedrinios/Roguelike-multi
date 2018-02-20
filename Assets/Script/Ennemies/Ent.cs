using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : AnimateEntity {

    public Shrub[] shrubs;
    public GameObject[] spells;
    public float timeBewteenSpells;

    private GameObject currentTarget;
    private List<GameObject> players;
    private int startHealth;

    //variables d'états
    private bool isPhase1;
    private bool isPhase2;
    private bool isPhase3;


    //utilitaires
    private bool doOnce = false;
    private float timeCounterSpells = 0;

    // Use this for initialization
    void Start () {
        base.Start();
        startHealth = health;
        timeCounterSpells = timeBewteenSpells;

        setActivePhase(1);

        health = 90; // TESTS
    }
	
	// Update is called once per frame
	void Update () {

        //DEBUGS
        Debug.Log("Ent health : " + health);
        Debug.Log("Phase Active : " + getActivePhase());

        //VARIABLE DE COMPTAGE DE TEMPS
        timeCounterSpells += Time.deltaTime;


        //1ère phase : Au dessus de 50%
        if (isPhase1) 
        {
            chooseRandomTarget();       //choisi une cible au hasard
            castRandomSpell();          //fait une attaque au harsard sur la cible choisi au hasard

            //CHANGEMENT DE PHASE
            if (health<=startHealth/2)
            {
                setActivePhase(2);
            }
        }
        //2ème phase : En dessous de 50%
        else if (isPhase2)
        {
            claimHealing();            //Les abrisseaux le soignent
            chooseRandomTarget();      //choisi une cible au hasard
            castRandomSpell();            //fait une attaque au harsard sur la cible choisi au hasard

            //CHANGEMENT DE PHASE
            if (getNumberOfShrubsAlive() == 0 || health == 200)
            {
                setActivePhase(1);
            }
            else if (health ==0)
            {
                setActivePhase(3);
            }
        }
        //3ème phase : Égale à 0
        else if (isPhase3)
        {
            //Tombe à terre

            //Si il est touché, meurt

            //Sinon se relève

            //fait apparaitre une porte pour le prochain niveau
        }
	}


    //Cette fonction choisi une nouvelle cible parmis tous les joueurs instanciés de façon aléatoire et la met dans currentTarget.
    void chooseRandomTarget()
    {
        int rdm = Mathf.RoundToInt(Random.Range(0, GameManager.instance.players.Count));
        currentTarget = GameManager.instance.players[rdm];
        Debug.Log("Ent Target : " +currentTarget);
    }

    void castRandomSpell()
    {
        if (timeCounterSpells >= 5)
        {
            int rdm = Mathf.RoundToInt(Random.Range(0, spells.Length));
            Instantiate(spells[rdm], currentTarget.transform.position, Quaternion.identity);

            timeCounterSpells = 0;
        }
    }

    void setAllShrubsCanBeDamaged(bool b)
    {
        for (int i = 0; i < shrubs.Length; i++)
        {
            shrubs[i].setCanBeDamaged(b);
        }
    }

    void claimHealing()
    {
        for (int i = 0; i < shrubs.Length; i++)
        {
            shrubs[i].healEnt();
        }
    }

    int getNumberOfShrubsAlive()
    {
        int res = 0;

        for (int i = 0; i < shrubs.Length; i++)
        {
            if (!shrubs[i].getIsDead())
            {
                res++;
            }
        }
        return res;
    }

    void setActivePhase(int i)
    {
        switch (i)
        {
            case 1:
                isPhase1 = true;
                isPhase2 = false;
                isPhase3 = false;

                setCanBeDamaged(true);                //vulnérable
                setAllShrubsCanBeDamaged(false);      //arbrisseaux invulnérables
                break;
            case 2:
                isPhase1 = false;
                isPhase2 = true;
                isPhase3 = false;

                setAllShrubsCanBeDamaged(true);        //Les arbrisseaux son vulnérable

                if (getNumberOfShrubsAlive() > 0)
                {
                    setCanBeDamaged(false);                //Invulnérable
                }
                break;
            case 3:
                isPhase1 = false;
                isPhase2 = false;
                isPhase3 = true;
                break;
        }
    }

    int getActivePhase()
    {
        if (isPhase1)
        {
            return 1;
        }
        else if (isPhase2)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
