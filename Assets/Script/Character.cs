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
    private float primaryTimer;
    private float secondaryTimer;

    private bool isCarrying = false;
    private InanimateEntity carriedObject;

    void Start () {
        speed = 10;
        life = 10;
        attack = 1;
         primaryTimer=0;
    secondaryTimer=0;
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
       if(life<=0)
        {
            Debug.Log("YOU DIED!  (git gud)");
        }

        //ramassage puis lancer
        if (Input.GetButtonDown("Keyboard 2 interact")/*|| Input.GetButtonDown("Keyboard 1 interact")*/)
        {
            Debug.Log("gg");
            Interract();
        }

        //déséquipement arme 1
        if (Input.GetButton("Keyboard 2 primary"))
        {
            Debug.Log("gg");
            primaryTimer += Time.deltaTime;
            if (primaryTimer > 2 && inventory[0] != null)
                Carry(inventory[0]);
        }
        if (Input.GetButtonUp("Keyboard 2 primary"))
            primaryTimer = 0;

        //desequipement arme 2
        if (Input.GetButton("Keyboard 2 secondary"))
        {
            Debug.Log("gg");
            secondaryTimer += Time.deltaTime;
            if (secondaryTimer > 2 && inventory[1]!=null)
                Carry(inventory[1]);
        }
        if (Input.GetButtonUp("Keyboard 2 secondary"))
            secondaryTimer = 0;
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

    private void TryCarry()
    {
        if (ground.Count != 0)
        {
            carriedObject = (InanimateEntity)ground[0];
            Carry(carriedObject);
        }   //No need to remove the item from the ground list, it's done in OnTriggerExit
    }

    public void Interract () {
        //Si un objet interractible est sous ses pieds, alors interragis avec.
        if (isCarrying)
        {
            Throw();
        }
        else
        {
            TryCarry();
        }
    }

    public void Carry(InanimateEntity Ientity)
    {
        if(Ientity == inventory[0])
        {
            Ientity.Unequip(0);
        }
        if (Ientity == inventory[1])
        {
            Ientity.Unequip(1);
        }
        if (Ientity.tag == "Player" || Ientity.tag == "ennemi")
        {
            Ientity.GetComponent<AnimateEntity>().stun = true;
        }
        isCarrying = true;
        Debug.Log("blub");
        Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
        Ientity.pickupCollider.enabled = false;
        Ientity.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Ientity.transform.parent = this.transform;
        Ientity.transform.localPosition = Vector3.zero;
        Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        carriedObject = Ientity;
    }

    public void Throw()
    {
        Debug.Log("bloub");
        if (isCarrying)
        {
            Debug.Log("blib");
            if (carriedObject.tag == "Player" || carriedObject.tag == "ennemy")
            {
                carriedObject.GetComponent<AnimateEntity>().stun = false;
            }
            carriedObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50, ForceMode2D.Impulse);
            //carriedObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
            carriedObject.pickupCollider.enabled = true;
            carriedObject.transform.parent = null;
            //carriedObject.transform.localPosition = Vector3.zero;
            //carriedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject = null;
            isCarrying = false;
        }
    }

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
}
