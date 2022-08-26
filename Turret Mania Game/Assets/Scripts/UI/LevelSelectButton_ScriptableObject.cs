using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "LevelButton", menuName = "Level Button", order = 1)]
    public class LevelSelectButton_ScriptableObject : SelectOption_SO
    {
        public string SceneName;
    }
}
