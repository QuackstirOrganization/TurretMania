using System;
using UnityEngine;
using Debugs;

namespace TurretGame
{
    public class PlayerInputManager : MonoBehaviour
    {
        #region Input
        public string ShootInput;
        public string HorizontalMoveInput;
        public string VerticalMoveInput;
        #endregion

        #region Events
        public event Action ShootAction;
        public event Action<float> HorizontalMoveAction;
        public event Action<float> VerticalMoveAction;
        #endregion

        // Update is called once per frame
        void Update()
        {
            HorizontalMoveAction(Input.GetAxisRaw(HorizontalMoveInput));
            VerticalMoveAction(Input.GetAxisRaw(VerticalMoveInput));

            if (Input.GetButton(ShootInput))
            {
                ShootAction();
            }

            //if (Input.GetButtonDown(ShootInput))
            //{
            //    isShooting = true;
            //}

            //if (Input.GetButtonUp(ShootInput))
            //{
            //    isShooting = false;

            //}
        }
    }
}
