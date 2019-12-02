using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziGun : Gun
{
    [SerializeField] private float maxSpreadAngle = 15;
    protected override void Shoot()
    {
        float _angle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
        StartCoroutine(ShootBullet(_angle));
    }

}

