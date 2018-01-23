using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AnimateEntity : InanimateEntity {

    protected int life;
    protected float speed;
    protected int attack;
    protected bool canBeDamaged=true;
    public bool stun = false;
    protected Rigidbody2D rigidb;
    protected Animator animator;
    public Vector3 direction;


    public abstract void Move(Vector2 direction);

    public virtual void DecreaseHealth(int damage)
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

    public int GetAttack()
    {
        return attack;
    }

}
