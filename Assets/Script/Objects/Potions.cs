using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Weapon {

    public string powerSelected;
    public GameObject rayon;
    public int potionColorId;
    private int degat;
    private PotionManager potionManager;
    
    // Use this for initialization
    void Start () {
        potionManager = FindObjectOfType<PotionManager>();
        degat = 2;
    }
	
	// Update is called once per frame
	void Update () {


        if (powerSelected == "")
        {
            Debug.Log(potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId]);
            powerSelected = potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId];
        }
    }

    public override void Use(Character user) // peut etre utilise 1 fois on la bois initialise le pouvoir de la potion
    {
        Debug.Log("Pouvoir de la potion : " + powerSelected);
        SoundManager.playSound("DrinkingPotion");
        base.Use(user);
        powerSelected = potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId];

        if (user.inventory[0] != null)
        {
            if (this == user.inventory[0].GetComponent<Potions>())
            {
                user.UI.emptySlotInventory(0);
                Unequip(0);
            }

        }
        else if (user.inventory[1] != null)
        {
            if (this == user.inventory[1].GetComponent<Potions>())
            {
                user.UI.emptySlotInventory(1);
                Unequip(1);
            }
        }
        switch (powerSelected)
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
        StartCoroutine("die");
    }

    void powerStun(Character user)
    {
        var coroutine = user.Stun(1.5f);
        StartCoroutine(coroutine);
    }

    void powerHeal(Character user)
    {
        user.ReceiveHealt(4, user.gameObject);   
        Debug.Log("heal fait");
    }

    void powerDegats(Character user)
    {
        user.ReceiveHit(degat, user.gameObject);
        Debug.Log("Vie : " + user.health);
    }

    public IEnumerator creationOfEffectZone()
    {
        yield return new WaitForSeconds(1);
        SoundManager.playSound("BreakingPotion");
        // Recuperer la position et creation de la zone collider à cette position
        var position = this.transform.position;
        rayon.GetComponent<EffectZone>().potionColorId = potionColorId; // Transfere l'id de la potion
        Instantiate(rayon, position, new Quaternion());
        Destroy(this.gameObject);
    }

    public IEnumerator die()
    {
        foreach(SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
        yield return new WaitForSeconds(1.6f);
        Debug.Log("trkl");
        
        Destroy(this.gameObject);

    }
}
