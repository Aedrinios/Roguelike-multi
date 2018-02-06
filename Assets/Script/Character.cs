using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AnimateEntity
{

    public InanimateEntity[] inventory;
    public ArrayList ground;
    [HideInInspector]
    public PlayerUI UI;


    private Rigidbody2D rigidb;
    private Animator animator;
    private float primaryTimer;
    private float secondaryTimer;
    private bool isCarrying = false;
    private InanimateEntity carriedObject;
    private float deathTime;
    private float deathTimeCount;
    private int startLife;
    private bool deathAudioHasPlayed;
    private float opacityValue;
    private float blinkTime;
    private float blinkTimeCount;
    private GlobalHealthManager globalHealthManager;


    protected override void Start()
    {
        globalHealthManager = FindObjectOfType<GlobalHealthManager>();

        base.Start();
        inventory = new InanimateEntity[2];
        ground = new ArrayList();
        rigidb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        deathTime = 3;
        deathTimeCount = 0;
        startLife = health;
        deathAudioHasPlayed = false;
        opacityValue = 0.3f;
        blinkTime = 0.2f;
        blinkTimeCount = 0;
    }

    public override void ReceiveHit(int value, GameObject other)
    {
        base.ReceiveHit(value, other);
        UI.SetHealth(health);
    }

    public void Update() // déséquiper pour l'instant
    {
        Debug.Log(health);

        if (Input.GetKeyDown("2"))
        {
            ReceiveHit(startLife,gameObject);
        }


        if (health <= 0)
        {
            UI.emptyFullInventory();
            inventory[0] = null;
            inventory[1] = null;

            if (isDead == false)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                globalHealthManager.globalHealth--;
                animator.SetBool("isDead", true);
                animator.SetFloat("directionY", -1);
                animator.SetFloat("directionX", 0);
                GetComponent<CharController>().enabled = false;
            }
            isDead = true;
            canBeDamaged = false;
            canAttack = false;
            
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacityValue);

            //joue le son de mort
            if (deathAudioHasPlayed == false)
            {
                audioSource.PlayOneShot(SoundManager.getSound("goule2Mort2"));
                deathAudioHasPlayed = true;
            }

            //temps d'invincibilité
            deathTimeCount += Time.deltaTime;
            blinkTimeCount += Time.deltaTime;

            //RESPAWN
            if (deathTimeCount >= deathTime)
            {
                isDead = false;
                canAttack = true;
                canBeDamaged = true;
                deathTimeCount = 0;
                health = startLife;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
                deathAudioHasPlayed = false;
                //audioSource.PlayOneShot(getSound("respawnSound"));
                opacityValue = 0.3f;
                UI.reactivateInventory();
            }
            //clignotement avant de respawn
            else if (deathTimeCount >= deathTime * 0.66f)
            {
                if (blinkTimeCount >= blinkTime)
                {
                    if (opacityValue == 1f)
                    {
                        opacityValue = 0.3f;
                        blinkTimeCount = 0;
                    }
                    else
                    {
                        opacityValue = 1f;
                        blinkTimeCount = 0;
                    }
                }
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, opacityValue);
            }
            else if (deathTimeCount >= deathTime * 0.33f)
            {
                GetComponent<CharController>().enabled = true;
                animator.SetBool("isDead", false);
            }
        }

        //ramassage puis lancer
        if (Input.GetButtonDown("Keyboard 2 interact")/*|| Input.GetButtonDown("Keyboard 1 interact")*/)
        {
            Debug.Log("gg");
            Interract();
        }

        //déséquipement arme 1
        if (Input.GetButton("Keyboard 2 primary"))
        {
            Debug.Log("gg");
            primaryTimer += Time.deltaTime;
            if (primaryTimer > 2 && inventory[0] != null)
                Carry(inventory[0]);
        }
        if (Input.GetButtonUp("Keyboard 2 primary"))
            primaryTimer = 0;

        //desequipement arme 2
        if (Input.GetButton("Keyboard 2 secondary"))
        {
            Debug.Log("gg");
            secondaryTimer += Time.deltaTime;
            if (secondaryTimer > 2 && inventory[1] != null)
                Carry(inventory[1]);
        }
        if (Input.GetButtonUp("Keyboard 2 secondary"))
            secondaryTimer = 0;
    }


    protected override void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void Revive()
    {
        animator.SetBool("isDead", false);
    }

    public void InputAction(params object[] args)
    {   
        if(!isDead)
        {
            int item = (int)args[0];
            if (inventory[item] == null)
                TryPickupItem(item);
            else
                inventory[item].Use(this);
            Debug.Log("pressed");
        }
    }

    public float GetRotation()
    {
        float rotation = Vector2.Angle(new Vector2(0, 1), this.direction);
        if (direction.x < 0)
            rotation *= -1;
        return rotation;
    }

    private void TryPickupItem(int slot)
    {
        if (ground.Count != 0)
        {
            inventory[slot] = (InanimateEntity)ground[0];
            inventory[slot].Equip(this);
            UI.ChangeWeapon(this, slot);
            //audioSource.PlayOneShot(getSound("pickUpItem"));
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("item"))
        {
            ground.Add(other.GetComponent<InanimateEntity>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("item"))
        {
            ground.Remove(other.GetComponent<InanimateEntity>());
        }
    }

    public void SetUI(PlayerUI UI)
    {
        this.UI = UI;
        UI.SetPlayer(this);
    }

    private void TryCarry()
    {
        if (ground.Count != 0)
        {
            carriedObject = (InanimateEntity)ground[0];
            Carry(carriedObject);
        }   //No need to remove the item from the ground list, it's done in OnTriggerExit
    }

    public void Interract()
    {
        //Si un objet interractible est sous ses pieds, alors interragis avec.
        if (isCarrying)
        {
            Throw();
        }
        else
        {
            TryCarry();
        }
    }

    public void Carry(InanimateEntity Ientity)
    {
        if (Ientity == inventory[0])
        {
            Ientity.Unequip(0);
            UI.emptySlotInventory(0);
        }
        if (Ientity == inventory[1])
        {
            Ientity.Unequip(1);
            UI.emptySlotInventory(1);
        }
        if (Ientity.tag == "Player" || Ientity.tag == "enemy")
        {
            Ientity.GetComponent<AnimateEntity>().stun = true;
        }
        isCarrying = true;
        Debug.Log("blub");
        Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
        Ientity.pickupCollider.enabled = false;
        Ientity.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Ientity.transform.parent = this.transform;
        Ientity.transform.localPosition = Vector3.zero;
        Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        carriedObject = Ientity;
    }

    public void Throw()
    {
        Debug.Log("bloub");
        if (isCarrying)
        {
            Debug.Log("blib");
            if (carriedObject.tag == "Player" || carriedObject.tag == "enemy")
            {
                carriedObject.GetComponent<AnimateEntity>().stun = false;
            }
            carriedObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50, ForceMode2D.Impulse);
            //carriedObject.GetComponentInChildren<CircleCollider2D>().enabled = true;
            carriedObject.pickupCollider.enabled = true;
            carriedObject.transform.parent = null;
            //carriedObject.transform.localPosition = Vector3.zero;
            //carriedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject.GetComponent<CircleCollider2D>().enabled = true;
            carriedObject = null;
            isCarrying = false;
        }
    }
}


