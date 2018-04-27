using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSmash : MonoBehaviour {

    private float timeCount = 0;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        SoundManager.playSound("attackSmashSound");    
	}
	
	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;
        if (timeCount >= 2)
        {
            Destroy(gameObject);
        }
        else if (timeCount >= 1.2f)
        {
            animator.SetBool("hasSmashed", true);
        }
        else if(timeCount >= 0.5f)
        {
            animator.SetBool("isSpawned", true);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(2, other.gameObject);
        }
        
    }
}
