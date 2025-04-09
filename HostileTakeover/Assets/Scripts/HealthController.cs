using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float damageMultiplier = 0f;

    public Health health { get; private set; }
    public GameObject[] enemyObjects = new GameObject[5];

    public void Awake()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        this.health = GetComponent<Health>();
        if (!health)
        {
            health = GetComponent<Health>();
        }
    }

    public void InflictDamage(float damage, GameObject damageSource)
    {
        this.health = this.GetComponent<Health>();
        if (health && damageSource != this.gameObject)
        {
            float totalDamage = damage + damageMultiplier;
            Debug.Log("Total Damage: " + totalDamage);
            health.TakeDamage(totalDamage, damageSource);
        }
    }
}
