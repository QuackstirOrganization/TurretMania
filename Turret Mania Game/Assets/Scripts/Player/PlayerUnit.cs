using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debugs;

namespace TurretGame
{
    public class PlayerUnit : MonoBehaviour
    {
        private Health PlayerHealth;
        private Ammo PlayerAmmo;
        private Shoot PlayerShooting;
        private Movement PlayerMovement;
        private PlayerInputManager PlayerInput;
        public TurretWeapon[] Weapons;

        private Vector2 moveVectorTot;

        public Text UI_playerAmmo;

        void Start()
        {

            //Find player components
            #region Player Components
            PlayerHealth = this.GetComponent<Health>();
            PlayerAmmo = this.GetComponent<Ammo>();
            PlayerMovement = this.GetComponent<Movement>();
            PlayerInput = this.GetComponent<PlayerInputManager>();
            PlayerShooting = this.GetComponentInChildren<Shoot>();
            #endregion

            //Subscribe to player events
            #region Events

            //Subscribe to player health events
            PlayerHealth.DeathAction += OnPlayerDeath;
            PlayerHealth.DamageAction += OnPlayerDamage;

            //Subscribe to player input events
            PlayerInput.ShootAction += OnPlayerShoot;
            PlayerInput.HorizontalMoveAction += OnPlayerHorizontalMove;
            PlayerInput.VerticalMoveAction += OnPlayerVerticalMove;

            //Subscribe to player shoot events
            PlayerShooting.ShootAction += OnProjectileShot;

            //Subscribe to player ammo events
            PlayerAmmo.ReloadAction += reloadingWeapon;

            #endregion
            selectWeapon(0);
        }

        #region Input Functions

        //Shooting Function
        #region Shooting Functions
        void OnPlayerShoot()
        {
            if (PlayerAmmo.CurrAmmo <= 0)
            {
                return;
            }

            PlayerShooting.isShoot();

            if (PlayerAmmo.CurrAmmo <= 0)
            {
                PlayerAmmo.RecoilWeapon();
            }
        }

        void OnProjectileShot()
        {
            PlayerAmmo.CurrAmmo--;
            UpdateAmmoUI();
        }

        void UpdateAmmoUI()
        {
            UI_playerAmmo.text = "" + PlayerAmmo.CurrAmmo;
        }
        #endregion


        //Movement Function
        #region Movement Functions
        void OnPlayerMove()
        {
            PlayerMovement.Move(moveVectorTot);
        }

        void OnPlayerHorizontalMove(float moveVector)
        {
            moveVectorTot.x = moveVector;
            OnPlayerMove();
        }

        void OnPlayerVerticalMove(float moveVector)
        {
            moveVectorTot.y = moveVector;
            OnPlayerMove();
        }
        #endregion

        #endregion

        void OnPlayerDeath()
        {
            GlobalDebugs.DebugPM(this, "Is Dead");
        }

        private void OnDestroy()
        {
            //Unsubscribe from all events
            PlayerHealth.DeathAction -= OnPlayerDeath;
            PlayerHealth.DamageAction -= OnPlayerDamage;

            //Subscribe to player input events
            PlayerInput.ShootAction -= OnPlayerShoot;
            PlayerInput.HorizontalMoveAction -= OnPlayerHorizontalMove;
            PlayerInput.VerticalMoveAction -= OnPlayerVerticalMove;

            //Subscribe to player shoot events
            PlayerShooting.ShootAction -= OnProjectileShot;

            //Subscribe to player ammo events
            PlayerAmmo.ReloadAction -= reloadingWeapon;
        }

        void OnPlayerDamage(float Health)
        {
            GlobalDebugs.DebugPM(this, "Health: " + Health);
        }

        void reloadingWeapon()
        {
            selectWeapon(Random.Range(0, Weapons.Length));
            Invoke("UpdateAmmoUI", 0.1f);
        }

        void selectWeapon(int indexWant)
        {
            PlayerShooting.ProjectileSpeed = Weapons[indexWant].projectileSpeed;
            PlayerShooting.damage = Weapons[indexWant].damage;
            PlayerShooting.fireRate = Weapons[indexWant].fireRate;
            PlayerAmmo.MaxAmmo = Weapons[indexWant].ammo;
        }
    }
}
