using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGun : Gun
{
    protected override void Shoot()
    {
        StartCoroutine(ShootBullet());
    }
    
}
