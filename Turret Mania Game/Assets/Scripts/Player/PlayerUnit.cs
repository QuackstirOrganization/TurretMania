using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debugs;

namespace TurretGame
{
    public class PlayerUnit : CharacterUnitBase
    {
        private Shoot PlayerShooting;
        private PlayerInputManager PlayerInput;
        public TurretWeapon[] Weapons;

        private Vector2 moveVectorTot;

        public Action<float, float> AmmoUIAction;
        public Action<float, float> HealthUIAction;

        protected override void Start()
        {
            base.Start();

            //Find player components
            #region Player Components
            PlayerInput = this.GetComponent<PlayerInputManager>();
            PlayerShooting = this.GetComponentInChildren<Shoot>();
            #endregion

            //Subscribe to player events
            #region Events

            //Subscribe to player input events
            PlayerInput.ShootAction += OnPlayerShoot;
            PlayerInput.MoveAction += OnPlayerMove;

            //Subscribe to player shoot events
            PlayerShooting.ShootAction += OnProjectileShot;

            //Subscribe to player ammo events
            _Ammo.ReloadAction += reloadingWeapon;

            #endregion
            selectWeapon(0);
        }

        //--------------------------------------------------//
        //Shooting Functions
        void OnPlayerShoot()
        {
            if (_Ammo.CurrAmmo <= 0)
            {
                return;
            }

            PlayerShooting.isShoot();

            if (_Ammo.CurrAmmo <= 0)
            {
                _Ammo.RecoilWeapon();
            }
        }

        void OnProjectileShot()
        {
            _Ammo.CurrAmmo--;
            UpdateAmmoUI();
        }
        //--------------------------------------------------//

        //Movement Functions
        void OnPlayerMove()
        {
            _Movement.Move(moveVectorTot);
        }

        void OnPlayerMove(float moveVectorX, float moveVectorY)
        {
            moveVectorTot = new Vector2(moveVectorX, moveVectorY);
            OnPlayerMove();
        }

        //--------------------------------------------------//
        protected override void OnDamage(float Health)
        {
            base.OnDamage(Health);
            UpdateHealthUI();
            GlobalDebugs.DebugPM(this, "Health: " + Health);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //Subscribe to player input events
            PlayerInput.ShootAction -= OnPlayerShoot;
            PlayerInput.MoveAction -= OnPlayerMove;

            //Subscribe to player shoot events
            PlayerShooting.ShootAction -= OnProjectileShot;

            //Subscribe to player ammo events
            _Ammo.ReloadAction -= reloadingWeapon;
        }
        //--------------------------------------------------//




        //--------------------------------------------------//
        void reloadingWeapon()
        {
            selectWeapon(UnityEngine.Random.Range(0, Weapons.Length));
            Invoke("UpdateAmmoUI", 0.1f);
        }

        void selectWeapon(int indexWant)
        {
            PlayerShooting.ProjectileSpeed = Weapons[indexWant].projectileSpeed;
            PlayerShooting.damage = Weapons[indexWant].damage;
            PlayerShooting.fireRate = Weapons[indexWant].fireRate;
            _Ammo.MaxAmmo = Weapons[indexWant].ammo;
        }
        //--------------------------------------------------//




        //--------------------------------------------------//
        public void UpdateHealthUI()
        {
            if (HealthUIAction != null)
                HealthUIAction(_Health.MaxHealth, _Health.CurrHealth);
        }
        public void UpdateAmmoUI()
        {
            if (AmmoUIAction != null)
                AmmoUIAction(_Ammo.MaxAmmo, _Ammo.CurrAmmo);
        }
        //--------------------------------------------------//
    }
}
