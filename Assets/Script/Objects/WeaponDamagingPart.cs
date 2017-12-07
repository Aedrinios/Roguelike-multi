using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagingPart : MonoBehaviour {
	public Weapon weaponPart;
	void OnTriggerEnter2Dz(BoxCollider2D other){
		//if (other.tag.Equals("Ennemi")){
			GameObject hit = other.gameObject;
			if ( hit != weaponPart.Holder.gameObject)
            {
                Debug.Log("hit");
                other.gameObject.GetComponent<AnimateEntity>().DecreaseHealth(weaponPart.Holder.GetAttack() * weaponPart.GetDamage());
				//weaponPart.OnCharacterHit(hit);
            }
		//}
	}
}
