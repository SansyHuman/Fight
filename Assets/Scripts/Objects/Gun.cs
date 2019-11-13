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
    
    private bool isShotOnCooldown = false;

    [SerializeField] private Transform rightHandPivot;
    [SerializeField] private Transform bulletPivot;              //Pivot to determine where bullet will instantiate
    [SerializeField] private Transform barrelPivot;              //Pivot to determine the direction where bullet will shot

    private Character player;

    public void Initialize(Character player, Transform arm, Vector3 relPivot)
    {
        transform.parent = arm;

        Vector3 localPos = relPivot - rightHandPivot.localPosition;
        transform.localPosition = localPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
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
