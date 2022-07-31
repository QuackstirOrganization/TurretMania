using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugs;

namespace TurretGame
{
    public class TerrainBase : MonoBehaviour
    {
        //Effects
        protected EffectBase[] _effects;

        [SerializeField] protected string terrainEffect;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _effects = this.GetComponents<EffectBase>();

            foreach (EffectBase effect in _effects)
            {
                effect.InitializeEffect();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag != null && collision.gameObject.CompareTag(terrainEffect))
            {
                foreach (EffectBase effect in _effects)
                {
                    effect.GetCharacterUnit(collision.GetComponent<CharacterUnitBase>());
                    effect.EffectActivation();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(terrainEffect))
            {
                foreach (EffectBase effect in _effects)
                {
                    effect.RemoveEffect();
                }
            }
            else
            {
                GlobalDebugs.DebugPM(this, " collided with this " + collision.gameObject.tag);
            }
        }
    }
}
