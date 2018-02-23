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

    // Use this for initialization
    protected override void Start()
    {
        speed = 3;
        attack = 1;
        health = 6;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        baseColliderRadius=circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
        summonTimer = 7;
		animator = this.GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
	{
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
			if (health <= 0) 
			{
				Destroy (gameObject);
			}
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
		
		summonTimer = 5;
		int no = (int)Random.Range(0, summon.Length);
		GameObject blub = Instantiate(summon[no], target.transform.position - direction / 2, Quaternion.identity);
		animator.SetBool ("isAttacking", false);
	}
}
