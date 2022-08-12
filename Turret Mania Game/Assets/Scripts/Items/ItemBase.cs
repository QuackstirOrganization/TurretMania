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
        [SerializeField] protected int StackAmount = 500;

        [SerializeField] protected SlopeType slopeType;
        [SerializeField] protected float initialVal;
        [SerializeField] protected float SlopeIncrease = 0.5f;
        [SerializeField] protected bool increasePercent = true;
        [SerializeField] protected ItemScriptableObject _scriptableObject;
        protected Rarity rarity;

        public float currentMultiplier;

        protected virtual void Start()
        {
            InitializeValues();
            UpdateEffects();
        }

        protected void InitializeValues()
        {
            slopeType = _scriptableObject.slopeType;
            initialVal = _scriptableObject.initialVal;
            SlopeIncrease = _scriptableObject.SlopeIncrease;
            increasePercent = _scriptableObject.increasePercent;
        }

        public virtual void GetCharacterUnit(CharacterUnitBase characterUnit)
        {
            _characterUnit = characterUnit;
            GlobalDebugs.DebugPM(this, "Item Base Initialized");
        }

        protected virtual void ProcEffects()
        {
            GlobalDebugs.DebugPM(this, "Item Base Proc effect");

        }

        protected virtual void RemoveEffects()
        {
            GlobalDebugs.DebugPM(this, "Item Base Proc removed");

        }

        protected virtual void UpdateEffects()
        {
            GlobalDebugs.DebugPM(this, "Item Base Proc updated");

        }

        protected virtual void updateMultipler(SlopeType SlopeWant, float initialValue, float increaseRate, bool IncreasePercentage)
        {
            currentMultiplier = pickSlope(SlopeWant, initialValue, increaseRate, IncreasePercentage);

        }

        public virtual void IncreaseStackAmt(int IncreaseBy)
        {
            GlobalDebugs.DebugPM(this, "Current stack amount is " + StackAmount);
            StackAmount += IncreaseBy;
            UpdateEffects();
        }

        protected virtual float pickSlope(SlopeType SlopeWant, float initialValue, float increaseRate, bool IncreasePercentage)
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

        protected virtual float linearSlope(float initalValue, float increaseRate)
        {
            return initalValue * increaseRate * StackAmount;
        }

        protected virtual float exponentialSlope(float initalValue, float increaseRate)
        {
            return initalValue + Mathf.Pow(StackAmount, StackAmount);
        }

        protected virtual float hyperbolicSlope(float initalValue, float increaseRate)
        {
            return (((increaseRate * StackAmount) + 1) / 1) - initalValue;
        }
    }
}
