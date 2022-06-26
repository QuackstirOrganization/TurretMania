using UnityEngine;
using UnityEngine.SceneManagement;
using Debugs;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    private Health PlayerHealth;
    private Ammo PlayerAmmo;
    private Shoot PlayerShooting;

    public Text UI_playerAmmo;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Find player components
        PlayerHealth = player.GetComponent<Health>();
        PlayerAmmo = player.GetComponent<Ammo>();
        PlayerShooting = player.GetComponentInChildren<Shoot>();

        //Subscribe to player health events
        #region Player Health Events
        PlayerHealth.DeathAction += OnPlayerDeath;
        #endregion

        //Subscribe to player shoot events
        PlayerShooting.ShootAction += UpdateAmmoUI;

        //Update AmmoUI
        UpdateAmmoUI(PlayerAmmo.CurrAmmo);
    }

    public void UpdateAmmoUI(int Ammo)
    {
        UI_playerAmmo.text = "" + Ammo;
    }

    void OnPlayerDeath()
    {
        GlobalDebugs.DebugPM(this, "Player has died");

        //Unsubscribe from all events
        PlayerHealth.DeathAction -= OnPlayerDeath;
        PlayerShooting.ShootAction -= UpdateAmmoUI;


        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
