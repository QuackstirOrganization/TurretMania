using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform mousePos;
    public Transform BulletOrigin;
    private AudioSource AS;
    public AudioClip ShootSFX;


    [Header("Projectile")]
    public GameObject Projectile;
    public float ProjectileSpeed;
    public float damage;

    public Vector3 angle;

    public event Action ShootAction;

    public float fireRate;
    private float lastTimeFired;

    public bool isShooting;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void isShoot()
    {
        if (Time.time - lastTimeFired > 1 / fireRate)
        {
            lastTimeFired = Time.time;
            ShootWeapon();
        }
    }

    public void ShootWeapon()
    {
        ShootAction();

        angle = (this.transform.position - mousePos.position);

        GameObject ProjectileShot = Instantiate(Projectile, BulletOrigin.position, Quaternion.identity);
        ProjectileShot.GetComponent<Rigidbody2D>().velocity = -transform.up * ProjectileSpeed;
        ProjectileShot.GetComponent<DamageEnemy>().Damage = damage;
        ProjectileShot.transform.rotation = transform.rotation;

        AS.PlayOneShot(ShootSFX);
    }


}
