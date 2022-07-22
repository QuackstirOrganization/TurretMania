using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    [CreateAssetMenu(fileName = "Turret Head", menuName = "Turret Head", order = 0)]
    public class Turret : ScriptableObject
    {
        public float Health = 10;
        public float movementSpeed = 5;
    }
}
