using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [System.Serializable]
    public class ScrapData
    {
        protected Dictionary<Rarity, int> D_ScrapRarityAmount = new Dictionary<Rarity, int>();


        public ScrapData(ScrapManager scrapManager)
        {
            if (D_ScrapRarityAmount.ContainsKey(Rarity.Common))
            {
                D_ScrapRarityAmount[Rarity.Common] += scrapManager.commonScrap;
            }
            else
            {
                D_ScrapRarityAmount.Add(Rarity.Common, scrapManager.commonScrap);
            }
        }
    }
}
