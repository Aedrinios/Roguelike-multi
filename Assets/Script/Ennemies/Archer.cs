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





    // Use this for initialization
    void Start()
    {
        speed = 10;
        attack = 2;
        life = 10;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        shootTimer = 1;
    }



    // Update is called once per frame
    void Update()
    {       
        if(hasTarget)
        {
            shootTimer -= Time.deltaTime;
            direction = target.transform.position - gameObject.transform.position;
            circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius = direction.magnitude * 2;

            if (circleColliderRadius < 10)
                rigidb.velocity = -direction.normalized * speed;

            else if (circleColliderRadius > 30)
                rigidb.velocity = direction.normalized * speed;

            else
            {
                rigidb.velocity = Vector2.zero;
                if (shootTimer <= 0)
                {
                    shootTimer = 3;
                    Shoot();
                }      
            }
        }

        if (life <= 0)
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
            collision.gameObject.GetComponent<Character>().DecreaseHealth(attack);
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
