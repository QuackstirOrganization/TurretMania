using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
    public class ItemScriptableObject : ScriptableObject
    {
        public string Name;
        public GameObject ItemPrefab;

        public Rarity itemRarity;
        public SlopeType slopeType;
        public float initialVal;
        public float SlopeIncrease = 0.5f;
        public bool increasePercent = true;
    }
}
