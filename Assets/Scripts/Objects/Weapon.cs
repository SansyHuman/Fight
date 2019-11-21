using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] protected float damage;
    [SerializeField] [Range(0, 100)] protected float knockBackForce;
    [SerializeField] [Range(0, 1)] protected float knockBackDuration;

    public float Damage
    {
        get => damage;
        set
        {
            damage = value;
            if (damage < 0)
                damage = 0;
            if (damage > 10)
                damage = 10;
        }
    }

    public float KnockBackForce
    {
        get => knockBackForce;
        set
        {
            knockBackForce = value;
            if (knockBackForce < 0)
                knockBackForce = 0;
            if (knockBackForce > 10)
                knockBackForce = 10;
        }
    }

    public float KnockBackDuration
    {
        get => knockBackDuration;
        set
        {
            knockBackDuration = value;
            if (knockBackDuration < 0)
                knockBackDuration = 0;
            if (knockBackDuration > 1)
                knockBackDuration = 1;
        }
    }
}
