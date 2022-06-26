using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "Turret Head", menuName = "Turret Head", order = 0)]
    public class TurretHead : ScriptableObject
    {
        public float Health = 10;
        public int Ammo = 5;
        public float ProjectileSpeed = 20;
    }
}
