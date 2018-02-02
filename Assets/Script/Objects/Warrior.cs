using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AnimateEntity {

    public CircleCollider2D targetChoice;
    public GameObject target;
    public bool hasTarget =false;



	// Use this for initialization
	protected override void  Start () {
        speed = 6;
        attack = 4;
        health = 10;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);
        rigidb = GetComponent<Rigidbody2D>();
    }
	


	// Update is called once per frame
	void Update () {
        if(hasTarget)
        {
            direction = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<CircleCollider2D>().radius= direction.magnitude;
            rigidb.velocity = direction.normalized * speed;
        }
        
        if(health<=0)
        {
            Destroy(gameObject);
        }
	}

    public override void Move(Vector2 direction)
    {
        throw new System.NotImplementedException();
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
