using System;
using System.Collections.Generic;
using System.Collections;
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

        public Action<bool> A_ShootAbility;
        public CharacterStat CS_DamageBase = new CharacterStat(5, 4, true);

        public CharacterStat CS_HealthVar = new CharacterStat(5, 4, true);
        private float f_HealthCurr;
        public float _HealthCurr
        {
            get { return f_HealthCurr; }
        }
        public Action<float> A_Damage;
        public Action<float> A_Death;

        public CharacterStat CS_MovementVar = new CharacterStat(5, 5, false);
        public float f_Acceleration;
        public Action A_MovementAbility;
        protected Vector2 v2_InputMoveVector;
        private bool isMoving = false;

        public Action<Vector3> A_Looking;



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

            InitializeVariables();
        }

        protected virtual void InitializeVariables()
        {
            f_HealthCurr = CS_HealthVar.BaseValue;
        }

        protected virtual void InitializeComponents()
        {
            if (this.GetComponent<Rigidbody2D>() != null)
                _Rb2D = this.GetComponent<Rigidbody2D>();
            else
                GlobalDebugs.DebugPM(this, "No Rigidbody2D Component");
        }

        protected virtual void UpdateVariables()
        {
            CS_HealthVar.UpdateStat();
            f_HealthCurr = CS_HealthVar.GetStat();
        }

        //----------SHOOTING FUNCTIONS----------//
        protected virtual void V_ShootAbility(bool b_activation)
        {
            if (A_ShootAbility != null)
                A_ShootAbility(b_activation);
        }


        //----------MOVEMENT FUNCTIONS----------//
        protected virtual void V_Moving(Vector2 MoveVector)
        {
            v2_InputMoveVector = MoveVector;

            if (!isMoving)
                StartCoroutine(IE_AccelerateMovement());
        }

        IEnumerator IE_AccelerateMovement()
        {
            isMoving = true;

            while (v2_InputMoveVector != Vector2.zero)
            {
                yield return new WaitForFixedUpdate();
                Debug.DrawLine(transform.position, (Vector3)v2_InputMoveVector + transform.position);
                _Rb2D.velocity = Vector2.Lerp(_Rb2D.velocity, v2_InputMoveVector * CS_MovementVar.GetStat(), Time.deltaTime * f_Acceleration);

                yield return null;
            }

            if (v2_InputMoveVector == Vector2.zero)
            {
                StartCoroutine(IE_SlowMovement());
                yield break;
            }
        }

        IEnumerator IE_SlowMovement()
        {
            isMoving = false;

            while (_Rb2D.velocity.magnitude > 0)
            {
                yield return new WaitForFixedUpdate();
                _Rb2D.velocity = Vector2.Lerp(_Rb2D.velocity, Vector2.zero, Time.deltaTime * f_Acceleration);
                if (v2_InputMoveVector != Vector2.zero)
                {
                    yield break;
                }
                yield return null;
            }
        }

        protected virtual void V_Looking(Vector3 LookVector)
        {
            if (A_Looking != null)
                A_Looking(LookVector);
        }

        protected virtual void V_MovementAbility()
        {
            if (A_MovementAbility != null)
                A_MovementAbility();
        }


        //----------HEALTH FUNCTIONS----------//
        protected virtual void V_Healing(float Heal)
        {
            f_HealthCurr += Heal;

            if (f_HealthCurr > CS_HealthVar.GetStat())
            {
                f_HealthCurr = CS_HealthVar.GetStat();
            }
        }

        protected virtual void V_Damage(float Damage)
        {
            f_HealthCurr -= Damage;

            if (A_Damage != null)
                A_Damage(Damage);

            if (f_HealthCurr < 0)
                V_Death();

            OnDamagedEvents.Invoke();
        }

        protected virtual void V_Death()
        {
            OnDeathEvents.Invoke();

            if (itemcrap != null)
                itemcrap.text = getItemRarityAMT(Rarity.Common).ToString();
        }




        //----------ITEM FUNCTIONS----------//
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

            UpdateVariables();
        }


    }
}
