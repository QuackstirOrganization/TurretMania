using UnityEngine;
using UnityEngine.SceneManagement;
using Debugs;
using UnityEngine.UI;

namespace TurretGame
{
    public class PlayerManager : MonoBehaviour
    {
        private GameObject player;
        private Health PlayerHealth;
        private PlayerUnit playerUnit;

        public Text Text_playerAmmo;
        public Image Slider_playerAmmo;

        public Text Text_playerHealth;
        public Image Slider_playerHealth;

        public Text Text_playerLevel;
        public Image Slider_playerProgBar;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            //Find player components
            PlayerHealth = player.GetComponent<Health>();
            playerUnit = player.GetComponent<PlayerUnit>();

            //Subscribe to player health events
            #region Player Health Events
            PlayerHealth.DeathAction += OnPlayerDeath;
            #endregion

            //Subscribe to player shoot events
            //PlayerShooting.ShootAction += UpdateAmmoUI;

            playerUnit.AmmoUIAction += UpdateAmmoUI;
            playerUnit.HealthUIAction += UpdateHealthUI;
            playerUnit.ExpLevelUIAction += UpdateProgressBarUI;
            playerUnit.UpdateAmmoUI();
            playerUnit.UpdateHealthUI();
            playerUnit.UpdateProgressUI();
        }

        public void UpdateAmmoUI(float maxAmmo, float currAmmo)
        {
            Text_playerAmmo.text = currAmmo.ToString();
            Slider_playerAmmo.fillAmount = currAmmo / maxAmmo;
        }

        public void UpdateHealthUI(float maxHealth, float currHealth)
        {
            Text_playerHealth.text = currHealth.ToString();
            Slider_playerHealth.fillAmount = currHealth / maxHealth;
        }

        public void UpdateProgressBarUI(float expNextLevel, float currentExp, int currLevel)
        {
            Text_playerLevel.text = currLevel.ToString();
            Slider_playerProgBar.fillAmount = (currentExp / expNextLevel);
        }


        void OnPlayerDeath()
        {
            //Unsubscribe from all events
            PlayerHealth.DeathAction -= OnPlayerDeath;
            playerUnit.AmmoUIAction -= UpdateAmmoUI;
            playerUnit.HealthUIAction -= UpdateHealthUI;
            playerUnit.ExpLevelUIAction -= UpdateProgressBarUI;



            GlobalDebugs.DebugPM(this, "Player has died");

            //reload scene
            Invoke("DELETEFUNCTION", 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void DELETEFUNCTION()
        {
            SceneManager.LoadScene("Customization Screen");

        }
    }
}
