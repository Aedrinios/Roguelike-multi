using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : AnimateEntity
{

    public CircleCollider2D targetChoice;
    public GameObject target;
    public bool hasTarget = false;
    public GameObject arrow;
    public bool canShoot;
    public float shootTimer;
    public float circleColliderRadius;
    public float baseColliderRadius;

    public RuntimeAnimatorController animControl;
    public RuntimeAnimatorController animControlArc;
	public bool damageAudioHasPlayed;
	public bool deathAudioHasPlayed;


    // Use this for initialization
    protected override void  Start()
    {
        base.Start();
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        baseColliderRadius=circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
        animator = this.GetComponent<Animator>();
        shootTimer = 1;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("8"))
        {
            this.GetComponent<AnimateEntity>().ReceiveHit(2,gameObject);
        }

        if (hasTarget) {
			shootTimer -= Time.deltaTime;
			direction = target.transform.position - gameObject.transform.position;
			circleColliderRadius = gameObject.GetComponent<CircleCollider2D> ().radius = direction.magnitude;

			if (circleColliderRadius < baseColliderRadius * 0.33f) {
				Move (-this.direction);
			}
            else if (circleColliderRadius > baseColliderRadius * 0.67f) {
				Move (this.direction);
			}
            else
            {
                rigidb.velocity = Vector2.zero;
				//Idle ();
				if (shootTimer <= 0) {
					Idle ();
					shootTimer = 2;
                    StartCoroutine("Shoot");
				}      
			}
		} else 
			Idle ();

        if (health <= 0)
        {
			if (deathAudioHasPlayed == false)
			{
				SoundManager.playSound("archerDying");
				deathAudioHasPlayed = true;
			}
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

    private IEnumerator Shoot()
    {
		SoundManager.playSound ("archerArrow");
        this.GetComponent<Animator>().runtimeAnimatorController = animControlArc as RuntimeAnimatorController;
        Vector3 toTarget = direction.normalized;
        GameObject go= Instantiate(arrow, gameObject.transform.position+toTarget, Quaternion.identity,transform);
        go.GetComponent<Arrow>().user = gameObject.GetComponent<AnimateEntity>();
        go.GetComponent<Arrow>().Holder = gameObject.GetComponent<AnimateEntity>();
        float sign = (direction.y < Vector3.right.y) ? 1.0f : -1.0f;
        go.transform.rotation=Quaternion.Euler(0,0,270-Vector3.Angle(Vector3.right,direction)*sign);
        go.GetComponent<Rigidbody2D>().velocity = toTarget*15;
        yield return new WaitForSeconds(0.75f);
        this.GetComponent<Animator>().runtimeAnimatorController = animControl as RuntimeAnimatorController;
    }

	public override void ReceiveHit(int value, GameObject other)
	{
		//joue le son de dégats
		if (damageAudioHasPlayed == false)
		{
			//play hit sound
			SoundManager.playSound("archerDamage");
			damageAudioHasPlayed = true;
		}
		base.ReceiveHit(value, other);
	}
}