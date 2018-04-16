using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : AnimateEntity
{

    public CircleCollider2D targetChoice;
    public GameObject target;
    public bool hasTarget = false;
    public float summonTimer;
    public GameObject[] summon;
    public float circleColliderRadius;
    public float baseColliderRadius;
    public bool deathAudioHasPlayed;
    public bool damageAudioHasPlayed;

    // Use this for initialization
    protected override void Start()
    {
		base.Start ();
        summonTimer = 2;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        baseColliderRadius=circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
		animator = this.GetComponent<Animator>();
        deathAudioHasPlayed = false;
        damageAudioHasPlayed = false;
    }

    // Update is called once per frame
    void Update()
	{
        if (Input.GetKeyDown("6"))
        {
            this.GetComponent<AnimateEntity>().ReceiveHit(2, gameObject);
        }
        if (this.IsDying())
        {
            //joue le son de mort
            if (deathAudioHasPlayed == false)
            {
                SoundManager.playSound("mageMort2");
                deathAudioHasPlayed = true;
            }
        }

        if (hasTarget) 
		{
			summonTimer -= Time.deltaTime;
			direction = target.transform.position - gameObject.transform.position;
			circleColliderRadius = gameObject.GetComponent<CircleCollider2D> ().radius = direction.magnitude;


			if (circleColliderRadius < baseColliderRadius*0.25)
				Move (-this.direction);
			else if (circleColliderRadius > baseColliderRadius*0.75f)
				Move (this.direction);
			else 
			{
				
				if (summonTimer <= 0) {
					animator.SetBool ("isAttacking", true);
				}
			}
			/*if (health <= 0) 
			{
				Destroy (gameObject);
			}*/
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
        if (other.tag == "Player")
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
		
    public void Summon()
    {
		
		summonTimer = 4;
		int no = (int)Random.Range(0, summon.Length);
        SoundManager.playSound("mageInvocation4");
        GameObject blub = Instantiate(summon[no], target.transform.position - direction / 2, Quaternion.identity);
        blub.transform.position = new Vector3(blub.transform.position.x, blub.transform.position.y, 0);
		animator.SetBool ("isAttacking", false);
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

    public override void ReceiveHit(int value, GameObject other)
    {
        //joue le son de dégats
        if (damageAudioHasPlayed == false)
        {
            //play hit sound
            SoundManager.playSound("ghoulDegat1");
            damageAudioHasPlayed = true;
        }
        base.ReceiveHit(value, other);
    }
}
