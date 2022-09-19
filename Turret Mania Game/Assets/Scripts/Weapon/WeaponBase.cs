using System;
using System.Collections;
using UnityEngine;

namespace TurretGame
{
    public class WeaponBase : MonoBehaviour
    {
        //private CharacterUnitBase characterBase;

        public int rotationOffset = 90;
        private float rotZ;

        private SpriteRenderer spriteRenderer;

        public Sprite Up;
        public Sprite Down;

        public TurretWeapon TW_weaponStats;


        //----------SHOOT VARIABLES----------//
        [Header("Shoot stuff")]
        public Transform BulletOrigin;
        private AudioSource AS;
        public AudioClip ShootSFX;


        [Header("Projectile")]
        public GameObject Projectile;
        public float ProjectileSpeed;
        public float damage;

        public Action A_Shoot;

        public float fireRate;
        private float lastTimeFired;

        public bool isShooting;

        //----------AMMO VARIABLES----------//
        [Header("Ammo")]
        public int CurrAmmo;



        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            AS = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (TW_weaponStats != null)
                initializeVariables();
        }

        public void initializeVariables()
        {
            damage = TW_weaponStats.damage;
            fireRate = TW_weaponStats.fireRate;
            ProjectileSpeed = TW_weaponStats.projectileSpeed;
            CurrAmmo = TW_weaponStats.ammo;
        }

        public void V_ShootWeapon(bool b_Activated)
        {
            isShooting = b_Activated;
            StartCoroutine(IE_Shooting());

            if (CurrAmmo <= 0)
            {
                return;
            }
        }


        IEnumerator IE_Shooting()
        {
            while (isShooting && CurrAmmo > 0)
            {
                yield return new WaitForFixedUpdate();
                if (Time.time - lastTimeFired > 1 / fireRate)
                {
                    lastTimeFired = Time.time;
                    ShootWeapon();
                    CurrAmmo--;
                }
                yield return null;
            }
        }

        public void ShootWeapon()
        {
            if (A_Shoot != null)
                A_Shoot();

            GameObject ProjectileShot = Instantiate(Projectile, BulletOrigin.position, Quaternion.identity);
            ProjectileShot.GetComponent<Rigidbody2D>().velocity = -transform.up * ProjectileSpeed;
            ProjectileShot.GetComponent<DamageEnemy>().Damage = damage;
            ProjectileShot.transform.rotation = transform.rotation;

            AS.PlayOneShot(ShootSFX);
        }



        public void V_ChangeWeaponSprite(float f_Rotation)
        {
            rotZ = f_Rotation;
            Debug.Log("ROtation of head: " + rotZ);
            ChangeTurret(0, 180, Up);
            ChangeTurret(-180, 0, Down);
        }

        void ChangeTurret(float RotationFrom, float RotationTo, Sprite ChangeTo)
        {
            if (rotZ > RotationFrom && rotZ < RotationTo)
            {
                spriteRenderer.sprite = ChangeTo;
            }
        }
    }
}
