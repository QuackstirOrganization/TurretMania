using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedIncrease : ItemBase
    {
        private float initialSpeed;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            StackAmount = 10;
            initialSpeed = _characterUnit.initalSpeed;
            ProcEffects();
        }

        protected override void ProcEffects()
        {
            base.ProcEffects();

            _characterUnit.modifiedSpeed = _characterUnit.modifiedSpeed + increase();
        }

        float increase()
        {
            return initialSpeed * SlopeIncrease * StackAmount;
        }

    }
}
