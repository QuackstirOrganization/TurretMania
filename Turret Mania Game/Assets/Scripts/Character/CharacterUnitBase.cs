using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debugs;

namespace TurretGame
{
    public class CharacterUnitBase : MonoBehaviour
    {
        public Turret turretType;

        //References
        #region Health
        protected Health _Health;
        public Health _health { get { return _Health; } }
        #endregion

        #region Ammo
        protected Ammo _Ammo;
        public Ammo _ammo { get { return _Ammo; } }
        #endregion

        #region Movement
        protected Movement _Movement;
        public Movement _movement { get { return _Movement; } }
        #endregion

        #region RigidBody2D
        protected Rigidbody2D _Rb2D;
        public Rigidbody2D _rb2D { get { return _Rb2D; } }
        #endregion

        //-----Health-----//
        [Header("Health Variables")]
        [SerializeField] protected float InitialHealth;
        public float initialHealth
        {
            get { return InitialHealth; }
            set { InitialHealth = value; }
        }

        [SerializeField] protected float ModifiedHealth;
        public float modifiedHealth
        {
            get { return ModifiedHealth; }
            set { ModifiedHealth = value; }
        }



        //-----Ammo-----//
        [Header("Ammo Variables")]
        [SerializeField] protected int InitialAmmo;
        public int initialAmmo
        {
            get { return InitialAmmo; }
            set { InitialAmmo = value; }
        }

        [SerializeField] protected int ModifiedAmmo;
        public int modifiedAmmo
        {
            get { return ModifiedAmmo; }
            set { ModifiedAmmo = value; }
        }


        //-----Movement-----//
        [Header("Movement Variables")]
        [SerializeField] protected float InitalSpeed;
        public float initalSpeed
        {
            get { return InitalSpeed; }
            set { InitalSpeed = value; }
        }

        [SerializeField] protected float ModifiedSpeed;
        public float modifiedSpeed
        {
            get { return ModifiedSpeed; }
            set { ModifiedSpeed = value; }
        }

        [Space(5)]
        [SerializeField] protected float InitalAcceleration;
        public float initalAcceleration
        {
            get { return InitalAcceleration; }
            set { InitalAcceleration = value; }
        }

        [SerializeField] protected float ModifiedAcceleration;
        public float modifiedAcceleration
        {
            get { return ModifiedAcceleration; }
            set { ModifiedAcceleration = value; }
        }


        [Header("Unity Events")]
        [SerializeField]
        private UnityEvent OnDamagedEvents;

        [SerializeField]
        private UnityEvent OnDeathEvents;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitializeComponents();
            SubscribeEvents();

            if (turretType != null)
            {
                initialHealth = turretType.Health;
                initalSpeed = turretType.movementSpeed;
            }
            InitializeVariables();
        }

        protected virtual void InitializeVariables()
        {
            if (_Health != null)
            {
                _Health.MaxHealth = InitialHealth;
            }

            if (_Ammo != null)
            {
                _Ammo.CurrAmmo = initialAmmo;
            }

            if (_Movement != null)
            {
                _Movement.Speed = initalSpeed;
                _Movement.Acceleration = initalAcceleration;
            }

        }

        protected virtual void InitializeComponents()
        {
            if (this.GetComponent<Health>() != null)
                _Health = this.GetComponent<Health>();
            else
                GlobalDebugs.DebugPM(this, "No Health Component");

            if (this.GetComponent<Ammo>() != null)
                _Ammo = this.GetComponent<Ammo>();
            else
                GlobalDebugs.DebugPM(this, "No Ammo Component");

            if (this.GetComponent<Movement>() != null)
                _Movement = this.GetComponent<Movement>();
            else
                GlobalDebugs.DebugPM(this, "No Movement Component");

            if (this.GetComponent<Rigidbody2D>() != null)
                _Rb2D = this.GetComponent<Rigidbody2D>();
            else
                GlobalDebugs.DebugPM(this, "No Rigidbody2D Component");
        }

        protected virtual void SubscribeEvents()
        {
            if (_Health != null)
            {
                _Health.DeathAction += OnDeath;
                _Health.DamageAction += OnDamage;
            }

        }

        protected virtual void OnDestroy()
        {
            if (_Health != null)
            {
                _Health.DeathAction -= OnDeath;
                _Health.DamageAction -= OnDamage;
            }

            GlobalDebugs.DebugPM(this, "Is Dead");
        }

        protected virtual void OnDamage(float Health)
        {
            OnDamagedEvents.Invoke();
        }

        protected virtual void OnDeath()
        {
            OnDeathEvents.Invoke();
        }
    }
}
