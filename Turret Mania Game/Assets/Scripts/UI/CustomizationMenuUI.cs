using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurretGame
{
    public class CustomizationMenuUI : MonoBehaviour
    {
        public TMP_Text ScrapText;


        // Start is called before the first frame update
        void Start()
        {
            ScrapText.text = ScrapManager.I_ScrapManager.commonScrap.ToString();

            ScrapManager.I_ScrapManager.AddScrapAction += UpdateScrapAmountUI;
        }

        void UpdateScrapAmountUI(int Scrap)
        {
            ScrapText.text = Scrap.ToString();
        }

        private void OnDisable()
        {
            ScrapManager.I_ScrapManager.AddScrapAction -= UpdateScrapAmountUI;

        }
    }
}
