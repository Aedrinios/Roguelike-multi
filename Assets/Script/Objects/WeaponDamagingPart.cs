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
                    if(other.name== "protectionShield")
                    {
                       other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(0, other.gameObject); 
                       SoundManager.playSound("shieldSound2"); //BRUIT DE BOUCLIER                  
                    }
                    else
                    {
                        switch (transform.parent.name)
                        {
                            case ("Sword"):
                                SoundManager.playSound("epeeSound"); // EPEE
                                break;

                            case ("Lance"):
                                SoundManager.playSound("lanceSound"); // LANCE
                                break;

                            case ("Stick"):
                                SoundManager.playSound("stickSound"); // BATON 
                                break;

                            case ("Boomerang"):
                                SoundManager.playSound("armeEpee"); // BOOMERANG
                                break;
                        }

                    }
                   
                }
            }
    }
}

