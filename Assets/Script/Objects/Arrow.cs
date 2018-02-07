using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : InanimateEntity {

    public AnimateEntity user;

    private void Start()
    {
        user = gameObject.GetComponent<InanimateEntity>().Holder;
    }

    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InanimateEntity>() != user)
        {
            switch (holder.tag)
            {
                case ("Player"):
                    if (collision.tag == "Player")
                    {
                        collision.gameObject.GetComponent<Character>().ReceiveHit(0, collision.gameObject);
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "enemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().ReceiveHit(5, collision.gameObject);
                        Destroy(gameObject);
                    }
                    break;
                case ("enemy"):
                    if (collision.tag == "Player")
                    {
                        collision.gameObject.GetComponent<Character>().ReceiveHit(3, collision.gameObject);
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "enemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().ReceiveHit(2, collision.gameObject);
                        Destroy(gameObject);
                    }
                    break;
                default: break;
            }
        }
    }

    public override void Use(Character user)
    {
        throw new System.NotImplementedException();
    }
}
