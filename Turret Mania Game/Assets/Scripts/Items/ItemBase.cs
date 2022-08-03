using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugs;

namespace TurretGame
{
    public enum Rarity
    {
        Common, Rare, Legendary
    }

    public enum SlopeType
    {
        Linear, Exponential, Hyperbolic
    }


    public abstract class ItemBase : MonoBehaviour
    {
        protected CharacterUnitBase _characterUnit;
        [SerializeField] protected int StackAmount = 1;

        [SerializeField] protected SlopeType slopeType;
        [SerializeField] protected float initialVal;
        [SerializeField] protected float SlopeIncrease = 0.5f;
        [SerializeField] protected bool increasePercent = true;
        protected Rarity rarity;

        public virtual void GetCharacterUnit(CharacterUnitBase characterUnit)
        {
            _characterUnit = characterUnit;
        }


        protected virtual void Start()
        {
            GetCharacterUnit(this.GetComponent<CharacterUnitBase>());
            GlobalDebugs.DebugPM(this, "Item Base Initialized");
        }

        protected virtual void ProcEffects()
        {

        }

        protected virtual void RemoveEffects()
        {

        }

        public virtual void IncreaseStackAmt(int IncreaseBy)
        {
            StackAmount += IncreaseBy;
            UpdateItem(slopeType, initialVal, SlopeIncrease, increasePercent);
        }

        protected virtual void UpdateItem(SlopeType SlopeWant, float initialValue, float increaseRate, bool IncreasePercentage)
        {
            pickSlope(SlopeWant, initialValue, increaseRate, IncreasePercentage);
        }

        float pickSlope(SlopeType SlopeWant, float initialValue, float increaseRate, bool IncreasePercentage)
        {
            increaseRate = IncreasePercentage ? (increaseRate * 0.01f * initialValue) : increaseRate;

            switch (SlopeWant)
            {
                case SlopeType.Linear:
                    return linearSlope(initialValue, increaseRate);
                case SlopeType.Exponential:
                    return exponentialSlope(initialValue, increaseRate);
                case SlopeType.Hyperbolic:
                    return hyperbolicSlope(initialValue, increaseRate);
                default:
                    return linearSlope(initialValue, increaseRate);
            }
        }



        float linearSlope(float initalValue, float increaseRate)
        {
            return initalValue + increaseRate * StackAmount;
        }

        float exponentialSlope(float initalValue, float increaseRate)
        {
            return initalValue + Mathf.Pow(StackAmount, StackAmount);
        }

        float hyperbolicSlope(float initalValue, float increaseRate)
        {
            return (((increaseRate * StackAmount) + 1) / 1) - initalValue;
        }
    }
}
