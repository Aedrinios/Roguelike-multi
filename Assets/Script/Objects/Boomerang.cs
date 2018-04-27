using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon {

    public int isUsed; // 0 : pas utilisé / 1 : boomerang lancé / 2 : boomerang reviens;
    public Vector3 dir;
    public Vector3 previousDir;

    protected override void Update()
    {
        if (holder == null)
            base.Update();
    }

    public override void Use(Character user)
    {
        if (isUsed == 0) // lance le boomerang
        {
            isUsed = 1;
            dir = user.direction;
            if (dir == Vector3.zero) // pour eviter d'avoir un boomerang qui tourne sur soi.
            {
                dir = previousDir;
            }
            LaunchAnimation();
            holder.stun = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 50, ForceMode2D.Impulse);
            StartCoroutine(Wait(0.5f));
            previousDir = dir;
        }
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
            animator.SetBool("attack",true);
            transform.localPosition = Vector3.zero;
        }
    }

    public override void EndAnim()
    {
        base.EndAnim();
        animator.SetBool("attack", false);
        GetComponent<CircleCollider2D>().enabled = false;
    }

    IEnumerator Wait(float tps)
    {
        yield return new WaitForSeconds(tps);
        if(isUsed==2) // "ramasse" le boomerang
        {
            isUsed = 0;
            EndAnim();
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //Equip(holder.GetComponent<Character>());
        }
        if (isUsed==1) // lance le retour du boomerang
        {
            dir = (holder.transform.position - gameObject.transform.position).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(dir * 50f, ForceMode2D.Impulse);
            isUsed = 2;
            StartCoroutine(Wait(0.5f));
        }
    }
}
