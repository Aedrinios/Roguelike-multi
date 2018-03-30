using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Weapon {

    private bool canBeUse;
    public string powerSelected;
    public GameObject rayon;
    public int potionColorId;
    private int degat;
    private PotionManager potionManager;
    
    // Use this for initialization
    void Start () {
        canBeUse = true;
        potionManager = FindObjectOfType<PotionManager>();
        degat = 2;
    }
	
	// Update is called once per frame
	void Update () {
		if (!canBeUse) StartCoroutine("die");
        if (powerSelected == "")
        {
            powerSelected = potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId];
        }
    }

    public override void Use(Character user) // peut etre utilise 1 fois on la bois initialise le pouvoir de la potion
    {
        Debug.Log("Pouvoir de la potion : " + powerSelected);
        base.Use(user);
        powerSelected = potionManager.GetComponent<PotionManager>().tabPowerPotions[potionColorId];
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
        //Pour unequip apres l'avoir use mais il manque les animations ce qui releve une erreur
        if (this == user.inventory[0].GetComponent<Potions>()) Unequip(0);
        else Unequip(1);
        canBeUse = false;
    }

    void powerStun(Character user)
    {
        user.stun = true;
        var coroutine = endStun(user);
        StartCoroutine(coroutine);
    }

    void powerHeal(Character user)
    {
        user.ReceiveHealt(1, user.gameObject);    // TO DO
        Debug.Log("heal fait");
    }

    void powerDegats(Character user)
    {
        user.ReceiveHit(degat, user.gameObject);
        Debug.Log("Vie : " + user.health);
    }

    public IEnumerator creationOfEffectZone()
    {
        yield return new WaitForSeconds(2);
        // Recuperer la position et creation de la zone collider à cette position
        var position = this.transform.position;
        rayon.GetComponent<EffectZone>().potionColorId = potionColorId; // Transfere l'id de la potion
        Instantiate(rayon, position, new Quaternion());
        Destroy(this.gameObject);
    }

    public IEnumerator endStun(Character user)
    {
        yield return new WaitForSeconds(2);
        user.stun = true;
    }

    public IEnumerator die()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
