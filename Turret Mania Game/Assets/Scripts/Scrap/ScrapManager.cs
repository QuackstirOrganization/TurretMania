using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debugs;

namespace TurretGame
{

    public class ScrapManager : MonoBehaviour, ISaveable
    {
        public int commonScrap;

        private PlayerUnit player;

        private static ScrapManager _scrapManager;
        public static ScrapManager I_ScrapManager { get { return _scrapManager; } }

        public Action<int> AddScrapAction;

        void Awake()
        {
            if (_scrapManager == null)
            {
                _scrapManager = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            FindObjectOfType<SavingLoading>().Load();


            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void game()
        {
            //player._health.DeathAction += addScrap;
            //player._health.DeathAction += death;
            player.AddItemAction += addScrap;

        }

        void death()
        {
            //player._health.DeathAction -= death;
            player.AddItemAction -= addScrap;

        }

        private void addScrap(int amount)
        {
            //commonScrap += player.getItemRarityAMT(Rarity.Common) * 100;
            commonScrap += amount * 100;
            //player._health.DeathAction -= addScrap;

            if (AddScrapAction != null)
                AddScrapAction(commonScrap);
        }

        public void removeScrap(int removeAmount)
        {
            commonScrap -= removeAmount;

            if (AddScrapAction != null)
                AddScrapAction(commonScrap);
        }

        #region Saving
        public object CaptureState()
        {
            GlobalDebugs.DebugPM(this, "Scrap has been saved!");
            return new CurrentScrap
            {
                i_commonScrap = commonScrap
            };
        }

        public void RestoreState(object state)
        {
            var CurrentScrapData = (CurrentScrap)state;

            commonScrap = CurrentScrapData.i_commonScrap;
        }

        [Serializable]
        private struct CurrentScrap
        {
            //public Dictionary<Rarity, int> i_D_ScrapRarityAmount;
            public int i_commonScrap;
        }
        #endregion

        #region Scene Functions
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);

            if (FindObjectOfType<PlayerUnit>() != null)
            {
                player = FindObjectOfType<PlayerUnit>();
                //game();
                Invoke("game", 0.1f);
            }
        }
        void OnDisable()
        {
            Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion
    }
}
