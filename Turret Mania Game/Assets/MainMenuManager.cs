using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurretGame
{
    public class MainMenuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GoToScene(string LoadThisScene)
        {
            SceneManager.LoadScene(LoadThisScene);
        }
    }
}
