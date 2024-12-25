using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptable enemyStatus;
    //Current status
    float currentHealth;
    float currentDamage;
    float currentMoveSpeed;

    void Awake(){
        currentHealth = enemyStatus.HealthPoint;
        currentDamage = enemyStatus.Damage;
        currentMoveSpeed = enemyStatus.MoveSpeed;
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){
        Destroy(gameObject);
    }
}
