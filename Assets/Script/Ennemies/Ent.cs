using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : AnimateEntity {

    private GameObject currentTarget;
    private List<GameObject> players;
    private Shrub[] shrubs;
    private int startHealth;

    // Use this for initialization
    void Start () {
        startHealth = health;
        setCanBeDamaged(false); 
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Ent health : " + health);
        

        //1ère phase : Au dessus de 50%
        if (health > startHealth/2) 
        {
            //vulnérable

            //choisi une cible au hasard

            //fait une attaque au harsard sur la cible choisi au hasard
        }
        //2ème phase : En dessous de 50%
        else if (health < startHealth /2 && health > 0)
        {
            //Invulnérable

            //choisi une cible au hasard

            //fait une attaque au harsard sur la cible choisi au hasard
        }
        //3ème phase : Égale à 0
        else if (health <= 0)
        {
            //Tombe à terre

            //Si il est touché, meurt

            //Sinon se relève

            //fait apparaitre une porte pour le prochain niveau
        }
	}


    //Cette fonction choisi une nouvelle cible parmis tous les joueurs instanciés de façon aléatoire et la met dans currentTarget.
    void chooseNewTarget()
    {
        int rdm = Mathf.RoundToInt(Random.Range(0, GameManager.instance.players.Count));
        currentTarget = GameManager.instance.players[rdm];
        Debug.Log("Ent Target : " +currentTarget);
    }
}
