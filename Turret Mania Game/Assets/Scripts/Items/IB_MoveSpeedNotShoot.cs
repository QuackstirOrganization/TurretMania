using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedNotShoot : ItemBase
    {
        private PlayerInputManager playerInputManager;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            if (_characterUnit.GetComponent<PlayerInputManager>() != null)
            {
                playerInputManager = _characterUnit.GetComponent<PlayerInputManager>();

                playerInputManager.ShootActionDown += ProcEffects;
                playerInputManager.ShootActionUp += RemoveEffects;

                RemoveEffects();
            }
        }

        private void OnDestroy()
        {
            if (_characterUnit.GetComponent<PlayerInputManager>() != null)
            {
                playerInputManager.ShootActionDown -= ProcEffects;
                playerInputManager.ShootActionUp -= RemoveEffects;
            }
        }

        protected override void ProcEffects()
        {
            base.ProcEffects();

            _characterUnit.modifiedSpeed -= currentMultiplier;
        }

        protected override void RemoveEffects()
        {
            base.RemoveEffects();

            _characterUnit.modifiedSpeed += currentMultiplier;
        }

        protected override void UpdateEffects()
        {
            _characterUnit.modifiedSpeed -= currentMultiplier;
            updateMultipler(slopeType, _characterUnit.initalSpeed, SlopeIncrease, increasePercent);
        }
    }
}
