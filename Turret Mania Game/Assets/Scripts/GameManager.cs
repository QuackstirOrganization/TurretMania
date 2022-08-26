using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager i_GameManager;
        public static GameManager I_GameManager
        {
            // Get the functions
            get
            {
                // Check if Instance is not there
                if (i_GameManager == null)
                {
                    // If not then find a object with the component PauseManager
                    i_GameManager = FindObjectOfType<GameManager>();
                }
                // Return the Instance
                return i_GameManager;
            }

            // Private set so other components cant modify this script
            private set
            {
                i_GameManager = value;
            }
        }

        private void Awake()
        {
            if (i_GameManager == null)
            {
                i_GameManager = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
