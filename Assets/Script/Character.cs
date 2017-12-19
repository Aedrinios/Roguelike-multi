using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AnimateEntity {

	public InanimateEntity[] inventory;
	public ArrayList ground;
	[HideInInspector]
	public PlayerUI UI;

	protected override void Start () {
        base.Start();
		inventory = new InanimateEntity [2];
	// private Rigidbody2D rigidb;
	// private Animator animator;
    // private float primaryTimer;
    // private float secondaryTimer;

    // private bool isCarrying = false;
    // private InanimateEntity carriedObject;
    // primaryTimer=0;
    // secondaryTimer=0;
		ground = new ArrayList();
		rigidb = this.GetComponent<Rigidbody2D> ();
		
	}

	public override void ReceiveHit(int value, GameObject other){
		base.ReceiveHit(value,other);
		UI.SetHealth(health);
	}

    // public void Update() // déséquiper pour l'instant
    // {
    //    if(Input.GetKeyDown(KeyCode.T))
    //     {
    //         inventory[1].Unequip();
    // {    
    //    if(life<=0)
    //     {
    //         Debug.Log("YOU DIED!  (git gud)");
    //     }

    //     //ramassage puis lancer
    //     if (Input.GetButtonDown("Keyboard 2 interact")/*|| Input.GetButtonDown("Keyboard 1 interact")*/)
    //     {
    //         Debug.Log("gg");
    //         Interract();
    //     }

    //     //déséquipement arme 1
    //     if (Input.GetButton("Keyboard 2 primary"))
    //     {
    //         Debug.Log("gg");
    //         primaryTimer += Time.deltaTime;
    //         if (primaryTimer > 2 && inventory[0] != null)
    //             Carry(inventory[0]);
    //     }
    //     if (Input.GetButtonUp("Keyboard 2 primary"))
    //         primaryTimer = 0;

    //     //desequipement arme 2
    //     if (Input.GetButton("Keyboard 2 secondary"))
    //     {
    //         Debug.Log("gg");
    //         secondaryTimer += Time.deltaTime;
    //         if (secondaryTimer > 2 && inventory[1]!=null)
    //             Carry(inventory[1]);
    //     }
    //     if (Input.GetButtonUp("Keyboard 2 secondary"))
    //         secondaryTimer = 0;
    // }

	protected override void Die(){
		base.Die();
		isDying = true;
		animator.SetBool ("isDying", true);
	}

	public void Revive (){
		isDying = false;
		animator.SetBool ("isDying", false);
		health = 10;
		UI.SetHealth(health);
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
			UI.ChangeWeapon(this, slot);
		}	//No need to remove the item from the ground list, it's done in OnTriggerExit
	}

    // private void TryCarry()
    // {
    //     if (ground.Count != 0)
    //     {
    //         carriedObject = (InanimateEntity)ground[0];
    //         Carry(carriedObject);
    //     }   //No need to remove the item from the ground list, it's done in OnTriggerExit
    // }

    // public void Interract () {
    //     //Si un objet interractible est sous ses pieds, alors interragis avec.
    //     if (isCarrying)
    //     {
    //         Throw();
    //     }
    //     else
    //     {
    //         TryCarry();
    //     }
    // }

    // public void Carry(InanimateEntity Ientity)
    // {
    //     if(Ientity == inventory[0])
    //     {
    //         Ientity.Unequip(0);
    //     }
    //     if (Ientity == inventory[1])
    //     {
    //         Ientity.Unequip(1);
    //     }
    //     if (Ientity.tag == "Player" || Ientity.tag == "ennemi")
    //     {
    //         Ientity.GetComponent<AnimateEntity>().stun = true;
    //     }
    //     isCarrying = true;
    //     Debug.Log("blub");
    //     Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
    //     Ientity.pickupCollider.enabled = false;
    //     Ientity.GetComponentInChildren<SpriteRenderer>().enabled = true;
    //     Ientity.transform.parent = this.transform;
    //     Ientity.transform.localPosition = Vector3.zero;
    //     Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //     carriedObject = Ientity;
    // }

    // public void Throw()
    // {
    //     Debug.Log("bloub");
    //     if (isCarrying)
    //     {
    //         Debug.Log("blib");
    //         if (carriedObject.tag == "Player" || carriedObject.tag == "ennemi")
    //         {
    //             carriedObject.GetComponent<AnimateEntity>().stun = false;
    //         }
    //         carriedObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50, ForceMode2D.Impulse);
    //         //carriedObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
    //         carriedObject.pickupCollider.enabled = true;
    //         carriedObject.transform.parent = null;
    //         //carriedObject.transform.localPosition = Vector3.zero;
    //         //carriedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //         carriedObject = null;
    //         isCarrying = false;
    //     }
    // }

    public override void  Idle() {
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

	public void SetUI(PlayerUI UI){
		this.UI = UI;
		UI.SetPlayer(this);
	}
}
