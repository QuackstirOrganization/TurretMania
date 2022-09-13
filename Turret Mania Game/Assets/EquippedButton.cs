using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public abstract class EquippedButton : MonoBehaviour
    {
        public SelectOption_SO OptionSO;
        public GameObject Menu;

        protected virtual void Start()
        {
            //OptionSO = this.GetComponent<SelectOption_SO>();
            Menu.SetActive(false);
        }
        public virtual void OpenMenu()
        {
            Menu.SetActive(true);
        }
    }
}
