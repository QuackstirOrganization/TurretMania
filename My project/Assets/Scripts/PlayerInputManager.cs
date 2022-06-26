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

        public float fireRate;
        private float lastTimeFired;

        public bool isShooting;

        // Update is called once per frame
        void Update()
        {
            HorizontalMoveAction(Input.GetAxisRaw(HorizontalMoveInput));
            VerticalMoveAction(Input.GetAxisRaw(VerticalMoveInput));

            if (Input.GetButtonDown(ShootInput))
            {
                isShooting = true;
            }

            if (Input.GetButtonUp(ShootInput))
            {
                isShooting = false;
            }
        }

        private void FixedUpdate()
        {
            if (!isShooting)
            {
                return;
            }

            if (Time.time - lastTimeFired > 1 / fireRate)
            {
                lastTimeFired = Time.time;
                ShootAction();
            }
        }
    }
}
