using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptable enemyStatus;
    public CharacterStatus player;
    //Current status
    float currentHealth;
    float currentDamage;
    float currentMoveSpeed;

    void Awake()
    {
        currentHealth = enemyStatus.HealthPoint;
        currentDamage = enemyStatus.Damage;
        currentMoveSpeed = enemyStatus.MoveSpeed;

        if (player == null)
        {
            player = FindObjectOfType<CharacterStatus>();  // Automatically find player if not assigned
        }

        ScaleStatsByLevel();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ScaleStatsByLevel()
    {
        if (player != null)
        {
            currentHealth = enemyStatus.HealthPoint * Mathf.Pow(1.1f, player.level);
            currentDamage = enemyStatus.Damage * Mathf.Pow(1.1f, player.level);
            currentMoveSpeed = enemyStatus.MoveSpeed;  // Keep move speed constant or adjust as needed
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterStatus player = other.gameObject.GetComponent<CharacterStatus>();
            player.TakeDamage(currentDamage);
        }
    }
}
