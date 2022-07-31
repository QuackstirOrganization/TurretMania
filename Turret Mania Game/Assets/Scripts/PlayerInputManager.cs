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
        public event Action ShootActionDown;
        public event Action ShootActionUp;
        public event Action<float, float> MoveAction;
        #endregion

        // Update is called once per frame
        void Update()
        {
            MoveAction(Input.GetAxisRaw(HorizontalMoveInput),
                Input.GetAxisRaw(VerticalMoveInput));

            
            if (Input.GetButtonDown(ShootInput) && ShootActionDown != null)
            {
                ShootActionDown();
            }
            else if (Input.GetButtonUp(ShootInput) && ShootActionUp != null)
            {
                ShootActionUp();
            }
            else if (Input.GetButton(ShootInput) && ShootAction != null)
            {
                ShootAction();
            }
        }
    }
}
