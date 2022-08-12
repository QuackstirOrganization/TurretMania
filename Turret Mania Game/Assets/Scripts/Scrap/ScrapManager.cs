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



            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void game()
        {
            player._health.DeathAction += addScrap;

        }

        private void addScrap()
        {
            commonScrap += player.getItemRarityAMT(Rarity.Common) * 100;
            player._health.DeathAction -= addScrap;

            if (AddScrapAction != null)
                AddScrapAction(commonScrap);

            CaptureState();
        }

        public void removeScrap(int removeAmount)
        {
            commonScrap -= removeAmount;

            if (AddScrapAction != null)
                AddScrapAction(commonScrap);
        }


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


        private struct CurrentScrap
        {
            //public Dictionary<Rarity, int> i_D_ScrapRarityAmount;
            public int i_commonScrap;
        }


        #region Scene Functions
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);

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
