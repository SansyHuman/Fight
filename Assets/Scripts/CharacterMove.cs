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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        characterTr = transform;
        rb = GetComponent<Rigidbody2D>();
        self = gameObject;
        feet = GetComponentInChildren<CharacterFeet>();
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
        rb.velocity = new Vector2(horzInput * speed, rb.velocity.y);

        if (Input.GetButton("Jump") && isLanding)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
