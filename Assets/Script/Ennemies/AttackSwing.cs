using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwing : MonoBehaviour {

    private float timeCount = 0;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
		SoundManager.playSound("attackSwingSound");
    }
	
	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;
        if (timeCount >= 3)
        {
            Destroy(gameObject);
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
