using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debugs;

public class DamageEnemy : MonoBehaviour
{
    public float Damage;
    public string Opponent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GlobalDebugs.DebugPM(this, "Triggered by " + collision.gameObject);

        if (collision.gameObject.CompareTag(Opponent))
        {
            collision.gameObject.GetComponent<Health>().Damage(Damage);
            Destroy(gameObject);
        }
    }
}
