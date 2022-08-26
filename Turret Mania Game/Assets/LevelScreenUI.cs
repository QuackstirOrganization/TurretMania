using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TurretGame
{
    public class LevelScreenUI : MonoBehaviour
    {
        public Image Background;
        public TMP_Text FlavorText;

        private void Start()
        {
            LevelSelectManager.I_LevelSelectManager.ActionLevelWant += ChangeLevelVisuals;
        }

        private void OnDestroy()
        {
            LevelSelectManager.I_LevelSelectManager.ActionLevelWant -= ChangeLevelVisuals;
        }


        void ChangeLevelVisuals(LevelSelectButton_ScriptableObject SelectedLevel)
        {
            Background.sprite = SelectedLevel.Picture;
            FlavorText.text = SelectedLevel.Description;
        }
    }
}
