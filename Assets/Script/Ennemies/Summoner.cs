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



    // Use this for initialization
    void Start()
    {
        speed = 3;
        attack = 1;
        life = 6;
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        summonTimer = 7;
    }



    // Update is called once per frame
    void Update()
    {

        if (hasTarget)
        {
            summonTimer -= Time.deltaTime;
            direction = target.transform.position - gameObject.transform.position;
            circleColliderRadius = gameObject.GetComponent<CircleCollider2D>().radius = direction.magnitude * 2;

            if (circleColliderRadius < 10)
                rigidb.velocity = -direction.normalized * speed;

            else if (circleColliderRadius > 30)
                rigidb.velocity = direction.normalized * speed;

            else
            {
                rigidb.velocity = Vector2.zero;
                if (summonTimer <= 0)
                {
                    summonTimer = 5;
                    Summon(target.transform.position - direction/2);
                }
            }
            if (life <= 0)
            {
                Destroy(gameObject);
            }
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

    private void Summon(Vector3 pos)
    {
        int no = (int)Random.Range(0, summon.Length);
        GameObject blub = Instantiate(summon[no], pos, Quaternion.identity);
    }
}
