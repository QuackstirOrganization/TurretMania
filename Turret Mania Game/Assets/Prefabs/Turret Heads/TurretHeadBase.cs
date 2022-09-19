using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TurretGame
{
    public class TurretHeadBase : MonoBehaviour
    {
        public float GunRecoilSpeed;
        public bool canPickUp = false;
        bool GunExist = false;

        private PlayerUnit characterBase;
        public TurretWeapon[] tw;
        public List<GameObject> turretDeckRand = new List<GameObject>();
        private int i_selectedWeapon = 0;


        [Header("SFX")]
        public AudioClip GunLossSFX;
        public AudioClip ReloadSFX;

        [Header("Game Objects")]
        private SpriteRenderer TurretSprite;
        public WeaponBase currWeapon;

        [Header("References")]
        private Rigidbody2D Rb2D;
        private AudioSource AS;

        //[Header("Events")]
        public Action ReloadAction;
        public Action ThrowAction;


        public int rotationOffset = 90;
        private float rotZ;

        Vector3 difference;

        void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            AS = GetComponent<AudioSource>();

            TurretSprite = GetComponent<SpriteRenderer>();


            InitializeTurretHead();
            createTurretWeapons();
        }

        void InitializeTurretHead()
        {
            TurretSprite.enabled = false;

            if (GetComponentInParent<PlayerUnit>() != null)
                characterBase = GetComponentInParent<PlayerUnit>();

            characterBase.A_ShootAbility += RecoilWeapon;
            characterBase.A_Looking += rotateHead;

            characterBase.SR_Turret_Head.enabled = true;
        }

        void createTurretWeapons()
        {
            foreach (var item in tw)
            {
                GameObject newWeapon = Instantiate(item.WeaponObject, Vector3.zero, Quaternion.identity, this.transform);
                newWeapon.GetComponent<WeaponBase>().TW_weaponStats = item;
                newWeapon.GetComponent<WeaponBase>().initializeVariables();
                for (int i = 0; i < item.frequency; i++)
                {
                    turretDeckRand.Add(newWeapon);
                }
                newWeapon.SetActive(false);
            }
            Shuffle();
            turretDeckRand[i_selectedWeapon].SetActive(true);
            currWeapon = turretDeckRand[i_selectedWeapon].GetComponent<WeaponBase>();
        }

        void Shuffle()
        {
            for (int i = 0; i < turretDeckRand.Count; i++)
            {
                int rand = UnityEngine.Random.Range(0, turretDeckRand.Count);
                GameObject temp = turretDeckRand[i];

                turretDeckRand[i] = turretDeckRand[rand];
                turretDeckRand[rand] = temp;
            }
        }

        void selectWeapon()
        {
            i_selectedWeapon++;

            if (i_selectedWeapon >= turretDeckRand.Count)
            {
                Shuffle();
                i_selectedWeapon = 0;
            }

            turretDeckRand[i_selectedWeapon].SetActive(true);
            currWeapon = turretDeckRand[i_selectedWeapon].GetComponent<WeaponBase>();
        }

        void RemoveTurretHead()
        {
            TurretSprite.enabled = true;
            characterBase.SR_Turret_Head.enabled = false;
            characterBase.A_ShootAbility -= RecoilWeapon;
            characterBase.A_Looking -= rotateHead;

            turretDeckRand[i_selectedWeapon].SetActive(false);

            characterBase = null;
        }

        private void rotateHead(Vector3 V2_Rotation)
        {
            difference = V2_Rotation - transform.position;
            difference.Normalize(); // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
            currWeapon.V_ChangeWeaponSprite(rotZ);
        }

        public void RecoilWeapon(InputActionPhase firingPhase)
        {

            if (currWeapon.CurrAmmo > 0)
            {
                currWeapon.V_ShootWeapon(firingPhase != InputActionPhase.Canceled);
                return;
            }

            if (GunExist)
            {
                return;
            }

            if (firingPhase == InputActionPhase.Started)
            {
                GunExist = true;

                transform.SetParent(null);

                AS.PlayOneShot(GunLossSFX);

                Rb2D.bodyType = RigidbodyType2D.Dynamic;

                Rb2D.velocity = (Vector3)characterBase._rb2D.velocity + -transform.up * GunRecoilSpeed;

                transform.rotation = Quaternion.identity;

                if (ThrowAction != null)
                    ThrowAction();

                RemoveTurretHead();

                canPickUp = false;
                Invoke("PickUp", 0.5f);
            }
        }

        void PickUp()
        {
            canPickUp = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canPickUp)
            {
                return;
            }

            if (collision.CompareTag("Player"))
            {
                if (ReloadAction != null)
                    ReloadAction();

                AS.PlayOneShot(ReloadSFX);

                transform.SetParent(collision.transform);
                Rb2D.bodyType = RigidbodyType2D.Kinematic;
                Rb2D.velocity = Vector2.zero;

                transform.localPosition = Vector3.zero;

                GunExist = false;

                InitializeTurretHead();
                selectWeapon();
            }
        }
    }
}
