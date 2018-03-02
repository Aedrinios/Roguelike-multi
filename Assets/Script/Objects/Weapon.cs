using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InanimateEntity {

public float pushPower;
	public Animator animator;
	public Rigidbody2D rigid;
	[Range(0,6)]
	public int armorPoints;

    private void Start()
    {
        damage = 5;
    }

    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude < 0.1)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public override void Use(Character user) {
		CopyUserRotation(user);
		LaunchAnimation();
	}

	private void CopyUserRotation (Character user) { 	//Useless pour le moment
		this.transform.rotation = Quaternion.Euler(0,0, user.GetRotation() * -1);
	}

	private void LaunchAnimation () {
        if (!animator.GetBool("attack"))
        {
            transform.localPosition = Vector3.zero;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            animator.SetBool("attack", true);
            holder.stun = true;
            PlaySound();
        }
	}

	public void EndAnim(){
        for (int i = 0; i < GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
        }
		holder.stun = false;
	}

    private void PlaySound()
    {
        switch (gameObject.name)
        {
            case ("Sword"):
                SoundManager.playSound("armeEpeeVide");
                break;

            case ("Stick"):
                SoundManager.playSound("armeEpeeVide"); //FINIR
                break;
            case ("Lance"):
                SoundManager.playSound("armeEpeeVide");//FINIR
                break;

            case ("Bow"):
                SoundManager.playSound("arcSound");//FINIR
                break;

            case ("Boomerang"):
                SoundManager.playSound("armeEpeeVide");//FINIR
                break;
        }
    }
}
