using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    public GameObject arrow;
    private float shootTimer=0;

    private void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    public override void Use(Character user)
    {
        base.Use(user);
        if(shootTimer<=0)
        {
            shootTimer = 1;
            GameObject go = Instantiate(arrow, holder.transform);
            Vector2 direction = holder.direction;
            Vector3 toTarget = direction.normalized;

            go.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            go.GetComponent<Arrow>().user = gameObject.GetComponent<AnimateEntity>();
            float sign = (direction.y < Vector3.right.y) ? 1.0f : -1.0f;
            go.transform.rotation = Quaternion.Euler(0, 0, 270 - Vector3.Angle(Vector3.right, direction) * sign);
            go.GetComponent<Rigidbody2D>().velocity = toTarget * 15;
        }
    }
}
