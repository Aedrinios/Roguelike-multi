using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvator : AnimateEntity {

    public CircleCollider2D targetChoice; //detection zone
    public GameObject target; //his target is an enemy
    public GameObject ally; //his target is an ally
    public bool hasTarget = false;
    public bool hasAlly = false;
    public bool onSummoner = false;
    public float circleColliderRadius;
    public float baseColliderRadius;
    public bool damageAudioHasPlayed;
    public bool deathAudioHasPlayed;


    // Use this for initialization
    protected override void Start () {
		base.Start ();
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        baseColliderRadius = circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
        animator = this.GetComponent<Animator>();
		//animator.SetBool ("isAttacking", false);

    }
	
	// Update is called once per frame
	void Update () {


        
        if (hasTarget) // player in sight
        {
            
            direction = target.transform.position - gameObject.transform.position;
			circleColliderRadius = gameObject.GetComponent<CircleCollider2D> ().radius = direction.magnitude;	

            if (circleColliderRadius < baseColliderRadius * 0.25f)
            {
                Move(-this.direction);
            }
            else if (circleColliderRadius > baseColliderRadius * 0.67f)
            {
                Idle();
            }
        }
        else
            Idle();

        

        if (health <= 0)
        {   ally.GetComponent<AnimateEntity>().setCanBeDamaged(true);
            //joue le son de mort
            if (deathAudioHasPlayed == false)
            {
                SoundManager.playSound("salvatorDying");
                deathAudioHasPlayed = true;
            }
            
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            hasTarget = true;
        }

        if(other.gameObject.tag == "enemy" && !(other.gameObject.name.Contains("Salvator")))
        {
            if (!hasAlly)
            {
                if (other.gameObject.name.Contains("Summoner")) //if its a summoner --> shield 
                {
                    onSummoner = true;
                }
                ally = other.gameObject;
				hasAlly = true;                
            }
            else
            {
                if (!onSummoner)
                {
                    if (other.gameObject.name.Contains("Summoner")) //if its a summoner --> shield 
                    {
                        onSummoner = true;
                        ally.GetComponent<AnimateEntity>().setCanBeDamaged(true);
                        ally = other.gameObject;
                    }

                }
            }

            if (ally.GetComponent<AnimateEntity>().getCanBeDamaged()) { 
                //SoundManager.playSound("putShield");
                animator.SetBool("isAttacking", true);
		        yield return new WaitForSeconds (0.5f);
		        ally.GetComponent<AnimateEntity>().setCanBeDamaged(false);
		        animator.SetBool ("isAttacking", false);
            }
        }
    }

    public override void Move(Vector2 direction)
    {
        rigidb.velocity = direction.normalized * speed;
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", true);
    }

    public override void ReceiveHit(int value, GameObject other)
    {
        //joue le son de dégats
        if (damageAudioHasPlayed == false)
        {
            //play hit sound
            SoundManager.playSound("salvatorDamage");
            damageAudioHasPlayed = true;
        }
        base.ReceiveHit(value, other);
    }

}
