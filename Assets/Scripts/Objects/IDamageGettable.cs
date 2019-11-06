using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageGettable
{
    float Health { get; }
    void GetDamage(float damage);
    void OnDeath();
}
