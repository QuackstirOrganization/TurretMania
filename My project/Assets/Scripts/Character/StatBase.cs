using System;
using UnityEngine;

namespace TurretGame
{
    public class StatBase : MonoBehaviour
    {
        public PropertyName StatName;

        [SerializeField] private float currStat;
        public float maxStat;

        // Start is called before the first frame update
        void Start()
        {
            currStat = maxStat;
        }

        void GetStat()
        {

        }

        void SetStat()
        {

        }
    }
}
