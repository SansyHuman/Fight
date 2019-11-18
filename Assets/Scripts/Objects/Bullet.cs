using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 velocity;
    private Rigidbody2D rb;
    private Collider2D collider;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    } 

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player " + collision.transform.parent.name + " hit by a bullet.");
            collision.gameObject.GetComponentInParent<Character>().GetDamage(1.0f);
        }
        Destroy(gameObject);
    }
}
