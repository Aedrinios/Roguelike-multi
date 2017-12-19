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
		ground = new ArrayList();
		rigidb = this.GetComponent<Rigidbody2D> ();
		
	}

	public override void ReceiveHit(int value, GameObject other){
		base.ReceiveHit(value,other);
		UI.SetHealth(health);
	}

    public void Update() // déséquiper pour l'instant
    {
       if(Input.GetKeyDown(KeyCode.T))
        {
            inventory[1].Unequip();
        }
    }

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

	public void SetUI(PlayerUI UI){
		this.UI = UI;
		UI.SetPlayer(this);
	}
}
