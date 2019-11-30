using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthpackItem : Item
{
    [SerializeField] private int healingAmount = 10;

    protected override void UseItem()
    {
        player.GetHeal(healingAmount);
        print("Healthpack used on " + player.name);
    }
}
