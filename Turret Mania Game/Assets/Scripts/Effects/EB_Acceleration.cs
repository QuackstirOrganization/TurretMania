using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public class EB_Acceleration : EffectBase
    {
        [SerializeField] float newAcceleration;
        private float initialAcceleration;

        public override void GetCharacterUnit(CharacterUnitBase characterUnit)
        {
            base.GetCharacterUnit(characterUnit);
            initialAcceleration = characterUnit._movement.Acceleration;
        }

        public override void EffectActivation()
        {
            base.EffectActivation();

            _characterUnit._movement.Acceleration = newAcceleration;
        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            _characterUnit._movement.Acceleration = _characterUnit.initalAcceleration;
        }
    }
}
