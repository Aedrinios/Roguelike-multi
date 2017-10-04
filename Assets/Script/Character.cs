using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float speed = 10;

	private Rigidbody2D rigidb;

	void Start () {
		rigidb = this.GetComponent<Rigidbody2D> ();
		this.GetComponent<SpriteRenderer>().color = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
	}

	public void Move(Vector2 direction){
		if (rigidb != null)
			rigidb.velocity = direction * speed;
	}

	public void ReceiveHit (){

	}

	public void Attack () {

	}

	public void Interract () {

	}

	public void Shrink (params object[] parameters) {
		this.transform.localScale *= 0.9f;
	}

	public void Grow (params object[] parameters) {
		this.transform.localScale *= 1.1f;
	}
}
