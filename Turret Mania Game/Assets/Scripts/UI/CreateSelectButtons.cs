using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class CreateSelectButtons : MonoBehaviour
    {
        public GameObject selectButton;
        public SelectOption_SO[] SelectOptions;
        // Start is called before the first frame update
        void Awake()
        {
            foreach (SelectOption_SO option in SelectOptions)
            {
                GameObject newOptionButton = Instantiate(selectButton);
                newOptionButton.GetComponent<RectTransform>().SetParent(this.GetComponent<RectTransform>());
                SelectButton newSelectButton = newOptionButton.GetComponent<SelectButton>();
                newSelectButton.optionSelect = option;
                newSelectButton.InitiateButton();
            }
        }
    }
}
