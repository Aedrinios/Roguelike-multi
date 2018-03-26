using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectZone : MonoBehaviour {

    private string[] powersTab = { "Stun", "Degats", "Heal" };
    public string power;

    // Use this for initialization
    void Start () {
        StartCoroutine("die");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy" || other.tag == "Player")
        {
            Debug.Log("JE SUIS DANS LE TRIGGER DE EFFECT et de type player or ennemy");
        }
        // Appel du pouvoir selectionner
    }

    void powerStun(Character user)
    {

    }

    void powerHeal(Character user)
    {
        user.health += 2;
    }

    void powerDegats(Character user)
    {
        //Character.ReceiveHit(2, user);
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
