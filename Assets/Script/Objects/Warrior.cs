using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : AnimateEntity {
    public GameObject target;
    public WarriorDetectionZone detectionZone;

	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            Move(target.transform.position - gameObject.transform.position);
        }
        else 
            Idle();
	}

    protected override void Die(){
        Destroy(this.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           collision.gameObject.GetComponent<Character>().ReceiveHit(attack, this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           collision.gameObject.GetComponent<Character>().ReceiveHit(attack, this.gameObject);
        }
    }
}
