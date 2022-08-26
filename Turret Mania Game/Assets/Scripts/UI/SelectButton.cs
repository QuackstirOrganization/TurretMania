using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurretGame
{
    public class SelectButton : MonoBehaviour
    {
        public SelectOption_SO optionSelect;
        public Image SelectImage;

        public void InitiateButton()
        {
            SelectImage.sprite = optionSelect.Picture;

        }
    }
}
