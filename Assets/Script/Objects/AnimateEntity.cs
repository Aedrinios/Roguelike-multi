using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AnimateEntity : InanimateEntity {

    protected float life;
    protected float speed;
    protected float attack;
    protected bool canBeDamaged=true;
    public bool stun = false;
    protected Rigidbody2D rigidb;
    protected Animator animator;
    public Vector3 direction;


    public abstract void Move(Vector2 direction);

    public virtual void DecreaseHealth(float damage)
    {
        this.life -= damage;
    }

    public virtual void Idle()
    {
        animator.SetBool("isMoving", false);
        rigidb.velocity = Vector3.zero;
    }


    public override void Use(Character user)
    {
        throw new System.NotImplementedException();
    }

    public float GetAttack()
    {
        return attack;
    }

}
