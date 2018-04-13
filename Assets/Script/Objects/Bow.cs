using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    public GameObject arrow;
    private bool canShoot = true;

    public override void Use(Character user)
    {
        if (canShoot && !user.stun)
        {
            base.Use(user);
            canShoot = false;
            GameObject go = Instantiate(arrow, holder.transform);
            Vector2 direction = holder.direction;
            Vector3 toTarget = direction.normalized;
            go.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            go.GetComponent<Arrow>().user = gameObject.GetComponent<AnimateEntity>();
            float sign = (direction.y < Vector3.right.y) ? 1.0f : -1.0f;
            go.GetComponent<InanimateEntity>().Holder = user;
            go.transform.rotation = Quaternion.Euler(0, 0, 270 - Vector3.Angle(Vector3.right, direction) * sign);
            go.GetComponent<Rigidbody2D>().velocity = toTarget * 15;
            StartCoroutine("ResetTimer");
        }
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
