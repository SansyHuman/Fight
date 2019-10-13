using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] [Range(0, 9)] private float speed = 3f;
    [SerializeField] [Range(0, 500)] private float jumpForce = 80f;
    private bool isLanding = false;

    private Animator anim;
    private SpriteRenderer sprite;
    private Transform characterTr;
    private Rigidbody2D rb;
    private GameObject self;

    private CharacterFeet feet;
    private CharacterAttack punch;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        characterTr = transform;
        rb = GetComponent<Rigidbody2D>();
        self = gameObject;

        feet = GetComponentInChildren<CharacterFeet>();
        punch = GetComponent<CharacterAttack>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.001f)
        {
            anim.SetBool("isWalking", true);
            sprite.flipX = rb.velocity.x < 0;
        }
        else
            anim.SetBool("isWalking", false);

        anim.SetBool("isLanding", isLanding);
        anim.SetBool("isFallingDown", rb.velocity.y <= 0);
    }

    private void FixedUpdate()
    {
        float deltaTime = Time.deltaTime;

        MoveCharacter(deltaTime);
    }

    private void MoveCharacter(float deltaTime)
    {
        float horzInput = Input.GetAxis("Horizontal");

        if ((punch.IsAttacking && isLanding )|| anim.GetBool("isCrouching") == true)
        {
            float xVel = rb.velocity.x;
            if (xVel >= 0)
            {
                xVel -= 30 * deltaTime;
                if (xVel < 0)
                    xVel = 0;
            }
            else
            {
                xVel += 30 * deltaTime;
                if (xVel > 0)
                    xVel = 0;
            }

            rb.velocity = new Vector2(xVel, rb.velocity.y);
        }
        else
            rb.velocity = new Vector2(horzInput * speed, rb.velocity.y);

        if (Input.GetButton("Jump") && isLanding && !punch.IsAttacking)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (Input.GetButton("Crouch") && isLanding && !punch.IsAttacking)
            anim.SetBool("isCrouching", true);
        else
            anim.SetBool("isCrouching", false);
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
