using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AnimateEntity {

    public CircleCollider2D targetChoice;
    public GameObject target;
    public bool hasTarget =false;
    public AudioSource audioSource2;



	// Use this for initialization
	protected override void  Start () {
        speed = 6;
        attack = 4;
        health = 10;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);
    }
	


	// Update is called once per frame
	void Update () {
        if(hasTarget)
        {
            direction = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<CircleCollider2D>().radius= direction.magnitude;
			Move (this.direction);
		} else
			Idle ();
        
        
        if(health<=0)
        {
            Debug.Log("warrior meurt");
            //audioSource2.PlayOneShot(sounds[0]);
            //audioSource2.PlayOneShot(getSound("guerrierMort1"));
            Destroy(gameObject);
        }
	}

    public override void Move(Vector2 direction)
    {
		rigidb.velocity = direction.normalized * speed;
		animator.SetFloat ("directionX", direction.x);
		animator.SetFloat ("directionY", direction.y);
		animator.SetBool ("isMoving", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            target = other.gameObject;
            hasTarget = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
			collision.gameObject.GetComponent<Character>().ReceiveHit(attack,gameObject);
        }
    }
}
