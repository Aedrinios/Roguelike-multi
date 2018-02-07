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
                    Debug.Log("hit");
                    other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(weaponPart.Holder.GetAttack() * weaponPart.GetDamage(), other.gameObject);
                    audioSource.PlayOneShot(SoundManager.getSound("armeEpee"), 1f);
                }
            }
    }

    




}

