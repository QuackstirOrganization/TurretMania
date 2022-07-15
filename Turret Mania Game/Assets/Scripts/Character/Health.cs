using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    public float maxHealth;

    public event Action DeathAction;

    public event Action<float> DamageAction;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float dmg)
    {
        health -= dmg;

        if (health > 0)
        {
            DamageAction(health);
            return;
        }
        DeathAction();
    }


}
