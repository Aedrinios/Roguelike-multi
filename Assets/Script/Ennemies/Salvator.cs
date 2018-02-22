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
   

    // Use this for initialization
    protected override void Start () {
        speed = 11;
        health = 10;
        attack = 0;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        baseColliderRadius = circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
        animator = this.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        //animator.SetBool("isAttacking", false);
        if (hasTarget) // player in sight
        {
            
            direction = target.transform.position - gameObject.transform.position;
            circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius = direction.magnitude;

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
        
        if (hasAlly)
        {
            ally.GetComponent<AnimateEntity>().setCanBeDamaged(false);
           
        }

        if (health <= 0)
        {
            ally.GetComponent<AnimateEntity>().setCanBeDamaged(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            hasTarget = true;
        }

        if(other.tag == "enemy" && !(other.gameObject.name.Contains("Salvator")))
        {
            if (!hasAlly)
            {
                if (other.gameObject.name.Contains("Summoner")) //if its a summoner --> shield 
                {
                    onSummoner = true;
                }
                ally = other.gameObject;
                hasAlly = true;

                Debug.Log("Je protège");
                
            }
            else
            {
                if ( onSummoner == false)
                {
                    if (other.gameObject.name.Contains("Summoner")) //if its a summoner --> shield 
                    {
                        onSummoner = true;
                        ally.GetComponent<AnimateEntity>().setCanBeDamaged(true);
                        ally = other.gameObject;
                    }

                }
            }
            animator.SetBool("isAttacking", true);
        }
      

    }

    public override void Move(Vector2 direction)
    {
        rigidb.velocity = direction.normalized * speed;
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", true);
    }

}
