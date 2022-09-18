using System;
using System.Collections;
using UnityEngine;

namespace TurretGame
{
    public class WeaponBase : MonoBehaviour
    {
        private CharacterUnitBase characterBase;

        public int rotationOffset = 90;
        private float rotZ;

        private SpriteRenderer spriteRenderer;

        public Sprite Up;
        public Sprite Down;


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
            characterBase = GetComponentInParent<CharacterUnitBase>();
            AS = GetComponent<AudioSource>();
            characterBase.A_ShootAbility += V_ShootWeapon;
            characterBase.A_Looking += V_RotateWeapon;
        }

        private void OnDisable()
        {
            characterBase.A_ShootAbility -= V_ShootWeapon;
            characterBase.A_Looking -= V_RotateWeapon;
        }

        void V_ShootWeapon(bool b_Activated)
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



        void V_RotateWeapon(Vector3 V2_Rotation)
        {
            Vector3 difference = V2_Rotation - transform.position;
            difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);


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
