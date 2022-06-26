using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public enum GameFeelType
    {
        ScreenShake,
        ObjectFlash
    }



    public class Gamefeel : MonoBehaviour
    {
        public SpriteRenderer currentSprite;
        public Color globalInitialColor;

        public GameObject particleEffect;

        private void Start()
        {
            //currentSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void enableParticles()
        {
            particleEffect.SetActive(true);
        }

        public void ObjectFlash(float flashTime)
        {
            globalInitialColor = currentSprite.color;

            currentSprite.color = Color.white;

            Invoke("ReturnColor", flashTime);
        }

        void ReturnColor()
        {
            currentSprite.color = globalInitialColor;
        }
    }
}
