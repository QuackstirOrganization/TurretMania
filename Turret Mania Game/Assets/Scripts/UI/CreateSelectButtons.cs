using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [System.Serializable]
    public struct CreatedSelectButtons
    {
        public SelectOption_SO buttonSO;
        public GameObject buttonObject;
        public string name;
        public bool isEnabled;
    }


    public class CreateSelectButtons : MonoBehaviour
    {
        public GameObject selectButton;
        public SelectOption_SO[] SelectOptions;
        public List<CreatedSelectButtons> selectedButtons = new List<CreatedSelectButtons>();
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

                CreatedSelectButtons newButton;
                newButton.buttonSO = option;
                newButton.buttonObject = newOptionButton;
                newButton.name = option.name;
                newButton.isEnabled = true;

                selectedButtons.Add(newButton);
            }
        }

        [ContextMenu("Update Buttons")]
        public void updateButtonList()
        {
            foreach (CreatedSelectButtons button in selectedButtons)
            {
                button.buttonObject.SetActive(button.isEnabled);
            }
        }
    }
}
