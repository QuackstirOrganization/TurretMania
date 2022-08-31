using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 1)]
    public class TurretWeapon : PlayerSelectButton
    {
        public float damage = 5;
        public float fireRate = 1;
        public float projectileSpeed = 5;
        public int ammo = 5;
    }
}
