using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InanimateEntity : MonoBehaviour
{
    protected bool isEquipped;
    public BoxCollider2D pickupCollider;
    public Sprite thumbnail;
    [SerializeField] protected AnimateEntity holder;
    protected int damage;
    public Vector2 currentRoom;

    public AnimateEntity Holder
    {
        get { return holder; }
        set { holder = value; }
    }

    public virtual void Equip(Character user)
    {
        isEquipped = true;
        holder = user;
        foreach (SpriteRenderer sp in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.enabled = false;
            Debug.Log(sp.gameObject.name);
        }
        this.GetComponentInChildren<CircleCollider2D>().enabled = false;
        pickupCollider.enabled = false;
        this.transform.parent = user.transform;
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public virtual void Unequip(int no)
    {
        holder.GetComponent<Character>().inventory[no] = null;
        foreach (SpriteRenderer sp in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.enabled = true;
        }
        transform.position = holder.transform.position;
        holder = null;
        isEquipped = false;
        this.GetComponentInChildren<CircleCollider2D>().enabled = true;
    }

    public abstract void Use(Character user);

    public virtual int GetDamage()
    {
        return damage;
    }
}
