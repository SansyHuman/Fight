using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    private Rigidbody2D rb2D;
    [SerializeField] [Range(0, 10)] private float damageFallOff = 0;        //Rate of Bullet's damage decrease when travelling further

    private void FixedUpdate()
    {
        damage -= damageFallOff * Time.deltaTime;
    }

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb2D.velocity = velocity;
        direction = velocity.x < 0 ? -1 : 1;
    }

    float direction = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player " + collision.transform.parent.name + " hit by a bullet.");
            Debug.Log("Damage done by this bullet: " + damage);
            collision.gameObject.GetComponentInParent<Character>().GetDamage(damage, knockBackForce * direction, knockBackDuration);
        }
        Destroy(gameObject);
    }
}
