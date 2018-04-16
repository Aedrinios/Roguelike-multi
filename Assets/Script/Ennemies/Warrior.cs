using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AnimateEntity {

    public CircleCollider2D targetChoice;
    public GameObject target;
    public bool hasTarget =false;
    public AudioSource audioSource2;
    public bool damageAudioHasPlayed;
    public bool deathAudioHasPlayed;


    // Use this for initialization
    protected override void  Start () {
		base.Start ();
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);
        damageAudioHasPlayed = false;
        deathAudioHasPlayed = false;
    }
	


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("7"))
        {
            this.GetComponent<AnimateEntity>().ReceiveHit(2, gameObject);
        }

        if (hasTarget)
        {
            direction = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<CircleCollider2D>().radius= direction.magnitude;
			Move (this.direction);
		} else
			Idle ();
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

    public override void ReceiveHit(int value, GameObject other)
    {
        //joue le son de dégats
        if (damageAudioHasPlayed == false)
        {
            //play hit sound
            SoundManager.playSound("warriorDamageSound");
            damageAudioHasPlayed = true;
        }
        base.ReceiveHit(value, other);
    }

    protected override void Die()
    {
        //joue le son de mort
        if (deathAudioHasPlayed == false)
        {
            //play hit sound
            SoundManager.playSound("mageMort2");
            deathAudioHasPlayed = true;
        }
        base.Die();
    }
}
