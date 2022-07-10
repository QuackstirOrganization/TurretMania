using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        private Vector2 moveVectorTot;

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

            #endregion
        }

        #region Input Functions

        //Shooting Function
        #region Shooting Function
        void OnPlayerShoot()
        {
            if (PlayerAmmo.CurrAmmo <= 0)
            {
                return;
            }

            PlayerAmmo.CurrAmmo--;
            PlayerShooting.ShootWeapon();

            if (PlayerAmmo.CurrAmmo <= 0)
            {
                PlayerAmmo.RecoilWeapon();
            }
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

            PlayerInput.ShootAction -= OnPlayerShoot;
            PlayerInput.HorizontalMoveAction -= OnPlayerHorizontalMove;
            PlayerInput.VerticalMoveAction -= OnPlayerVerticalMove;
        }

        void OnPlayerDamage(float Health)
        {
            GlobalDebugs.DebugPM(this, "Health: " + Health);
        }
    }
}
