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
	private Animator animator;

	void Start () {
		inventory = new InanimateEntity [2];
		ground = new ArrayList();
		rigidb = this.GetComponent<Rigidbody2D> ();
		//this.GetComponent<SpriteRenderer>().color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		animator = this.GetComponent<Animator> ();
	}

	public void Move(Vector2 direction){	//Le if stun doit être retiré (ça doit être implémenté avec le pattern state)
		if (!stun) {
            this.direction = direction.normalized; //utile pour diriger les armes
			rigidb.velocity = direction * speed;
			animator.SetFloat ("directionX", direction.x);
			animator.SetFloat ("directionY", direction.y);
			animator.SetBool ("isMoving", true);
		} else {
			rigidb.velocity = Vector2.zero;
			animator.SetBool ("isMoving", false);
		}
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

	public void Idle() {
		animator.SetBool ("isMoving", false);
		rigidb.velocity = Vector3.zero;
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
