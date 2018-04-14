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
        {
            if (other.tag.Equals("enemy"))
            {
                GameObject hit = other.gameObject;
                if (hit != weaponPart.Holder.gameObject)
                {
                    other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(weaponPart.Holder.GetAttack() * weaponPart.GetDamage(), weaponPart.Holder.gameObject);
                    if (other.gameObject.GetComponent<AnimateEntity>().getCurrentShield()!=null)
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
            if(other.tag == "Player" && weaponPart.Holder != other.gameObject.GetComponent<AnimateEntity>())
            {
                if (other.gameObject.GetComponent<AnimateEntity>().getCurrentShield() != null)
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
                            StartCoroutine(other.gameObject.GetComponent<AnimateEntity>().Slow(0.8f, 1));
                            break;

                        case ("Lance"):
                            SoundManager.playSound("lanceSound"); // LANCE
                            StartCoroutine(other.gameObject.GetComponent<AnimateEntity>().Stun(1));
                            break;

                        case ("Stick"):
                            SoundManager.playSound("stickSound"); // BATON
							other.gameObject.GetComponent<AnimateEntity>().FriendlyKnockBack(weaponPart.gameObject);
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

