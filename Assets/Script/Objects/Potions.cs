using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Weapon {

    private bool canBeUse;
    private string[] powersTab = { "Stun", "Degats", "Heal"};
    public string power;

	// Use this for initialization
	void Start () {
        canBeUse = true;
        damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (!canBeUse) Destroy(this.gameObject); // est détruit si ne peut plus etre utilisé
	}

    public override void Use(Character user) // peut etre utilise 1 fois on la bois initialise le pouvoir de la potion
    {
        base.Use(user);
        canBeUse = false;
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

    // peut etre jeter 1 fois et elle créer une zone quand elle touche le soll (colider) tous ceux qui sont dedans sont touché par le pouvoir

}
