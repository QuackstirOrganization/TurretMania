using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class EquipWeaponButton : EquippedButton
    {
        public CreateSelectButtons WeaponsMenu;
        public List<CreatedSelectButtons> buttonstuffs = new List<CreatedSelectButtons>();

        protected override void Start()
        {
            //Menu = GameObject.FindGameObjectWithTag("UI_WeaponMenu");
            base.Start();
        }
        public override void OpenMenu()
        {
            CreatedSelectButtons buttonthing;

            base.OpenMenu();
            //WeaponsMenu.selectedButtons.find
            buttonthing = WeaponsMenu.findOptions(OptionSO);


            //foreach (var button in WeaponsMenu.D_selectedButtons)
            //{
            //    for (int i = 0; i < PlayerCustomization.i_PlayerCustomize.TurretWeapons.Length; i++)
            //    {
            //        if (button.Key == PlayerCustomization.i_PlayerCustomize.TurretWeapons[i].Name)
            //        {
            //            WeaponsMenu.modifyOptionsEnabled(button.Value, false);
            //        }
            //    }
            //}

            WeaponsMenu.updateButtonList();
        }
    }
}
