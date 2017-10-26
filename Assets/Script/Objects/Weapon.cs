using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InanimateEntity {

public float pushPower;
	public Animator animator;
	public Rigidbody2D rigid;
	public override void Use(Character user) {
		CopyUserRotation(user);
		LaunchAnimation();
	}

	private void CopyUserRotation (Character user) { 	//Useless pour le moment
		this.transform.rotation = Quaternion.Euler(0,0, user.GetRotation() * -1);
	}

	private void LaunchAnimation () {
		if (!animator.GetBool("attack")){
			transform.localPosition = Vector3.zero;
			GetComponentInChildren<SpriteRenderer>().enabled = true;
			animator.SetBool("attack", true);
			holder.stun = true;
		}
	}

	public void EndAnim(){
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		holder.stun = false;
		Debug.Log("wut");
	}

	public void OnCharacterHit(Character other){
		Vector2 push = (Vector2) (other.transform.position - holder.transform.position).normalized;
		other.GetComponent<Rigidbody2D>().AddForce( push * pushPower);
	}

}
