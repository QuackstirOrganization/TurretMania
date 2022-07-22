using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currHealth;
    public float CurrHealth
    {
        get { return currHealth; } // Read only
        set
        {
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            currHealth = value;
        }
    }

    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; } // Read
        set { maxHealth = value; }
    }

    public event Action DeathAction;

    public event Action<float> DamageAction;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    public void Damage(float dmg)
    {
        currHealth -= dmg;

        if (currHealth > 0)
        {
            DamageAction(currHealth);
            return;
        }
        DeathAction();
    }


}
