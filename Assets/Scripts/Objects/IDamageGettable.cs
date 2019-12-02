using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface of all damagable objects.
/// </summary>
public interface IDamageGettable
{
    /// <value>Gets the health of the object.</value>
    float Health { get; }

    /// <summary>
    /// Deals damage to the object.
    /// </summary>
    /// <param name="damage">Damage the object gets</param>
    void GetDamage(float damage);

    /// <summary>
    /// Heal the object with an amount.
    /// </summary>
    /// <param name="heal">Healing the object gets</param>
    void GetHeal(float heal);

    /// <summary>
    /// Called when the object's health becomes 0.
    /// </summary>
    void OnDeath();
}
