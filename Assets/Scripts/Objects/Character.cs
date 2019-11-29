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
    [SerializeField] private float speed = 3f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float airAccelerationMultiplier = 0.7f; // Acceleration multiplier when the character is in air.
    [SerializeField] private float drag = 12f; // Drag acceleration when the movement key is not pressing.
    [SerializeField] private float jumpForce = 80f;

    [SerializeField] private KeyCode moveLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode moveRight = KeyCode.RightArrow;
    [SerializeField] private KeyCode jump = KeyCode.UpArrow;
    [SerializeField] private KeyCode interact = KeyCode.DownArrow;

    [SerializeField] private Gun weapon;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform weaponArm;

    private bool alive = true;
    private bool controllable = true;

    private bool isLanding = false;
    private bool isHitting = false;

    private Gun gunSlot;
    private Item itemSlot;

    /// <value>Override of <see cref="IDamageGettable.Health"/>. Gets the health of the object.</value>
    public float Health => health;
    public bool Alive => alive;
    public bool Controllable => controllable;

    private Animator anim;
    private Transform chrTransform;
    private Rigidbody2D rb2D;
    private CharacterFeet feet;
    private IKManager2D manager;
    private GameObject self;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        chrTransform = transform;
        rb2D = GetComponent<Rigidbody2D>();
        feet = GetComponentInChildren<CharacterFeet>();
        manager = GetComponent<IKManager2D>();
        self = gameObject;

        gunSlot = Instantiate<Gun>(weapon);
        gunSlot.name = weapon.name;
        gunSlot.Initialize(this, weaponArm, weaponPosition.localPosition);

        itemSlot = Instantiate<Gun>(item);
        itemSlot.name = item.name;
        itemSlot.Initialize(this, weaponArm, weaponPosition.localPosition);
    }

    /// <summary>
    /// Override of <see cref="IDamageGettable.GetDamage(float)"/>. Deals damage to the character.
    /// </summary>
    /// <param name="damage">Damage the character gets</param>
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            alive = false;
            OnDeath();
        }
    }

    public void GetDamage(float damage, float knockBackForce, float knockBackDuration)
    {
        GetDamage(damage);

        knockBackTime = 0;
        this.knockBackDuration = knockBackDuration;
        controllable = false;
        isHitting = true;
        anim.SetBool("hit", true);
        anim.SetBool("isHitting", isHitting);

        Vector3 scale = chrTransform.localScale;
        if (knockBackForce < 0)
            scale.x = 1;
        if (knockBackForce > 0)
            scale.x = -1;

        chrTransform.localScale = scale;

        rb2D.velocity = new Vector2(knockBackForce, rb2D.velocity.y);
    }

    /// <summary>
    /// Override of <see cref="IDamageGettable.OnDeath()"/>. Called when the character's health becomes 0.
    /// </summary>
    public void OnDeath()
    {
        anim.SetBool("dead", true);
    }

    float knockBackTime = 0;
    float knockBackDuration = 0;

    private void Update()
    {
        float xVel = rb2D.velocity.x;
        float absVel = Mathf.Abs(xVel);
        if (Mathf.Abs(xVel) > 0.1f && !isHitting)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("walkSpeed", absVel * 2f / speed);

            Vector3 scale = chrTransform.localScale;
            if (xVel < 0)
                scale.x = -1;
            else
                scale.x = 1;
            chrTransform.localScale = scale;

            if (gunSlot != null)
                SetSolversWeight(absVel / speed);
        }
        else
        {
            anim.SetBool("isWalking", false);
            SetSolversWeight(1);
        }

        anim.SetBool("isLanding", isLanding);
        anim.SetBool("isFallingDown", rb2D.velocity.y <= 0);

        if (!isLanding && !anim.GetBool("isFallingDown"))
            SetSolversWeight(1);

        if (gunSlot != null && gunSlot.Firing)
            SetSolversWeight(1);
    }

    private void LateUpdate()
    {
        if (anim.GetBool("hit"))
            anim.SetBool("hit", false);
    }

    private void SetSolversWeight(float weight)
    {
        if (weight < 0)
            weight = 0;
        if (weight > 1)
            weight = 1;

        for (int i = 0; i < manager.solvers.Count; i++)
            manager.solvers[i].weight = weight;
    }

    private void SetSolverWeight(string name, float weight)
    {
        for (int i = 0; i < manager.solvers.Count; i++)
        {
            if (name.Equals(manager.solvers[i].name))
            {
                manager.solvers[i].weight = weight;
                break;
            }
        }
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
        if (gunSlot != null && gunSlot.Firing)
            horzInput = isLanding ? 0 : horzInput;

        float horzVel = rb2D.velocity.x;

        if (!controllable)
        {
            if (isHitting)
            {
                knockBackTime += deltaTime;
                horzVel *= 0.6f;
                if (knockBackTime > knockBackDuration)
                {
                    isHitting = false;
                    controllable = true;
                    anim.SetBool("isHitting", isHitting);
                }
            }
            goto VelocitySet;
        }

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
            float velDiff = acceleration * horzInput * deltaTime;
            if (!isLanding)
                velDiff *= airAccelerationMultiplier;

            horzVel += velDiff;
            if (Mathf.Abs(horzVel) > speed)
            {
                if (horzVel < 0)
                    horzVel = -speed;
                else
                    horzVel = speed;
            }
        }

        VelocitySet:

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
