using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;
using Debugs;
using UnityEngine.InputSystem;

namespace TurretGame
{
    public class PlayerUnit : CharacterUnitBase
    {
        private Shoot _PlayerShooting;
        private PlayerInput _playerInput;
        public TurretWeapon[] Weapons;

        [SerializeField] private float ExpNextLevel = 3;
        public float expNextLevel { get { return ExpNextLevel; } }


        [SerializeField] private float CurrExp = 0;
        public float currExp { get { return CurrExp; } set { CurrExp = value; } }


        [SerializeField] private int CurrLevel = 1;
        public int currLevel { get { return CurrLevel; } }

        public Action<float, float> AmmoUIAction;
        public Action<float, float> HealthUIAction;
        public Action<float, float, int> ExpLevelUIAction;

        Vector2 v2_inputLookVector;

        public ItemScriptableObject[] duck;

        public MonoScript quack;

        [SerializeField] private Turret TurretType;
        public Turret turretType
        {
            get { return TurretType; }
            set
            {
                TurretType = value;

                setVariables();
            }
        }


        public SpriteRenderer SR_Turret_Head;
        public SpriteRenderer SR_Turret_Bottom;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //addDuck(quack.GetType());


            if (!collision.CompareTag("PickUp"))
            {
                return;
            }

            CurrExp++;

            if (CurrExp >= ExpNextLevel)
            {
                CurrLevel++;
                CurrExp = 0;
                ExpNextLevel += 2;
                AddItem(1, duck[UnityEngine.Random.Range(0, duck.Length)]);
            }

            Destroy(collision.gameObject);

            UpdateProgressUI();
        }


        protected override void Start()
        {
            base.Start();

            _playerInput = this.GetComponent<PlayerInput>();
            selectWeapon(0);
            setVariables();

            UpdateAmmoUI();
            UpdateHealthUI();
        }


        //Movement Functions
        public void OnPlayerMove(InputAction.CallbackContext context)
        {
            Vector2 InputVector = context.ReadValue<Vector2>();
            Debug.Log("Movement Vector: " + context.phase);
            V_Moving(InputVector);
        }

        public void OnPlayerDash(InputAction.CallbackContext context)
        {
            Debug.Log("Dash: ");
            V_MovementAbility();
        }

        public void OnPlayerShoot(InputAction.CallbackContext context)
        {
            bool a = context.phase != InputActionPhase.Canceled;

            Debug.Log("Shoot: " + a);

            V_ShootAbility(context.phase != InputActionPhase.Canceled);
        }


        public void OnPlayerLook(InputAction.CallbackContext context)
        {
            v2_inputLookVector = context.ReadValue<Vector2>();
            if (_playerInput.currentControlScheme == "KeyboardMouse")
            {
                V_Looking(Camera.main.ScreenToWorldPoint(v2_inputLookVector));
                return;
            }


            if (v2_inputLookVector != Vector2.zero)
            {
                V_Looking((Vector3)v2_inputLookVector + transform.position);
            }
            else
            {
                StartCoroutine(ChangeLooking());
            }
        }

        IEnumerator ChangeLooking()
        {
            while (v2_inputLookVector == Vector2.zero)
            {
                yield return new WaitForFixedUpdate();
                V_Looking((Vector3)v2_InputMoveVector + transform.position);

                if (v2_inputLookVector != Vector2.zero)
                {
                    yield break;
                }

                yield return null;
            }

        }

        //--------------------------------------------------//
        protected override void V_Damage(float Health)
        {
            base.V_Damage(Health);
            UpdateHealthUI();
            GlobalDebugs.DebugPM(this, "Health: " + Health);
        }






        //--------------------------------------------------//
        void reloadingWeapon()
        {
            selectWeapon(UnityEngine.Random.Range(0, Weapons.Length));
            Invoke("UpdateAmmoUI", 0.1f);
        }

        void selectWeapon(int indexWant)
        {
            //_PlayerShooting.ProjectileSpeed = Weapons[indexWant].projectileSpeed;
            //_PlayerShooting.damage = Weapons[indexWant].damage;
            //_PlayerShooting.fireRate = Weapons[indexWant].fireRate;
            //_Ammo.MaxAmmo = Weapons[indexWant].ammo;
        }
        //--------------------------------------------------//




        //--------------------------------------------------//
        public void UpdateHealthUI()
        {
            if (HealthUIAction != null)
                HealthUIAction(CS_HealthVar.GetStat(), _HealthCurr);
        }

        public void UpdateAmmoUI()
        {
            //if (AmmoUIAction != null)
            //AmmoUIAction(_Ammo.MaxAmmo, _Ammo.CurrAmmo);
        }

        public void UpdateProgressUI()
        {
            if (ExpLevelUIAction != null)
                ExpLevelUIAction(ExpNextLevel, CurrExp, CurrLevel);
        }
        //--------------------------------------------------//

        private void setVariables()
        {
            CS_HealthVar.BaseValue = _HealthCurr;
        }
    }
}
