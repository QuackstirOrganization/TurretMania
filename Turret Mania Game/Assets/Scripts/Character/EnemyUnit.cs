using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TurretGame
{
    public class EnemyUnit : CharacterUnitBase
    {
        private Health EnemyHealth;

        private SpriteRenderer EnemyVisuals;

        public GameObject ExpOrb;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            EnemyHealth = this.GetComponent<Health>();

            EnemyVisuals = GetComponentInChildren<SpriteRenderer>();

        }
        protected override void V_Death()
        {
            base.V_Death();
            Destroy(gameObject);
        }

        public void DropExp()
        {
            Instantiate(ExpOrb, transform.position, Quaternion.identity);
        }
    }
}