using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedNoTurret : ItemBase
    {
        private Ammo _ammo;

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

            _characterUnit._movement.Speed = _characterUnit.baseSpeed + (currentMultiplier - _characterUnit.initalSpeed);
        }

        protected override void RemoveEffects()
        {
            base.RemoveEffects();

            _characterUnit._movement.Speed = _characterUnit.baseSpeed;
        }

        protected override void UpdateEffects()
        {
            updateMultipler(slopeType, _characterUnit.initalSpeed, SlopeIncrease, increasePercent);
            RemoveEffects();
        }

    }
}
