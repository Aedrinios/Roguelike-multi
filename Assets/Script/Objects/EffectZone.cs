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
        degat = 3;
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
        var coroutine = user.Stun(0.75f);
        StartCoroutine(coroutine);
    }

    void powerHeal(Character user)
    {
        user.ReceiveHealt(3, user.gameObject);    // TO DO
    }

    void powerDegats(Character user)
    {
        user.ReceiveHit(degat, user.gameObject);
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(2.1f);
        Destroy(this.gameObject);
    }
}
