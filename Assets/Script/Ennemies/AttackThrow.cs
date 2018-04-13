using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThrow : MonoBehaviour {

    private float timeCounter = 0;
    private Animator animator;
    private GameObject target;
    private bool done = false;
    public SeedManager seed;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		SoundManager.playSound("rootThrowSound");  
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 2.5)
        {
            Destroy(gameObject);
        }
        else if (timeCounter >= 1.3)
        {
            if (done == false)
            {
                SeedManager newSeed = Instantiate(seed, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
                newSeed.setTarget(target);
                done = true;
            }
        }
        else if (timeCounter >= 0.6)
        {
            animator.SetBool("hasSpawn", true);
        }
	}

    public void setTarget(GameObject t)
    {
        target = t;
    }
}
