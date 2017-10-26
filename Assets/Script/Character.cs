using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float speed = 10;
	public Vector2 direction;
	public InanimateEntity[] inventory;
	public ArrayList ground;
	public bool stun = false;
	private Rigidbody2D rigidb;

	void Start () {
		inventory = new InanimateEntity [2];
		ground = new ArrayList();
		rigidb = this.GetComponent<Rigidbody2D> ();
		this.GetComponent<SpriteRenderer>().color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
	}

	public void Move(Vector2 direction){
		this.direction = direction.normalized; 
		if (rigidb != null)
			if (!stun) 
				rigidb.velocity = direction * speed;
			else 
				rigidb.velocity = Vector2.zero;
	}

	public void ReceiveHit (){

	}

	public void InputAction (params object[] args) {	//pas fan de ce nom
		int item = (int) args[0]; 	
		if (inventory[item] == null)
			TryPickupItem(item);
		else
			inventory[item].Use(this);

	}

	public float GetRotation(){
		float rotation = Vector2.Angle(new Vector2(0,1), this.direction);
			if (direction.x < 0)
				rotation *= -1;
		return rotation ;
	}

	private void TryPickupItem(int slot){
		if(ground.Count != 0 ){
			inventory[slot] = (InanimateEntity) ground[0];
			inventory[slot].Equip(this);
		}	//No need to remove the item from the ground list, it's done in OnTriggerExit
	}

	public void Interract () {
		//Si un objet interractible est sous ses pieds, alors interragis avec.
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Equals("item")){
			ground.Add(other.GetComponent<InanimateEntity>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag.Equals("item")){
			ground.Remove(other.GetComponent<InanimateEntity>());
		}
	}
}
