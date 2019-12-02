using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] private int bulletNum = 5;
    [SerializeField] private float maxSpreadAngle = 30;
    protected override void Shoot()
    {
        for(int i=0;i<bulletNum;i++)
        {
            float _angle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            //print(_angle);
            StartCoroutine(ShootBullet(_angle));
        }
            
    }

}
