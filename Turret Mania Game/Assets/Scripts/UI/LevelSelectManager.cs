using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class LevelSelectManager : MonoBehaviour
    {
        private LevelSelectButton_ScriptableObject LevelWant;
        public LevelSelectButton_ScriptableObject levelWant
        {
            get { return LevelWant; }
            set
            {
                LevelWant = value;

                if (ActionLevelWant != null)
                    ActionLevelWant(value);
            }
        }

        public Action<LevelSelectButton_ScriptableObject> ActionLevelWant;

        private static LevelSelectManager i_LevelSelectManager;
        public static LevelSelectManager I_LevelSelectManager
        {
            // Get the functions
            get
            {
                // Check if Instance is not there
                if (i_LevelSelectManager == null)
                {
                    // If not then find a object with the component PauseManager
                    i_LevelSelectManager = FindObjectOfType<LevelSelectManager>();
                }
                // Return the Instance
                return i_LevelSelectManager;
            }

            // Private set so other components cant modify this script
            private set
            {
                i_LevelSelectManager = value;
            }
        }

        private void Awake()
        {
            if (i_LevelSelectManager == null)
            {
                i_LevelSelectManager = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
