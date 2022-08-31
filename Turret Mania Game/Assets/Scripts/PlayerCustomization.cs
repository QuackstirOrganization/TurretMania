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

        private static PlayerCustomization I_PlayerCustomize;
        public static PlayerCustomization i_PlayerCustomize
        {
            get { return I_PlayerCustomize; }
            set
            {
                I_PlayerCustomize = value;
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (I_PlayerCustomize == null)
            {
                I_PlayerCustomize = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

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

            CharacterUnitBase PlayerCharacterUnit;
            Shoot PlayerShooting;

            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return;
            }

            Player = GameObject.FindGameObjectWithTag("Player");

            PlayerCharacterUnit = Player.GetComponent<CharacterUnitBase>();


            PlayerShooting = Player.GetComponentInChildren<Shoot>();

            //if (TurretType != null)
            //{
            //    PlayerCharacterUnit.initialHealth = TurretType.Health;
            //    PlayerCharacterUnit.initalSpeed = TurretType.movementSpeed;
            //}

            if (TurretWeapons != null)
            {
                //PlayerShooting.ProjectileSpeed = Head.ProjectileSpeed;
                //PlayerShooting.ProjectileSpeed = TurretWeapons[0].projectileSpeed;

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
