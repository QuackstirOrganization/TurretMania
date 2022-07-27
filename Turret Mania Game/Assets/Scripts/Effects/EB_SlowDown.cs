using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class EB_SlowDown : EffectBase
    {
        [SerializeField] float newSpeed;
        private float initialSpeed;

        public override void GetCharacterUnit(CharacterUnitBase characterUnit)
        {
            base.GetCharacterUnit(characterUnit);
            initialSpeed = _characterUnit._movement.Speed;
        }

        public override void EffectActivation()
        {
            base.EffectActivation();

            _characterUnit._movement.Speed = newSpeed;
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            _characterUnit._movement.Speed = initialSpeed;
        }
    }
}
