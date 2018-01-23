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
        if (gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 4)
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
                        //collision.gameObject.GetComponent<Character>().ReceiveHit();
                        Destroy(gameObject);
                    }
                    else if (collision.tag == "ennemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().DecreaseHealth(5);
                        Destroy(gameObject);

                    }

                    break;
                case ("ennemy"):
                    if (collision.tag == "Player")
                    {
                        collision.gameObject.GetComponent<Character>().ReceiveHit();
                        collision.gameObject.GetComponent<Character>().DecreaseHealth(3);
                        Destroy(gameObject);

                    }
                    else if (collision.tag == "ennemy")
                    {
                        collision.gameObject.GetComponent<AnimateEntity>().DecreaseHealth(2);
                        Destroy(gameObject);

                    }
                    break;
            }
        }
    }

    public override void Use(Character user)
    {
        throw new System.NotImplementedException();
    }
}
