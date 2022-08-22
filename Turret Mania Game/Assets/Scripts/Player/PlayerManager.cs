using UnityEngine;
using UnityEngine.SceneManagement;
using Debugs;
using UnityEngine.UI;
using TMPro;

namespace TurretGame
{
    public class PlayerManager : MonoBehaviour
    {
        private GameObject player;
        private Health PlayerHealth;
        private PlayerUnit playerUnit;

        [Header("Ammo")]
        public Text Text_playerAmmo;
        public Image Slider_playerAmmo;

        [Header("Health")]
        public TMP_Text Text_playerHealth;
        public Image Slider_playerHealth;

        [Header("Progression")]
        //public Text Text_playerLevel;
        public Image Slider_playerProgBar;

        public GameObject DeathScreen;

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

            DeathScreen.SetActive(false);

            //Subscribe to player shoot events
            //PlayerShooting.ShootAction += UpdateAmmoUI;

            playerUnit.AmmoUIAction += UpdateAmmoUI;
            playerUnit.HealthUIAction += UpdateHealthUI;
            playerUnit.ExpLevelUIAction += UpdateProgressBarUI;
            Invoke("UpdateAllUI", 0.1f);
        }

        void UpdateAllUI()
        {
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
            //Text_playerLevel.text = currLevel.ToString();
            Slider_playerProgBar.fillAmount = (currentExp / expNextLevel);
        }


        void OnPlayerDeath()
        {
            //Unsubscribe from all events
            PlayerHealth.DeathAction -= OnPlayerDeath;
            playerUnit.AmmoUIAction -= UpdateAmmoUI;
            playerUnit.HealthUIAction -= UpdateHealthUI;
            playerUnit.ExpLevelUIAction -= UpdateProgressBarUI;

            DeathScreen.SetActive(true);


            GlobalDebugs.DebugPM(this, "Player has died");

            //reload scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            FindObjectOfType<SavingLoading>().Save();
        }

        public void DELETEFUNCTION()
        {
            SceneManager.LoadScene("Customization Screen");

        }
    }
}
