﻿using System.Collections;
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





    // Use this for initialization
    protected override void  Start()
    {
        speed = 10;
        attack = 2;
        health = 10;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        invincibility = new Timer(timeOfInvincibility, true);
        animator = this.GetComponent<Animator>();
        shootTimer = 1;
    }



    // Update is called once per frame
    void Update()
    {       
		if (hasTarget) {
			shootTimer -= Time.deltaTime;
			direction = target.transform.position - gameObject.transform.position;
			circleColliderRadius = gameObject.GetComponent<CircleCollider2D> ().radius = direction.magnitude * 2;

			if (circleColliderRadius < 10) {
				Move (-this.direction);
			} else if (circleColliderRadius > 30) {
				Move (this.direction);
			} else {
				//Idle ();
				if (shootTimer <= 0) {
					Idle ();
					shootTimer = 3;
					Shoot ();
				}      
			}
		} else 
			Idle ();

        if (health <= 0)
        {
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
            collision.gameObject.GetComponent<Character>().StartCoroutine("ReceiveHit");
            collision.gameObject.GetComponent<Character>().ReceiveHit(attack,gameObject);
        }
    }

    private void Shoot()
    {
        Vector3 toTarget = direction.normalized;
        GameObject go= Instantiate(arrow, gameObject.transform.position+toTarget, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = toTarget*15;
        go.GetComponent<InanimateEntity>().Holder = gameObject.GetComponent<AnimateEntity>();
        //go.transform.LookAt(target.transform);
    }
}
