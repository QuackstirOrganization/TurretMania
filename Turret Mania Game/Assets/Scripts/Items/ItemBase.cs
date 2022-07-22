using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public enum Rarity
    {
        Common, Rare, Legendary
    }


    public class ItemBase : MonoBehaviour
    {
        [SerializeField] protected EffectBase[] _effects;
        [SerializeField] protected CharacterUnitBase _characterUnit;
        protected Rarity rarity;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            foreach (EffectBase effect in _effects)
            {
                effect.InitializeEffect();
            }
        }

        protected virtual void ProcEffects()
        {
            foreach (EffectBase effect in _effects)
            {
                effect.EffectActivation();
            }
        }
    }
}
