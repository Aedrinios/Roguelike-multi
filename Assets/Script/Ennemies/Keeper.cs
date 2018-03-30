using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : AnimateEntity {

    public CircleCollider2D targetChoice;
    public BoxCollider2D shield;
    public BoxCollider2D damageTaken;
    public GameObject target;
    public GameObject secondTarget;
    public bool targetIsDead;
    public bool hasTarget = false;
    public float circleColliderRadius;
    public float baseColliderRadius;
    public bool deathAudioHasPlayed;
    public bool damageAudioHasPlayed;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        speed = 4;
        attack = 3;
        health = 20;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);

        //var ShieldTest = GameObject.FindGameObjectWithTag("shield");
    }

    // Update is called once per frame
    void Update()
    {

        if (hasTarget)
        {
            Debug.Log("update:"+target);
            direction = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<CircleCollider2D>().radius = direction.magnitude;
            
            Move(this.direction);
          
        }
        else
        {
            Idle();
        }
        

    }

    public override void Move(Vector2 direction)
    {
        
        rigidb.velocity = direction.normalized * speed;
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", true);

        shield.offset = new Vector2(direction.normalized.x, direction.normalized.y);
        if (direction.normalized.x < (-0.5) || direction.normalized.x > 0.5)
        {
            shield.size = new Vector2(1, 3);
        }
        else
        {
            shield.size = new Vector2(3, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTarget && other.tag == "Player")
        {   
            target = other.gameObject;
            targetIsDead = false;
            hasTarget = true;
        }
        if (other.gameObject != target && other.tag == "Player")
        {
            secondTarget = other.gameObject;
        }
        if (other.gameObject == target && targetIsDead)
        {
            target = null;        
            hasTarget = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().ReceiveHit(attack, gameObject);
        }
        if (collision.gameObject.GetComponent<Character>().getIsDead())
        {
            targetIsDead = true;
            if (secondTarget != null)
            {
                target = secondTarget;
                secondTarget = null;
                targetIsDead = false;
            }
            else
            {
                hasTarget = false;
            }

            gameObject.GetComponent<CircleCollider2D>().radius = 10;
        }

    }
    public override void ReceiveHit(int value, GameObject other)
    {
        
        if(secondTarget!=null && target != other)
        {
            base.ReceiveHit(value, other);
            target = secondTarget;
            secondTarget = null;
        }
        else
        {
            base.ReceiveHit(0, other);
        }
        
    }

    protected override void Die()
    {
        //joue le son de mort
        if (deathAudioHasPlayed == false)
        {
            deathAudioHasPlayed = true;
        }
        base.Die();
    }

}
