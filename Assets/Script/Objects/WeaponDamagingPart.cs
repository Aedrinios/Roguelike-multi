using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamagingPart : MonoBehaviour {
	public Weapon weaponPart;
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Equals("Player")){
			Character hit = other.GetComponent<Character>();
			if ( hit != weaponPart.Holder)
				weaponPart.OnCharacterHit(hit);
		}
	}
}
