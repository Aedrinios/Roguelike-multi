using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Weapon {

    private bool canBeUse;
    private string[] powersTab = { "Stun", "Degats", "Heal"};
    public string power;
    public GameObject rayon;

	// Use this for initialization
	void Start () {
        canBeUse = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!canBeUse) Destroy(this.gameObject); // est détruit si ne peut plus etre utilisé
	}

    public override void Use(Character user) // peut etre utilise 1 fois on la bois initialise le pouvoir de la potion
    {
        base.Use(user);
        power = powersTab[Random.Range(1,3)]; // Selectionne un pouvoir random;

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

        canBeUse = false;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // Appel du pouvoir selectionner
    }

    public IEnumerator creationOfEffectZone()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("BLABLABLA AVANT LE CONTENU DE LA COROUTINE");
        // Recuperer la position et creation de la zone collider à cette position
        var position = this.transform.position;
        Instantiate(rayon);
        rayon.transform.position = position;
        CircleCollider2D sc = rayon.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        Debug.Log("BLABLABLA AVANT LE DESTROY DE LA POTION");
        Destroy(this.gameObject);
    }


}
