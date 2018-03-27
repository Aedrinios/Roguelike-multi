using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : Weapon {

    private bool canBeUse;
    private static string[] powersTab = {"Stun", "Degats", "Heal"};
    public string powerSelected;
    public GameObject rayon;
    public int potionColorId;
    private int degat;

    // Use this for initialization
    void Start () {
        canBeUse = true;
        randomPotion();
        degat = 2;
    }
	
	// Update is called once per frame
	void Update () {
		if (!canBeUse) Destroy(this.gameObject); // est détruit si ne peut plus etre utilisé
	}

    public override void Use(Character user) // peut etre utilise 1 fois on la bois initialise le pouvoir de la potion
    {
        Debug.Log("Vie de base : " + user.health);
        Debug.Log("Couleur potion : " + potionColorId);
        Debug.Log("Pouvoir de la potion : " + powerSelected);
        base.Use(user);
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
       /* Pour unequip apres l'avoir use mais il manque les animations ce qui releve une erreur
        if (this == user.inventory[0].GetComponent<Potions>()) Unequip(0);
        else Unequip(1);*/
        canBeUse = false;
    }

    public void randomPotion()
    {
        var random = Random.Range(0, 2);
        powerSelected = powersTab[random]; // Selectionne un pouvoir random;

        // Supprime le power attribué
        var list = new List<string>(powersTab);
        list.Remove(powersTab[random]);
        powersTab = list.ToArray();

        Debug.Log(powersTab.Length);
        // Transfere le pouvoir associer à la couleur de la potion
        switch (potionColorId)
        {
            case (0):
                rayon.GetComponent<EffectZone>().potionPowers[potionColorId] = powerSelected;
                break;
            case (1):
                rayon.GetComponent<EffectZone>().potionPowers[potionColorId] = powerSelected;
                break;
            case (2):
                rayon.GetComponent<EffectZone>().potionPowers[potionColorId] = powerSelected;
                break;
        }
    }

    void powerStun(Character user)
    {
        user.stun = true;
        var coroutine = endStun(user);
        StartCoroutine(coroutine);
    }

    void powerHeal(Character user)
    {
        user.health += 2;
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
        rayon.GetComponent<EffectZone>().power = powerSelected; // Transfere le pouvoir selectionner
        Instantiate(rayon, position, new Quaternion());
        Destroy(this.gameObject);
    }

    public IEnumerator endStun(Character user)
    {
        yield return new WaitForSeconds(2);
        user.stun = true;
    }
}
