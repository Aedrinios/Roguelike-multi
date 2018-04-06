using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : AnimateEntity {

    public CircleCollider2D targetChoice;
    public BoxCollider2D shield;
    public BoxCollider2D damageTaken;
    public GameObject target;
    public GameObject secondTarget;
    public bool hasTarget = false;
    public float circleColliderRadius;
    public float baseColliderRadius;
    public bool deathAudioHasPlayed;
    public bool damageAudioHasPlayed;
    public bool targetAudioHasPlayed;
    public bool animationHasPlayed; 


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
        if (health <= 0)
        {
            hasTarget = false;
            rigidb.velocity = direction.normalized * 0;
        }

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

        if (Input.GetKeyDown("6"))
        {
            ReceiveHit(5,gameObject);
        }

        if (Input.GetKeyDown("7"))
        {
            Die(); 
        }


    }

    public override void Move(Vector2 direction)
    {
       if (animationHasPlayed == false)
       {
            animationHasPlayed = true;
        }
       
        if (targetAudioHasPlayed == false)
        {
            SoundManager.playSound("targetArmor");
            targetAudioHasPlayed = true;
        }
       
        rigidb.velocity = direction.normalized * speed;
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", true);
        Debug.Log(animator.GetBool("isMoving"));


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
            hasTarget = true;
            target.GetComponent<Character>().StartCoroutine("targetColor");
        }
        if (other.gameObject != target && other.tag == "Player")
        {
            secondTarget = other.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasTarget && other.tag == "Player" && !other.gameObject.GetComponent<Character>().getIsDead())
        {
            hasTarget = true;
            target = other.gameObject;
            target.GetComponent<Character>().StartCoroutine("targetColor");
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
            hasTarget = false;
            gameObject.GetComponent<CircleCollider2D>().radius = 10;
        }

    }
    public override void ReceiveHit(int value, GameObject other)
    {
        
        if (secondTarget!=null && target != other)
        {
            //joue le son de dégats
            if (damageAudioHasPlayed == false)
            {
                //play hit sound
                SoundManager.playSound("keeperDamage");
                damageAudioHasPlayed = true;
            }
            base.ReceiveHit(value, other);
            target = secondTarget;
            target.GetComponent<Character>().StartCoroutine("targetColor");
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
            SoundManager.playSound("keeperMort");
            deathAudioHasPlayed = true;
        }
        base.Die();
    }

}
