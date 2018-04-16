

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AnimateEntity : InanimateEntity
{

    protected Timer invincibility;
    public int health;
    public int maxHP;
    public float speed;
    public int attack;
    public ProtectionShield protectionShield;
    protected bool canBeDamaged = true;
    protected bool canAttack = true;
    protected bool isDead = false;
    [HideInInspector]
    public bool stun = false;
    public bool slow = false;
    public float timeOfInvincibility;
    protected Rigidbody2D rigidb;
    protected Animator animator;
    [HideInInspector]
    public Vector3 direction;
    protected bool isDying = false;
    protected AudioSource audioSource;
    protected float scaleMultiplier = 1f;
    protected SpriteRenderer spriteRenderer;

    private ProtectionShield currentShield=null;
    private bool isNotSlowed = false;

    protected virtual void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        maxHP = health;
        invincibility = new Timer(timeOfInvincibility, true);
        rigidb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Move(Vector2 directionRequired)
    {
        if (!stun)
        {
            this.direction = directionRequired.normalized;
            rigidb.velocity = direction * speed;
            animator.SetFloat("directionX", direction.x);
            animator.SetFloat("directionY", direction.y);
            animator.SetBool("isMoving", true);
        }
    }
    public virtual void Idle()
    {
        animator.SetBool("isMoving", false);
        rigidb.velocity = Vector3.zero;
    }

    public virtual IEnumerator Stun(float time)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        stun = true;
        gameObject.GetComponent<AnimateEntity>().stun = true;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(time);
        stun = false;
        gameObject.GetComponent<AnimateEntity>().stun = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public virtual IEnumerator Slow(float pourcentage, float time)
    {
        if (!isNotSlowed)
        {
            isNotSlowed = true;
            Debug.Log("SLOW");
            float baseSpeed = speed;
            speed *= 1 - pourcentage;
            yield return new WaitForSeconds(time);
            speed = baseSpeed;
            isNotSlowed = false;
        }
    }

    public override void Use(Character user)
    {
        throw new System.NotImplementedException();
    }

    public int GetAttack()
    {
        return attack;
    }

    public bool getIsDead()
    {
        return isDead;
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
        //dyingSound.Play();
    }

    public void setCanBeDamaged(bool b)
    {
        canBeDamaged = b;

        if (!canBeDamaged)
        {
            if (currentShield == null)
            {
                //sprite change de couleur indiquant impossibilité d'être frappé, bouclier posé
                currentShield = Instantiate(protectionShield, transform.position, Quaternion.identity, transform); 
                currentShield.transform.localScale = new Vector3((transform.localScale.x + 0.3f) * scaleMultiplier, (transform.localScale.y + 0.3f) * scaleMultiplier, 0);
                //Debug.Log("Shield :" + this.name + "   " + scaleMultiplier);
                //Debug.Log(this.name +" : Shield de protection activé");
            }
        }
        else
        {
            if (currentShield != null)
            {
                Destroy(currentShield.gameObject);
                currentShield = null;
                //Debug.Log(this.name + " : Shield de protection désactivé");
            }
        }
    }


    public bool getCanBeDamaged()
    {
        return canBeDamaged;
    }

    public virtual void ReceiveHit(int value, GameObject other)
    {
        //Debug.Log("je suis dans receive hit"); 
        if (!invincibility.IsFinished())
        {
            return;
        }
        invincibility.ResetPlay();

        if (canBeDamaged &&!isDead)
        {
            if (value != 0)
                isDamaged();
            //reduce health
            health -= value;
            KnockBack(other);

        }
        if(!canBeDamaged && !isDead)
        {
            SoundManager.playSound("shieldSound2"); //BRUIT DE BOUCLIER     
        }
        
        if (health <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public virtual void ReceiveHealt(int value, GameObject other)
    {
        for (int i = 0; i < value; i++)
        {
            if (health < maxHP)
            {
                health++;
            }
        }
    }

    public virtual void isDamaged()
    {
        animator.SetTrigger("damaged");
        
    }

    public bool IsDying()
    {
        return isDying;
    }

    protected virtual void KnockBack(GameObject other)
    {
        Vector3 knockBackDirection = (transform.position - other.transform.position).normalized;
        transform.position += knockBackDirection * (int)knockbackDistances.low;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

	public virtual void FriendlyKnockBack(GameObject other)
	{
		Vector3 knockBackDirection = (transform.position - other.transform.position).normalized;
		transform.position += knockBackDirection * (int)knockbackDistances.low;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	}

    public ProtectionShield getCurrentShield()
    {
        return currentShield; 
    }

}

public enum knockbackDistances
{
    low = 1,
    medium = 2,
    high = 4
}



