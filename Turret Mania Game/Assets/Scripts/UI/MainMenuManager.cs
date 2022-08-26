using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurretGame
{
    public class MainMenuManager : MonoBehaviour
    {
        public void GoToScene(string LoadThisScene)
        {
            SceneManager.LoadScene(LoadThisScene);
            FindObjectOfType<SavingLoading>().Save();
        }
    }
}
