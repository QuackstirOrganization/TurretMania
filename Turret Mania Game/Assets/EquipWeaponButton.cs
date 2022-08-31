using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class EquipWeaponButton : EquippedButton
    {
        protected override void Start()
        {
            Menu = GameObject.FindGameObjectWithTag("UI_WeaponMenu");
            base.Start();
        }
        public override void OpenMenu()
        {
            base.OpenMenu();
        }
    }
}
