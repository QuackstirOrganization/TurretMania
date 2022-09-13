using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurretGame
{
    public class PlayerCustomizationButton : MonoBehaviour
    {
        private TMP_Text nameText;
        public string Name;

        private bool isBought = false;

        [SerializeField] private int cost;
        [SerializeField] private Turret TurretType;

        private PlayerCustomization _PlayerCustomization;

        private void Start()
        {
            _PlayerCustomization = FindObjectOfType<PlayerCustomization>();

            nameText = GetComponentInChildren<TMP_Text>();

            nameText.text = Name + " Cost: " + cost;
        }

        public void buttonClick()
        {
            if (ScrapManager.I_ScrapManager.commonScrap < cost)
            {
                return;
            }

            if (isBought)
            {
                _PlayerCustomization.HeadSelect(TurretType);
            }
            else
            {
                ScrapManager.I_ScrapManager.removeScrap(cost);
                isBought = true;
                _PlayerCustomization.HeadSelect(TurretType);
                nameText.text = Name;
            }
        }
    }
}
