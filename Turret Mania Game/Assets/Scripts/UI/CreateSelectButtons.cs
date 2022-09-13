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

        public CreatedSelectButtons(SelectOption_SO buttonSO, GameObject buttonObject, string name, bool isEnabled)
        {
            this.buttonSO = buttonSO;
            this.buttonObject = buttonObject;
            this.name = name;
            this.isEnabled = isEnabled;
        }
    }


    public class CreateSelectButtons : MonoBehaviour
    {
        public GameObject selectButton;
        public SelectOption_SO[] SelectOptions;
        //public List<CreatedSelectButtons> selectedButtons = new List<CreatedSelectButtons>();
        public Dictionary<string, CreatedSelectButtons> D_selectedButtons = new Dictionary<string, CreatedSelectButtons>();
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

                //selectedButtons.Add(newButton);
                D_selectedButtons.Add(option.name, newButton);
            }
        }

        public CreatedSelectButtons findOptions(SelectOption_SO buttonWant)
        {
            return D_selectedButtons[buttonWant.name];
        }

        public void modifyOptionsEnabled(CreatedSelectButtons modThis, bool isActive)
        {
            CreatedSelectButtons buttonGot =
                new CreatedSelectButtons
                (modThis.buttonSO, modThis.buttonObject, modThis.name, isActive);

            D_selectedButtons[buttonGot.name] = buttonGot;
        }

        [ContextMenu("Update Buttons")]
        public void updateButtonList()
        {
            foreach (var button in D_selectedButtons)
            {
                button.Value.buttonObject.SetActive(button.Value.isEnabled);
            }
        }
    }
}
