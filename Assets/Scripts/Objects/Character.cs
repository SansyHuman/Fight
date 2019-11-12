using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;

/// <summary>
/// Script of all characters
/// </summary>
public class Character : MonoBehaviour, IDamageGettable
{
    [SerializeField] private float health;
    [SerializeField] [Range(0.0001f, 30)] private float speed = 3f;
    [SerializeField] [Range(0.0001f, 60)] private float acceleration = 6f;
    [SerializeField] [Range(0, 1)] private float airAccelerationMultiplier = 0.7f; // Acceleration multiplier when the character is in air.
    [SerializeField] [Range(0.0001f, 120)] private float drag = 12f; // Drag acceleration when the movement key is not pressing.
    [SerializeField] [Range(0.0001f, 500)] private float jumpForce = 80f;

    [SerializeField] private KeyCode moveLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRight = KeyCode.RightArrow;
    [SerializeField] private KeyCode jump = KeyCode.UpArrow;
    [SerializeField] private KeyCode interact = KeyCode.DownArrow;

    [SerializeField] private LimbSolver2D[] solvers;

    private bool alive = true;

    private bool isLanding = false;

    /// <value>Override of <see cref="IDamageGettable.Health"/>. Gets the health of the object.</value>
    public float Health => health;

    private Animator anim;
    private Transform chrTransform;
    private Rigidbody2D rb2D;
    private CharacterFeet feet;
    private GameObject self;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        chrTransform = transform;
        rb2D = GetComponent<Rigidbody2D>();
        feet = GetComponentInChildren<CharacterFeet>();
        self = gameObject;
    }

    /// <summary>
    /// Override of <see cref="IDamageGettable.GetDamage(float)"/>. Deals damage to the character.
    /// </summary>
    /// <param name="damage">Damage the character gets</param>
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            OnDeath();
        }
    }

    /// <summary>
    /// Override of <see cref="IDamageGettable.OnDeath()"/>. Called when the character's health becomes 0.
    /// </summary>
    public void OnDeath()
    {

    }

    private void Update()
    {
        float xVel = rb2D.velocity.x;
        float absVel = Mathf.Abs(xVel);
        if (Mathf.Abs(xVel) > 0.0001f)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("walkSpeed", absVel * 2f / speed);

            Vector3 scale = chrTransform.localScale;
            if (xVel < 0)
                scale.x = -1;
            else
                scale.x = 1;
            chrTransform.localScale = scale;

            SetSolverWeight(absVel / speed);
        }
        else
        {
            anim.SetBool("isWalking", false);
            SetSolverWeight(1);
        }

        anim.SetBool("isLanding", isLanding);
        anim.SetBool("isFallingDown", rb2D.velocity.y <= 0);

        if (!isLanding && !anim.GetBool("isFallingDown"))
            SetSolverWeight(1);
    }

    private void SetSolverWeight(float weight)
    {
        if (weight < 0)
            weight = 0;
        if (weight > 1)
            weight = 1;

        for (int i = 0; i < solvers.Length; i++)
            solvers[i].weight = weight;
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

        if (Input.GetKey(jump) && isLanding)
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (feet.IsLanding)
            isLanding = true;
        else
            isLanding = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!feet.IsLanding)
            isLanding = false;
    }
}
