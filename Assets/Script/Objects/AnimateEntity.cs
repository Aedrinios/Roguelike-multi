

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AnimateEntity : InanimateEntity
{

    private Timer invincibility;
    public int health = 10;
    public float speed = 10;
    public int attack = 2;
    public AudioSource dyingSound, hitSound;
    protected bool canBeDamaged = true;
    protected bool canAttack = true;
    protected bool isDead = false;
    [HideInInspector]
    public bool stun = false;
    public float timeOfInvincibility;
    protected Rigidbody2D rigidb;
    protected Animator animator;
    [HideInInspector]
    public Vector3 direction;
    protected bool isDying = false;
    public AudioClip[] sounds;
    protected AudioSource audioSource;


    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);
        rigidb = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(Vector2 directionRequired)
    {
        this.direction = directionRequired.normalized;
        rigidb.velocity = direction * speed;
        animator.SetFloat("directionX", direction.x);
        animator.SetFloat("directionY", direction.y);
        animator.SetBool("isMoving", true);
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

    protected virtual void Die()
    {
        //dyingSound.Play();
    }

    public bool getCanBeDamaged()
    {
        return canBeDamaged;
    }

    public virtual void ReceiveHit(int value, GameObject other)
    {
        if (!invincibility.IsFinished())
            return;
        //hitSound.Play();
        invincibility.ResetPlay();
        
        if (canBeDamaged == true &&!isDead)
        {
            health -= value;
        }
        KnockBack(other);

        if (health <= 0)
            Die();
    }

    public bool IsDying()
    {
        return isDying;
    }

    private void KnockBack(GameObject other)
    {
        Vector3 knockBackDirection = (transform.position - other.transform.position).normalized;
        transform.position += knockBackDirection * (int)knockbackDistances.low;
    }

    public AudioClip getSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                return sounds[i];
            }
        }
        return new AudioClip();
    }

}

public enum knockbackDistances
{
    low = 1,
    medium = 2,
    high = 4
}

