using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debugs;

namespace TurretGame
{
    public class CharacterUnitBase : MonoBehaviour
    {
        protected Health _Health;
        protected Ammo _Ammo;
        protected Movement _Movement;

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
