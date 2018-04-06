using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : AnimateEntity {

    private Animator animator;
    private float timeCounter = 0;
    private GameObject target=null;
    private bool isGrow = false;

	// Use this for initialization
	void Start () {
        base.Start();
        animator = GetComponent<Animator>();
		SoundManager.playSound("attackRootSound");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("ROOT HEALTH : " + health);

        timeCounter += Time.deltaTime;

        if (health <= 0)
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
