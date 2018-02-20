using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] healthHearts;
    public Image[] weaponSprites;
    public Sprite primaryWeaponEmpty;
    public Sprite secondaryWeaponEmpty;
    public Image[] primaryWeaponHearts;
    public Image[] secondaryWeaponHearts;

    public Sprite[] hearts;
    public Sprite[] greenHearts; // primary
    public Sprite[] blueHearts; // secondary
    private Character player;

    public bool IsUnused()
    {
        return player == null;
    }

    public void SetPlayer(Character player)
    {
        this.player = player;
        this.gameObject.SetActive(true);
    }

    public void RemovePlayer()
    {
        player = null;
        this.gameObject.SetActive(false);
        //Remove UI
    }

    public void SetHealth(int health)
    {
        for (int i = 0; i < 5; ++i)
            {
                if (i * 2 <= health - 2)                    // Full heart
                    healthHearts[i].sprite = hearts[0];
                else if (i * 2 >= health + health % 2)        // empty heart
                    healthHearts[i].sprite = hearts[2];
                else                                         // Half heart
                    healthHearts[i].sprite = hearts[1];
        }
    }

    public void armorHealth(int health)
    {
        // Mettre une condition pour slot 2 puis slot 1 puis joueur
        if (player.inventory[1] != null && player.inventory[1].GetComponent<Weapon>().armorPoints > 0) // Slot 2
        {
            for (int i = 0; i < 3; ++i)
            {
                if (i * 2 <= health - 2)                    // Full heart
                    secondaryWeaponHearts[i].sprite = blueHearts[0];
                else if (i * 2 >= health + health % 2)        // empty heart
                    secondaryWeaponHearts[i].sprite = blueHearts[2];
                else                                         // Half heart
                    secondaryWeaponHearts[i].sprite = blueHearts[1];
            }
        }
        else if (player.inventory[0] != null && player.inventory[0].GetComponent<Weapon>().armorPoints > 0) // Slot 1
        {
            for (int i = 0; i < 3; ++i)
            {
                if (i * 2 <= health - 2)                    // Full heart
                    primaryWeaponHearts[i].sprite = greenHearts[0];
                else if (i * 2 >= health + health % 2)        // empty heart
                    primaryWeaponHearts[i].sprite = greenHearts[2];
                else                                         // Half heart
                    primaryWeaponHearts[i].sprite = greenHearts[1];
            }
        }
    }


    public void ChangeWeapon(Character player, int slot)
    {
        weaponSprites[slot].enabled = true;
        weaponSprites[slot].sprite = player.inventory[slot].GetComponent<Weapon>().thumbnail;
        SetHealth(player.inventory[slot].GetComponent<Weapon>().armorPoints);
    }

    public void emptyFullInventory()
    {
        weaponSprites[0].sprite = primaryWeaponEmpty;
        weaponSprites[1].sprite = secondaryWeaponEmpty;
    }

    public void emptySlotInventory(int index)
    {
        if (index == 0)
        {
            weaponSprites[0].sprite = primaryWeaponEmpty;
        }
        else if (index == 1)
        {
            weaponSprites[1].sprite = secondaryWeaponEmpty;
        }
    }

    public void reactivateInventory()
    {
        for(int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].enabled = true;
        }
    }

}
