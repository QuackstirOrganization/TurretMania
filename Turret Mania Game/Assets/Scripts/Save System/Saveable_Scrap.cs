using System;
using System.Collections.Generic;
using UnityEngine;
using Debugs;

namespace TurretGame
{
    public class Saveable_Scrap : MonoBehaviour, ISaveable
    {
        [SerializeField] private int commonScrap;

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
    }
}
