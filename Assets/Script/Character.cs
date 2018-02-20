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
    private string inputSetName;


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
        inputSetName = gameObject.GetComponent<CharController>().GetInputs().GetName() + " ";
    }

    public override void ReceiveHit(int value, GameObject other)
    {
        if (inventory[1] != null && inventory[1].GetComponent<Weapon>().armorPoints > 0) // Slot 2 
        {
            var arm2 = inventory[1].GetComponent<Weapon>().armorPoints;
            inventory[1].GetComponent<Weapon>().armorPoints = arm2 - value;
            UI.armorHealth(); // Armor Health
        }
        else if (inventory[0] != null && inventory[0].GetComponent<Weapon>().armorPoints > 0) // Slot 1
        {
            var arm1 = inventory[0].GetComponent<Weapon>().armorPoints;
            inventory[0].GetComponent<Weapon>().armorPoints = arm1 - value;
            UI.armorHealth(); // Armor Health
        }
        else // Joueur
        {
            base.ReceiveHit(value, other);
            UI.SetHealth(health); // Player health
        }
    }

    public void Update() // déséquiper pour l'instant
    {

        Debug.Log(inputSetName);
        if (Input.GetKeyDown("2"))

       // Debug.Log(health);
        if (Input.GetKeyDown("3"))
        {
            setCanBeDamaged(false);
        }

        if (Input.GetKeyDown("5"))
        {
            setCanBeDamaged(true);
        }

        if (Input.GetKeyDown("4"))
        {
            ReceiveHit(startLife, gameObject);
        }

        
        if (health <= 0)
        {
            UI.emptyFullInventory();
            inventory[0] = null;
            inventory[1] = null;

            if (isDead == false)
            {
                Debug.Log("OOOOOOOOOOOOOOOOOOOOO");
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                globalHealthManager.decreaseGlobalHealth(1);
                animator.SetBool("isDead", true);
                animator.SetFloat("directionY", -1);
                animator.SetFloat("directionX", 0);
                GetComponent<CharController>().enabled = false;

                isDead = true;
                canBeDamaged = false;
                canAttack = false;
            }

            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacityValue);

            //joue le son de mort
            if (deathAudioHasPlayed == false)
            {
                SoundManager.playSound("goule2Mort2");
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
                SoundManager.playSound("respawnSound");
                opacityValue = 0.3f;
                UI.reactivateInventory();
                UI.SetHealth(startLife);  //remplir coeurs
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
        if (Input.GetButtonDown(inputSetName + "interact"))
        {
            Debug.Log("gg");
            Interract();
        }

        //déséquipement arme 1
        if (Input.GetButton(inputSetName + "primary"))
        {
            Debug.Log("gg");
            primaryTimer += Time.deltaTime;
            if (primaryTimer > 1 && inventory[0] != null && !isCarrying)
                Carry(inventory[0]);
        }
        if (Input.GetButtonUp(inputSetName + "primary"))
        {
            primaryTimer = 0;
        }

        //desequipement arme 2
        if (Input.GetButton(inputSetName + "secondary"))
        {
            Debug.Log("gg");
            secondaryTimer += Time.deltaTime;
            if (secondaryTimer > 1 && inventory[1] != null)
                Carry(inventory[1]);
        }
        if (Input.GetButtonUp(inputSetName + "secondary"))
        {
            secondaryTimer = 0;
        }
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
        if (!isDead)
        {
            int item = (int)args[0];
            if (inventory[item] == null)
            {
                TryPickupItem(item);
            }
            else
            {
                inventory[item].Use(this);
                switch (inventory[item].name)
                {
                    case ("Sword"):
                        SoundManager.playSound("armeEpeeVide");
                        break;

                    case ("Stick"):
                        SoundManager.playSound("armeEpeeVide"); //FINIR
                        break;
                    case ("Spear"):
                        SoundManager.playSound("armeEpeeVide");//FINIR
                        break;
                    case ("Boomerang"):
                        SoundManager.playSound("armeEpeeVide");//FINIR
                        break;
                }
            }

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

    private void TryPickupItem(int slot)
    {
        if (ground.Count != 0)
        {
            inventory[slot] = (InanimateEntity)ground[0];
            inventory[slot].Equip(this);
            UI.ChangeWeapon(this, slot);
            SoundManager.playSound("pickUpItem");
        }
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

    private void TryCarry()
    {
        if (ground.Count != 0 && !isCarrying)
        {
            carriedObject = (InanimateEntity)ground[0];
            Carry(carriedObject);
        }
    }

    public void Carry(InanimateEntity Ientity)
    {
        if (Ientity == inventory[0] && !isCarrying)
        {
            Ientity.Unequip(0);
            UI.emptySlotInventory(0);
            isCarrying = true;
            Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
            Ientity.pickupCollider.enabled = false;
            foreach (SpriteRenderer sp in Ientity.GetComponentsInChildren<SpriteRenderer>())
            {
                sp.enabled = true;
            }
            Ientity.transform.parent = this.transform;
            Ientity.transform.localPosition = Vector3.zero;
            Ientity.transform.rotation = Quaternion.Euler(0, 0, 0);
            Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject = Ientity;
        }
        else if (Ientity == inventory[1] && !isCarrying)
        {
            Ientity.Unequip(1);
            UI.emptySlotInventory(1);
            isCarrying = true;
            Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
            Ientity.pickupCollider.enabled = false;
            foreach (SpriteRenderer sp in Ientity.GetComponentsInChildren<SpriteRenderer>())
            {
                sp.enabled = true;
            }
            Ientity.transform.parent = this.transform;
            Ientity.transform.localPosition = Vector3.zero;
            Ientity.transform.rotation = Quaternion.Euler(0, 0, 0);
            Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject = Ientity;
        }
        else if ((Ientity.tag == "Player" || Ientity.tag == "enemy") && !isCarrying)
        {
            Ientity.GetComponent<AnimateEntity>().stun = true;
            isCarrying = true;
            Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
            Ientity.pickupCollider.enabled = false;
            foreach (SpriteRenderer sp in Ientity.GetComponentsInChildren<SpriteRenderer>())
            {
                sp.enabled = true;
            }
            Ientity.transform.parent = this.transform;
            Ientity.transform.localPosition = Vector3.zero;
            Ientity.transform.rotation = Quaternion.Euler(0, 0, 0);
            Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject = Ientity;
        }
        else if (!isCarrying)
        {
            isCarrying = true;
            Ientity.GetComponentInChildren<CircleCollider2D>().enabled = false;
            Ientity.pickupCollider.enabled = false;
            foreach (SpriteRenderer sp in Ientity.GetComponentsInChildren<SpriteRenderer>())
            {
                sp.enabled = true;
            }
            Ientity.transform.parent = this.transform;
            Ientity.transform.localPosition = Vector3.zero;
            Ientity.transform.rotation = Quaternion.Euler(0, 0, 0);
            Ientity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            carriedObject = Ientity;
        }
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





