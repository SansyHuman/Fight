using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private KeyCode shoot = KeyCode.Space;          // BAD CODE
    [SerializeField] private KeyCode moveLeft = KeyCode.LeftArrow;   // BAD CODE
    [SerializeField] private KeyCode moveRight = KeyCode.RightArrow; // BAD CODE
    [SerializeField] private float shotCooldownTime = 0.5f;       // Delay time before next shot, mayb to be edit to rate of fire
    [SerializeField] private GameObject bulletPrefab;
    
    private Transform rightHandTransform;
    private bool isShotOnCooldown = false;
    private Transform bulletPivot;              //Pivot to determine where bullet will instantiate
    private Transform barrelPivot;              //Pivot to determine the direction where bullet will shot

    void Awake()
    {
        rightHandTransform = GameObject.Find("RightArmTarget").transform;  // BAD CODE
        bulletPivot = transform.Find("BulletPivot");
        barrelPivot = transform.Find("BarrelPivot");
    }

    void Update()
    {
        transform.position = rightHandTransform.position;
        transform.localRotation = Quaternion.Inverse(rightHandTransform.localRotation);
        if (Input.GetKeyDown(shoot) && !isShotOnCooldown)
        {
            Shoot();
            isShotOnCooldown = true;
            StartCoroutine(onShotCooldown());
        }
    }

    private IEnumerator onShotCooldown()
    {
        yield return new WaitForSeconds(shotCooldownTime);
        isShotOnCooldown = false;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        bullet.GetComponent<Bullet>().SetVelocity((bulletPivot.position-barrelPivot.position) * 0.3f);
    }
}
