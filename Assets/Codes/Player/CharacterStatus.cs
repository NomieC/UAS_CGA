using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public CharacterScriptable characterStatus;
    //Current status
    float currentHealth;
    float currentEnergy;
    float currentMoveSpeed;
    float currentSprintSpeed;
    float currentProjectileSpeed;

    [Header("Experience System")]
    public int experience = 0;
    public int level = 1;
    public int maxExperience;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int maxExperienceIncrease;
    }

    public List<LevelRange> levelRanges;

    // I-Frames
    [Header("I-Frames")]
    public float invincibilityTime;
    public bool isInvincible;
    public float invincibilityTimer;

    void Awake()
    {
        currentHealth = characterStatus.HealthPoint;
        currentEnergy = characterStatus.EnergyPoint;
        currentMoveSpeed = characterStatus.MoveSpeed;
        currentSprintSpeed = characterStatus.SprintSpeed;
        currentProjectileSpeed = characterStatus.ProjectileSpeed;
    }

    void start()
    {
        maxExperience = levelRanges[0].maxExperienceIncrease;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void AddExperience(int exp)
    {
        experience += exp;
        if (experience >= maxExperience)
        {
            level++;
            experience -= maxExperience;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    maxExperience += range.maxExperienceIncrease;
                    break;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;

            invincibilityTimer = invincibilityTime;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        // Destroy(gameObject);
        Debug.Log("You died");
    }
}