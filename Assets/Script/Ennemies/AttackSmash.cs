using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSmash : MonoBehaviour {

    private float timeCount = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;
        if (timeCount >= 1.2)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<AnimateEntity>().ReceiveHit(2, other.gameObject);
        Debug.Log("ATTACK SMASH");
    }
}
