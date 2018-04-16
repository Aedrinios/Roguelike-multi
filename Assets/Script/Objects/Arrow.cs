using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : InanimateEntity {

    public AnimateEntity user;

    private void Start()
    {
        user = transform.parent.GetComponent<AnimateEntity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InanimateEntity>() != user && !collision.isTrigger)
        {
            switch (user.tag)
            {
                case ("Player"):
                    if (collision.tag == "Player")
                    {
                        collision.gameObject.GetComponent<Character>().ReceiveHit(0, Holder.gameObject);
                        StartCoroutine(collision.gameObject.GetComponent<AnimateEntity>().Stun(0.8f));
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "enemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().ReceiveHit(5, Holder.gameObject);
                        Destroy(gameObject);
                    }
                    else if (collision.tag=="Wall")
                    {
                        Destroy(gameObject);
                    }
                        break;
                case ("enemy"):
                    if (collision.tag == "Player")
                    {
                        collision.gameObject.GetComponent<Character>().ReceiveHit(3, Holder.gameObject);
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "enemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().ReceiveHit(1, Holder.gameObject);
                        StartCoroutine(collision.gameObject.GetComponent<AnimateEntity>().Stun(0.4f));
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "Wall")
                    {
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
