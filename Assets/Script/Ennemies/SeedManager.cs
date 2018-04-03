using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour {

    public RootManager constrictRoot;
    private GameObject target=null;
    private Vector3 targetPos;
    private float timeCounter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter >= 5)
        {
            Destroy(gameObject);
        }

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.2f);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<AnimateEntity>().stun = true;
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            RootManager newRoot = Instantiate(constrictRoot, other.transform.position, Quaternion.identity);
            newRoot.setTarget(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void setTarget(GameObject t) {
        target = t;
        targetPos = t.transform.position;
    }
}
