using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class TerrainBase : MonoBehaviour
    {
        [SerializeField] protected EffectBase[] _effects;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            foreach (EffectBase effect in _effects)
            {
                effect.InitializeEffect();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }
    }
}
