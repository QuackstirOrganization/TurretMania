using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class IB_MoveSpeedNotShoot : ItemBase
    {
        private float initialSpeed;
        private PlayerInputManager playerInputManager;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            StackAmount = 3;

            initialSpeed = _characterUnit.initalSpeed;

            if (GetComponent<PlayerInputManager>() != null)
            {
                playerInputManager = GetComponent<PlayerInputManager>();

                playerInputManager.ShootActionDown += ProcEffects;
                playerInputManager.ShootActionUp += RemoveEffects;

                RemoveEffects();
            }
        }

        private void OnDestroy()
        {
            if (GetComponent<PlayerInputManager>() != null)
            {
                playerInputManager.ShootActionDown -= ProcEffects;
                playerInputManager.ShootActionUp -= RemoveEffects;
            }
        }

        protected override void ProcEffects()
        {
            base.ProcEffects();

            _characterUnit._movement.Speed = _characterUnit.modifiedSpeed;
        }

        protected override void RemoveEffects()
        {
            base.RemoveEffects();

            _characterUnit._movement.Speed = _characterUnit.modifiedSpeed + increase();
        }

        float increase()
        {
            return initialSpeed * SlopeIncrease * StackAmount;
        }
    }
}
