using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedIncrease : ItemBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            ProcEffects();
        }

        protected override void ProcEffects()
        {
            base.ProcEffects();

            //_characterUnit.modifiedSpeed += currentMultiplier;
        }

        protected override void UpdateEffects()
        {
            //_characterUnit.modifiedSpeed -= currentMultiplier;
            //updateMultipler(slopeType, _characterUnit.initalSpeed, SlopeIncrease, increasePercent);
            ProcEffects();
        }
    }
}
