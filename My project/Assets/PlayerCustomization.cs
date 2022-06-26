using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurretGame
{
    public class PlayerCustomization : MonoBehaviour
    {
        public TurretHead Head;
        public TurretBottom Bottom;

        // Start is called before the first frame update
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;


        }

        public void HeadSelect(TurretHead newHead)
        {
            Head = newHead;
        }

        public void BottomSelect(TurretBottom newBottom)
        {
            Bottom = newBottom;
        }

        void InitializeStats()
        {
            GameObject Player;
            Health PlayerHealth;
            Ammo PlayerAmmo;
            Shoot PlayerShooting;
            Movement PlayerMovement;

            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return;
            }

            Player = GameObject.FindGameObjectWithTag("Player");

            PlayerHealth = Player.GetComponent<Health>();
            PlayerAmmo = Player.GetComponent<Ammo>();
            PlayerMovement = Player.GetComponent<Movement>();
            PlayerShooting = Player.GetComponentInChildren<Shoot>();

            if (Head != null)
            {
                PlayerHealth.maxHealth = Head.Health;
                PlayerAmmo.MaxAmmo = Head.Ammo;
                PlayerShooting.ProjectileSpeed = Head.ProjectileSpeed;
            }

            if (Bottom != null)
            {
                PlayerMovement.Speed = Bottom.movementSpeed;
            }
        }

        #region Scene Functions
        public void SceneSelect(string SceneSelected)
        {
            SceneManager.LoadScene(SceneSelected);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);

            InitializeStats();
        }
        void OnDisable()
        {
            Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion
    }
}
