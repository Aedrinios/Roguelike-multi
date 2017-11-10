using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InanimateEntity : MonoBehaviour {
	protected bool isEquipped;
	public BoxCollider2D pickupCollider;
	protected Character holder;
	public Character Holder {
		get {return holder;}
	}
	public virtual void Equip (Character user){
		isEquipped = true;
        holder = user;
		this.GetComponentInChildren<SpriteRenderer>().enabled = false;
		pickupCollider.enabled = false;
		this.transform.parent = user.transform;
		transform.localPosition = Vector3.zero;

	}

	public virtual void Unequip (){
		isEquipped = false;
		pickupCollider.enabled = true;
        this.transform.parent = null;
        holder = null;
    }
	public abstract void Use (Character user);
}
