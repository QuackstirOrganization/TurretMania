using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Debugs;

namespace TurretGame
{
    public abstract class CharacterUnitBase : MonoBehaviour
    {
        public Text itemcrap;
        public Text speed;
        public Image speedslider;

        public Turret turretType;

        public ItemScriptableObject item;
        public ItemScriptableObject item1;

        public bool thing = false;

        protected Dictionary<string, ItemBase> ItemsDictionary = new Dictionary<string, ItemBase>();
        protected Dictionary<Rarity, int> ItemRarityAmountDictionary = new Dictionary<Rarity, int>();

        public int getItemRarityAMT(Rarity rarity)
        {
            int value;
            ItemRarityAmountDictionary.TryGetValue(rarity, out value);
            return value;
        }

        public Action<int> AddItemAction;

        #region Health
        protected Health _Health;
        public Health _health { get { return _Health; } }

        //-----Health-----//
        [Header("Health Variables")]
        [SerializeField] protected float InitialHealth;
        public float initialHealth
        {
            get { return InitialHealth; }
        }

        protected float ModifiedHealth;
        public float modifiedHealth
        {
            get { return ModifiedHealth; }
            set
            {
                ModifiedHealth = value;
                _Health.CurrHealth = value;
                GlobalDebugs.DebugPM(this, "Health is now" + value);
            }
        }
        #endregion

        #region Ammo
        protected Ammo _Ammo;
        public Ammo _ammo { get { return _Ammo; } }

        //-----Ammo-----//
        [Header("Ammo Variables")]
        [SerializeField] protected int InitialAmmo;
        public int initialAmmo
        {
            get { return InitialAmmo; }
        }

        protected int ModifiedAmmo;
        public int modifiedAmmo
        {
            get { return ModifiedAmmo; }
            set
            {
                ModifiedAmmo = value;
                if (_Ammo != null)
                    _Ammo.CurrAmmo = ModifiedAmmo;
                GlobalDebugs.DebugPM(this, "Ammo is now " + value);
            }
        }
        #endregion

        #region Movement
        protected Movement _Movement;
        public Movement _movement { get { return _Movement; } }

        //-----Movement-----//
        [Header("Movement Variables")]
        [SerializeField] protected float InitialSpeed;
        public float initalSpeed
        {
            get { return InitialSpeed; }
        }

        protected float ModifiedSpeed;
        public float modifiedSpeed
        {
            get { return ModifiedSpeed; }
            set
            {
                ModifiedSpeed = value;
                if (_Movement != null)
                    _Movement.Speed = value;
                GlobalDebugs.DebugPM(this, "Movement speed is now " + value);
            }
        }

        [Space(5)]
        [SerializeField] protected float InitalAcceleration;
        public float initalAcceleration
        {
            get { return InitalAcceleration; }
        }

        protected float ModifiedAcceleration;
        public float modifiedAcceleration
        {
            get { return ModifiedAcceleration; }
            set
            {
                ModifiedAcceleration = value;
                if (_Movement != null)
                    _Movement.Acceleration = value;
                GlobalDebugs.DebugPM(this, "Movement acceleration is now " + value);
            }
        }

        #endregion

        #region RigidBody2D
        protected Rigidbody2D _Rb2D;
        public Rigidbody2D _rb2D { get { return _Rb2D; } }
        #endregion

        [Header("Unity Events")]
        [SerializeField]
        private UnityEvent OnDamagedEvents;

        [SerializeField]
        private UnityEvent OnDeathEvents;

        private void Update()
        {
            if (!thing)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                AddItem(1, item);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                AddItem(1, item1);
            }

            if (speed != null)
            {
                speed.text = _Rb2D.velocity.magnitude.ToString("F2");
                speedslider.fillAmount = Mathf.Lerp(speedslider.fillAmount, _Rb2D.velocity.magnitude / 100, Time.deltaTime);
            }
        }


        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitializeComponents();
            SubscribeEvents();

            if (turretType != null)
            {
                InitialHealth = turretType.Health;
                InitialSpeed = turretType.movementSpeed;
            }
            InitializeVariables();
        }

        protected virtual void InitializeVariables()
        {
            if (_Health != null)
            {
                _Health.MaxHealth = InitialHealth;
                ModifiedHealth = InitialHealth;
            }

            if (_Ammo != null)
            {
                _Ammo.CurrAmmo = InitialAmmo;
                modifiedAmmo = InitialAmmo;
            }

            if (_Movement != null)
            {
                _Movement.Speed = InitialSpeed;
                _Movement.Acceleration = InitalAcceleration;

                ModifiedSpeed = InitialSpeed;
                ModifiedAcceleration = InitalAcceleration;
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

            if (itemcrap != null)
                itemcrap.text = getItemRarityAMT(Rarity.Common).ToString();
        }

        public void AddItem(int AmtAdd, ItemScriptableObject Item)
        {
            if (ItemsDictionary.ContainsKey(Item.Name))
            {
                ItemsDictionary[Item.Name].IncreaseStackAmt(AmtAdd);
            }
            else
            {
                GameObject newItem = Instantiate(Item.ItemPrefab);
                newItem.transform.parent = this.transform;
                ItemsDictionary.Add(Item.Name, newItem.GetComponent<ItemBase>());
                newItem.GetComponent<ItemBase>().GetCharacterUnit(this);
            }

            if (ItemRarityAmountDictionary.ContainsKey(Item.itemRarity))
            {
                ItemRarityAmountDictionary[item.itemRarity] += AmtAdd;
            }
            else
            {
                ItemRarityAmountDictionary.Add(item.itemRarity, AmtAdd);
            }

            if (AddItemAction != null)
                AddItemAction(AmtAdd);
        }
    }
}
