using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InanimateEntity : MonoBehaviour {
	protected bool isEquipped;
	public BoxCollider2D pickupCollider;
	public Sprite thumbnail;
	[SerializeField]protected Character holder;
    protected float damage;

	public Character Holder {
		get {return holder;}
	}
	public virtual void Equip (Character user){
		isEquipped = true;
        holder = user;
		this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponentInChildren<CircleCollider2D>().enabled = false;
        pickupCollider.enabled = false;
		this.transform.parent = user.transform;
		transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

	public virtual void Unequip (){
        transform.position = holder.transform.position;
        holder.inventory[1] = null;
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        this.GetComponentInChildren<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().AddForce(holder.direction.normalized*50, ForceMode2D.Impulse);
		pickupCollider.enabled = true;
        this.transform.parent = null;
        holder = null;
        isEquipped = false;      
    }
	public abstract void Use (Character user);

    public virtual float GetDamage()
    {
        return damage;
    }
}
