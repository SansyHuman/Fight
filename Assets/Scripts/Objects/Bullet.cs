using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 velocity;

    void Update()
    {
        transform.position += velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    } 

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player " + collider.transform.parent.name + " hit by a bullet.");
            collider.gameObject.GetComponentInParent<Character>().GetDamage(1.0f);
            Destroy(gameObject);
        }
    }
}
