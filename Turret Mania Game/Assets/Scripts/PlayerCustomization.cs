using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurretGame
{
    public class PlayerCustomization : MonoBehaviour
    {
        public Turret TurretType;
        public TurretWeapon[] TurretWeapons;

        // Start is called before the first frame update
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;


        }

        public void HeadSelect(Turret newHead)
        {
            TurretType = newHead;
        }

        public void BottomSelect(TurretWeapon newBottom)
        {
            //TurretWeapons = newBottom;
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

            if (TurretType != null)
            {
                PlayerHealth.MaxHealth = TurretType.Health;
                PlayerMovement.Speed = TurretType.movementSpeed;
            }

            if (TurretWeapons != null)
            {
                //PlayerShooting.ProjectileSpeed = Head.ProjectileSpeed;
                PlayerShooting.ProjectileSpeed = TurretWeapons[0].projectileSpeed;

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
