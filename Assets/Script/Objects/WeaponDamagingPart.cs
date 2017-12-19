using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagingPart : MonoBehaviour {
	public Weapon weaponPart;
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == weaponPart.Holder.gameObject)
			return;
		if (other.tag == "Enemie"){
            Debug.Log("hit enemie");
			other.gameObject.GetComponent<AnimateEntity>().ReceiveHit((int) (weaponPart.Holder.GetAttack() + weaponPart.damages), this.gameObject);
		}
		if (other.tag == "Player") {
            Debug.Log("hit player");
			other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(0 , this.gameObject);
		}
	}
}
