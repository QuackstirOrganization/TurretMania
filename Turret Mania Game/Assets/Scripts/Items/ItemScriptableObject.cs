using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
    public class ItemScriptableObject : ScriptableObject
    {
        public EffectBase[] effects;
    }
}
