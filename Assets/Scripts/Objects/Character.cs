using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageGettable
{
    [SerializeField] private float health;
    [SerializeField] [Range(0.0001f, 30)] private float speed = 3f;
    [SerializeField] [Range(0.0001f, 60)] private float acceleration = 6f;
    [SerializeField] [Range(0, 1)] private float airAccelerationMultiplier = 0.7f;
    [SerializeField] [Range(0.0001f, 120)] private float drag = 12f;
    [SerializeField] [Range(0.0001f, 500)] private float jumpForce = 80f;

    [SerializeField] private KeyCode moveLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRight = KeyCode.RightArrow;
    [SerializeField] private KeyCode jump = KeyCode.UpArrow;
    [SerializeField] private KeyCode interact = KeyCode.DownArrow;

    private bool alive = true;

    public float Health => health;

    private Transform chrTransform;
    private Rigidbody2D rb2D;
    private GameObject self;

    private void Awake()
    {
        chrTransform = transform;
        rb2D = GetComponent<Rigidbody2D>();
        self = gameObject;
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            OnDeath();
        }
    }

    public void OnDeath()
    {

    }

    private void FixedUpdate()
    {
        MoveCharacter(Time.deltaTime);
    }

    private void MoveCharacter(float deltaTime)
    {
        float horzInput = 0;
        if (Input.GetKey(moveLeft))
            horzInput = -1;
        if (Input.GetKey(moveRight))
            horzInput = 1;

        float horzVel = rb2D.velocity.x;

        if (horzInput == 0)
        {
            if (horzVel < 0)
            {
                horzVel += drag * deltaTime;
                if (horzVel > 0)
                    horzVel = 0;
            }
            else
            {
                horzVel -= drag * deltaTime;
                if (horzVel < 0)
                    horzVel = 0;
            }
        }
        else
        {
            horzVel += acceleration * horzInput * deltaTime;
            if (Mathf.Abs(horzVel) > speed)
            {
                if (horzVel < 0)
                    horzVel = -speed;
                else
                    horzVel = speed;
            }
        }

        rb2D.velocity = new Vector2(horzVel, rb2D.velocity.y);
    }
}
