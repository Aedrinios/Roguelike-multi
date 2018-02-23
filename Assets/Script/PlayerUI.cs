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

    public void armorHealth()
    {
        if (player.inventory[1] != null) // Slot 2
        {
            var health2 = player.inventory[1].GetComponent<Weapon>().armorPoints;

            if (health2 == 0) // empty heart
            {
                for (int i = 0; i < 3; ++i)
                {       
                    secondaryWeaponHearts[i].sprite = blueHearts[2];
                }
            }
            else
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (i * 2 <= health2 - 2)                    // Full heart
                        secondaryWeaponHearts[i].sprite = blueHearts[0];
                    else if (i * 2 >= health2 + health2 % 2)        // empty heart
                        secondaryWeaponHearts[i].sprite = blueHearts[2];
                    else                                         // Half heart
                        secondaryWeaponHearts[i].sprite = blueHearts[1];
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                secondaryWeaponHearts[i].sprite = blueHearts[2];
            }
        }

        if(player.inventory[0] != null) // Slot 1
        {
            var health1 = player.inventory[0].GetComponent<Weapon>().armorPoints;

            if (health1 == 0) // empty heart
            {
                for (int i = 0; i < 3; ++i)
                {       
                    primaryWeaponHearts[i].sprite = greenHearts[2];
                }
            }
            else
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (i * 2 <= health1 - 2)                    // Full heart
                        primaryWeaponHearts[i].sprite = greenHearts[0];
                    else if (i * 2 >= health1 + health1 % 2)        // empty heart
                        primaryWeaponHearts[i].sprite = greenHearts[2];
                    else                                         // Half heart
                        primaryWeaponHearts[i].sprite = greenHearts[1];
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                primaryWeaponHearts[i].sprite = greenHearts[2];
            }
        }
    }


    public void ChangeWeapon(Character player, int slot)
    {
        weaponSprites[slot].enabled = true;
        weaponSprites[slot].sprite = player.inventory[slot].GetComponent<Weapon>().thumbnail;
        armorHealth();// player.inventory[slot].GetComponent<Weapon>().armorPoints);
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
