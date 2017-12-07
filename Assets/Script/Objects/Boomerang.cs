using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : InanimateEntity {

    public float pushPower;
    public Animator animator;
    public CircleCollider2D rigid;
    public int isUsed; // 0 : pas utilisé / 1 : boomerang lancé / 2 : boomerang reviens;
    public Vector3 dir;
    public Vector3 previousDir;
    public Character owner;

    private void Start()
    {
        damage = 3;
    }

    public override void Use(Character user)
    {
        if(isUsed==0) // lance le boomerang
        {
            isUsed = 1;
            dir = user.direction;
            if (dir==Vector3.zero) // pour eviter d'avoir un boomerang qui tourne sur soi. aimplémenter dans le perso?
            {
                dir = previousDir;
            }
            LaunchAnimation();
           // holder.stun = true; // à commenter quand je changerai le mouvement du boomerang
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir*15, ForceMode2D.Impulse);
            StartCoroutine(Wait(0.5f));
            previousDir = dir;
        }
    }

    public override void Equip(Character user)
    {
        owner = user;
        isEquipped = true;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        pickupCollider.enabled = false;
        this.transform.parent = user.transform;
        transform.localPosition = Vector3.zero;
        holder = user;
    }


    private void CopyUserRotation(Character user)
    {   //Useless pour le moment
        this.transform.rotation = Quaternion.Euler(0, 0, user.GetRotation() * -1);
    }

    private void LaunchAnimation()
    {
        if (!animator.GetBool("attack"))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            animator.SetBool("attack",true);
            transform.localPosition = Vector3.zero;
        }
    }

    public void EndAnim()
    {
        animator.SetBool("attack", false);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        //holder.stun = false;
        Debug.Log("wut");
    }

    public void OnCharacterHit(Character other)
    {
        Vector2 push = (Vector2)(other.transform.position - holder.transform.position).normalized;
        other.GetComponent<Rigidbody2D>().AddForce(push * pushPower);
    }

    IEnumerator Wait(float tps)
    {
        yield return new WaitForSeconds(tps);
        if(isUsed==2) // "ramasse" le boomerang
        {
            Debug.Log("blub");
            isUsed = 0;
            EndAnim();
            //holder.stun = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Equip(holder);
        }
        if (isUsed==1) // lance le retour du boomerang
        {
            dir = (owner.transform.position - gameObject.transform.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 15f, ForceMode2D.Impulse);
            Debug.Log("blib");
            isUsed = 2;
            StartCoroutine(Wait(0.5f));
        }
    }
}
