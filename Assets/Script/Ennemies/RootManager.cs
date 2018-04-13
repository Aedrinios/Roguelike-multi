using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : AnimateEntity {

    private float timeCounter = 0;
    private GameObject target=null;
    private bool isGrow = false;
    private Ent ent;

	// Use this for initialization
	void Start () {
         base.Start();
         ent = FindObjectOfType<Ent>();
		 SoundManager.playSound("attackRootSound");
	}

    public override void ReceiveHit(int value, GameObject other)
    {
        if(other!= target)
            base.ReceiveHit(value, other);
    }

    // Update is called once per frame
    void Update () {
        Debug.Log("ROOT HEALTH : " + health);

        timeCounter += Time.deltaTime;

        if (health <= 0 || ent.health <= 0)
        {
            target.gameObject.GetComponent<AnimateEntity>().stun = false;
            target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            target.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            Destroy(gameObject);
        }
        else if (timeCounter >= 0.8)
        {
            animator.SetBool("hasGrow", true);
            isGrow = true;
        }

        if (isGrow)
        {
            if (target!=null)
            {
                target.gameObject.GetComponent<AnimateEntity>().ReceiveHit(2, target.gameObject);
            }
        }
		
	}

    public void setTarget(GameObject t)
    {
        target = t;
    }
}
