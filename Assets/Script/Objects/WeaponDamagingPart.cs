using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagingPart : MonoBehaviour
{
    public Weapon weaponPart;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("enemy"))
        {
            GameObject hit = other.gameObject;
            if (hit != weaponPart.Holder.gameObject)
            {
                Debug.Log("hit");
                other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(weaponPart.Holder.GetAttack() * weaponPart.GetDamage(), null);
            }
        }
    }
}

