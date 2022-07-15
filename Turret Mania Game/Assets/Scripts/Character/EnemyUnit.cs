using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnit : MonoBehaviour
{
    private Health EnemyHealth;
    public UnityEvent OnEnemyHurtEffects;

    private SpriteRenderer EnemyVisuals;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth = this.GetComponent<Health>();

        EnemyVisuals = GetComponentInChildren<SpriteRenderer>();

        //Subscribe to enemy health events
        #region Player Health Events
        EnemyHealth.DeathAction += OnEnemyDeath;
        EnemyHealth.DamageAction += OnEnemyDamage;
        #endregion
    }

    void OnEnemyDamage(float damage)
    {
        Debug.Log(damage);
        OnEnemyHurtEffects.Invoke();
    }

    void OnEnemyDeath()
    {
        //Unsubscribe all events
        EnemyHealth.DeathAction -= OnEnemyDeath;
        EnemyHealth.DamageAction -= OnEnemyDamage;

        Destroy(gameObject);
    }
}
