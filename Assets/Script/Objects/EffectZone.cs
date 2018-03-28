using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectZone : MonoBehaviour {
    
    public string power;
    private int degat;
    private PotionManager potionManager;
    public int potionColorId;

    // Use this for initialization
    void Start () {
        StartCoroutine("die");
        degat = 1;
        potionManager = FindObjectOfType<PotionManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        power= potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId];
        if (other.tag == "enemy" || other.tag == "Player")
        {
            Debug.Log("JE SUIS DANS LE TRIGGER DE EFFECT et de type player or ennemy");
            Debug.Log("THIS IS MY POWER : " + power);
            var user = other.GetComponent<Character>();
            switch (power)
            {
                case ("Stun"):
                    powerStun(user);
                    break;
                case ("Degats"):
                    powerDegats(user);
                    break;
                case ("Heal"):
                    powerHeal(user);
                    break;
            }
        }
        // Appel du pouvoir selectionner
    }

    void powerStun(Character user)
    {
        user.stun = false;
        var coroutine = endStun(user);
        StartCoroutine(coroutine);
        Debug.Log("stun fait");
    }

    void powerHeal(Character user)
    {
        Debug.Log("heal fait");
        user.ReceiveHealt(1, user.gameObject);
        //user.health += 2;
    }

    void powerDegats(Character user)
    {
        user.ReceiveHit(degat, user.gameObject);
        Debug.Log("degat fait");
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }

    public IEnumerator endStun(Character user)
    {
        yield return new WaitForSeconds(2);
        user.stun = true;
    }
}
