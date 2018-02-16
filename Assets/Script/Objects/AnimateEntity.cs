

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AnimateEntity : InanimateEntity
{

    protected Timer invincibility;
    public int health = 10;
    public float speed = 10;
    public int attack = 2;
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
    protected AudioSource audioSource;


    protected virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        invincibility = new Timer(timeOfInvincibility, true);
        rigidb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
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

    public void setCanBeDamaged(bool b)
    {
        canBeDamaged = b;

        if (!canBeDamaged)
        {
            //sprite change de couleur indiquant impossibilité d'être frappé, bouclier posé
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Debug.Log("Je change de couleur");
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(252f, 255f, 255f, 255f);
        }
    }


    public bool getCanBeDamaged()
    {
        return canBeDamaged;
    }

    public virtual void ReceiveHit(int value, GameObject other)
    {
        if (!invincibility.IsFinished())
        {
            return;
        }
        //hitSound.Play();
        invincibility.ResetPlay();
        
        if (canBeDamaged == true &&!isDead)
        {

            Debug.Log(value);
            health -= value;
            KnockBack(other);
        }
        
        if (health <= 0)
        {
            Die();
        }
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

}

public enum knockbackDistances
{
    low = 1,
    medium = 2,
    high = 4
}

