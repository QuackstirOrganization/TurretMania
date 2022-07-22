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
        public event Action<float, float> MoveAction;
        #endregion

        // Update is called once per frame
        void Update()
        {
            MoveAction(Input.GetAxisRaw(HorizontalMoveInput),
                Input.GetAxisRaw(VerticalMoveInput));

            if (Input.GetButton(ShootInput))
            {
                ShootAction();
            }
        }
    }
}
