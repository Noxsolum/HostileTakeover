using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 1f;

    public UnityAction<float, GameObject> onDamaged;
    public UnityAction onDeath;
    public UIScript uiScript;
    public VoiceLineScript vScript;

    public EnemyController enemy;
    public PlayerController player;
    public AudioSource tookDamage;
    public AudioClip hurt;

    public float currentHealth { get; set; }

    public bool invincible { get; set; }

    bool m_IsDead;

    private void Start()
    {
        vScript = GameObject.FindGameObjectWithTag("Voice").GetComponent<VoiceLineScript>();
        if (this.gameObject.CompareTag("Player"))
        {
            player = this.GetComponent<PlayerController>();
        }
        else { enemy = this.GetComponent<EnemyController>(); }
        currentHealth = maxHealth;
        invincible = false;
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        // Invinciblity Check
        if (invincible)
            return;
        // Apply Damage
        float healthBefore = currentHealth;
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);

        // Check if Health is greater than zero to play hit sound
        if (currentHealth > 0)
        {
            tookDamage.clip = hurt;
            tookDamage.Play();
        }

        // Random Chance for Quote
        int randi = Random.Range(1, 11);
        if(randi >= 8)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                // Player taken Damage
                enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
                vScript.PlayerHit();
            }
            else if (this.gameObject.CompareTag("Enemy"))
            {
                // Enemy taken Damage
                vScript.EnemyHit();
            }
        }

        // Check if dead
        HandleDeath();
    }

    private void HandleDeath()
    {
        if (m_IsDead)
            return;

        // call OnDie action
        if (currentHealth <= 0f)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                if (player != null)
                {
                    vScript.PlayerDeath();
                    player.onDeath();
                }
            }
            else
            { 
                if (enemy != null)
                {
                    m_IsDead = true;
                    enemy.onDeath();
                }
            }
        }
    }
}
