using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class EffectBase : MonoBehaviour
    {
        protected CharacterUnitBase _characterUnit;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitializeEffect();
        }

        public virtual void InitializeEffect()
        {
            _characterUnit = this.GetComponent<CharacterUnitBase>();
        }

        //Increase movement speed
        public virtual void EffectActivation()
        {

        }

        //Activate this for more movement speed
        protected virtual void UpdateEffectStats()
        {

        }
    }
}
