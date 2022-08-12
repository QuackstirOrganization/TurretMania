using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedNoTurret : ItemBase
    {
        private Ammo _ammo;
        bool isApplied = false;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            if (_characterUnit.GetComponent<Ammo>() != null)
            {
                _ammo = _characterUnit.GetComponent<Ammo>();

                _ammo.ReloadAction += RemoveEffects;
                _ammo.ThrowAction += ProcEffects;
            }
        }

        private void OnDestroy()
        {
            if (_characterUnit.GetComponent<PlayerInputManager>() != null)
            {
                _ammo.ReloadAction -= RemoveEffects;
                _ammo.ThrowAction -= ProcEffects;
            }
        }


        protected override void ProcEffects()
        {
            base.ProcEffects();

            _characterUnit.modifiedSpeed += currentMultiplier;
            isApplied = true;
        }

        protected override void RemoveEffects()
        {
            if (!isApplied)
            {
                return;
            }

            base.RemoveEffects();

            _characterUnit.modifiedSpeed -= currentMultiplier;

            isApplied = false;
        }

        protected override void UpdateEffects()
        {
            updateMultipler(slopeType, _characterUnit.initalSpeed, SlopeIncrease, increasePercent);
        }

    }
}
