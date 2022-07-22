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
            playerUnit.UpdateAmmoUI();
            playerUnit.UpdateHealthUI();
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


        void OnPlayerDeath()
        {
            GlobalDebugs.DebugPM(this, "Player has died");

            //Unsubscribe from all events
            PlayerHealth.DeathAction -= OnPlayerDeath;
            playerUnit.AmmoUIAction -= UpdateAmmoUI;
            playerUnit.HealthUIAction -= UpdateHealthUI;


            //reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
