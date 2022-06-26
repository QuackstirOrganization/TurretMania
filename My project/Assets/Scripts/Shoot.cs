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

    public Vector3 angle;

    public Ammo ammoThing;

    public event Action<int> ShootAction;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void ShootWeapon()
    {
        ShootAction(ammoThing.CurrAmmo);

        angle = (this.transform.position - mousePos.position);

        GameObject ProjectileShot = Instantiate(Projectile, BulletOrigin.position, Quaternion.identity);
        ProjectileShot.GetComponent<Rigidbody2D>().velocity = -transform.up * ProjectileSpeed;
        ProjectileShot.transform.rotation = transform.rotation;

        AS.PlayOneShot(ShootSFX);
    }


}
