using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public abstract class SelectOption_SO : ScriptableObject
    {
        [Header("Descriptors")]
        public string Name;
        public string Description;
        public Sprite Picture;
    }
}
