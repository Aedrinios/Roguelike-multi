using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagingPart : MonoBehaviour
{
    public Weapon weaponPart;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {       
        if (!other.isTrigger)
            if (other.tag.Equals("enemy"))
            {
                GameObject hit = other.gameObject;
                if (hit != weaponPart.Holder.gameObject)
                {
                    Debug.Log(gameObject.name +" : hit for " +weaponPart.Holder.GetAttack() * weaponPart.GetDamage());
                    other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(weaponPart.Holder.GetAttack() * weaponPart.GetDamage(), other.gameObject);

                    switch (transform.parent.name)
                    {
                        case ("Sword"):
                            SoundManager.playSound("epeeSound");
                            break;

                        case ("Lance"):
                            SoundManager.playSound("lanceSound");
                            break;

                        case ("Stick"):
                            SoundManager.playSound("stickSound");
                            break;

                        case ("Boomerang"):
                            SoundManager.playSound("armeEpee");
                            break;

                        case ("protectionShield"):
                            SoundManager.playSound("armeEpee"); //BRUIT DE BOUCLIER
                            break;
                    }
                }
            }
    }
}

