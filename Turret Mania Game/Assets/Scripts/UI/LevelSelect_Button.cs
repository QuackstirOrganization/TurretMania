using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class LevelSelect_Button : MonoBehaviour
    {
        public LevelSelectButton_ScriptableObject sceneWant;

        private void Start()
        {
            sceneWant = this.GetComponent<SelectButton>().optionSelect as LevelSelectButton_ScriptableObject;
        }

        public void SelectLevel()
        {
            LevelSelectManager.I_LevelSelectManager.levelWant = sceneWant;
        }
    }
}
