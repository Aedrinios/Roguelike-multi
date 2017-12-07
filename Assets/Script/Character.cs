using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AnimateEntity {

	public float speed = 10;
	public Vector2 direction;
	public InanimateEntity[] inventory;
	public ArrayList ground;
	private Rigidbody2D rigidb;
	private Animator animator;

	void Start () {
        speed = 10;
        life = 10;
        attack = 1;
		inventory = new InanimateEntity [2];
		ground = new ArrayList();
		rigidb = this.GetComponent<Rigidbody2D> ();
		//this.GetComponent<SpriteRenderer>().color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		animator = this.GetComponent<Animator> ();
	}

	public override void Move(Vector2 direction){	//Le if stun doit être retiré (ça doit être implémenté avec le pattern state)
    	this.direction = direction.normalized; 
		rigidb.velocity = direction * speed;
		animator.SetFloat ("directionX", direction.x);
		animator.SetFloat ("directionY", direction.y);
		animator.SetBool ("isMoving", true);
	}

    public void Update() // déséquiper pour l'instant
    {
       if(Input.GetKeyDown(KeyCode.T))
        {
            inventory[1].Unequip();
        }    
       if(life<=0)
        {
            Debug.Log("YOU DIED!  (git gud)");
        }
    }
	public IEnumerator ReceiveHit (){
        if(canBeDamaged)
        {
            canBeDamaged = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);          
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            canBeDamaged = true;
        }
	}

	public void InputAction (params object[] args) {	//pas fan de ce nom
		int item = (int) args[0]; 	
		if (inventory[item] == null)
			TryPickupItem(item);
		else
			inventory[item].Use(this);
		Debug.Log("pressed");

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
