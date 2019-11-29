using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private KeyCode shoot;
    [SerializeField] private float shotCooldownTime = 0.5f;       // Delay time before next shot, mayb to be edit to rate of fire
    [SerializeField] private float gunForce = 20.0f;       // Speed of the bullet out of the barrel
    [SerializeField] private Bullet bulletPrefab;
    
    private bool isShotOnCooldown = false;
    private bool firing = false;
    private const float fireAnimTime = 0.2f;

    [SerializeField] private Transform rightHandPivot;
    [SerializeField] private Transform bulletPivot;              //Pivot to determine where bullet will instantiate
    [SerializeField] private Transform barrelPivot;              //Pivot to determine the direction where bullet will shot

    private Character player;
    private Animator chrAnim;

    public bool Firing => firing;

    public void Initialize(Character player, Transform arm, Vector3 relPivot)
    {
        this.player = player;
        chrAnim = this.player.GetComponent<Animator>();

        transform.parent = arm;

        Vector3 localPos = relPivot - rightHandPivot.localPosition;
        transform.localPosition = localPos;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        shoot = player.GetAttackKey();
    }

    private float fireDuration = 0f;

    void Update()
    {
        if (fireDuration > fireAnimTime && firing)
        {
            firing = false;
            chrAnim.SetBool("firing", firing);
        }
        
        if (Input.GetKey(shoot) && !isShotOnCooldown && player.Controllable)
        {
            chrAnim.SetBool("fired", true);
            isShotOnCooldown = true;

            firing = true;
            chrAnim.SetBool("firing", firing);
            fireDuration = 0;

            StartCoroutine(onShotCooldown());
        }

        fireDuration += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (chrAnim.GetBool("fired"))
        {
            Shoot();
            chrAnim.SetBool("fired", false);
        }
    }

    private IEnumerator onShotCooldown()
    {
        yield return new WaitForSeconds(shotCooldownTime);
        isShotOnCooldown = false;
    }

    protected abstract void Shoot();

    WaitForSeconds wait = new WaitForSeconds(1 / 30f);
    protected IEnumerator ShootBullet()
    {
        yield return wait;
        Bullet bullet = Instantiate<Bullet>(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        Vector3 direction = bulletPivot.position - barrelPivot.position;
        direction.y = 0;
        //Gun in Stick Fight shoot at the direction related to barrel, but for now we just make it on the x-axis
        direction = direction.normalized;
        bullet.SetVelocity(direction * gunForce);
    }

    //For gun that have spread angle ex.shotgun
    //angle in degree
    protected IEnumerator ShootBullet(float angle)
    {
        yield return wait;
        Bullet bullet = Instantiate<Bullet>(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        angle = Mathf.Deg2Rad * angle;
        Vector2 direction = bulletPivot.position - barrelPivot.position;
        direction.y = 0;
        direction = direction.normalized;
        direction = new Vector3(direction.x * Mathf.Cos(angle), Mathf.Sin(angle), 0);
        bullet.SetVelocity(direction * gunForce);
    }
}
