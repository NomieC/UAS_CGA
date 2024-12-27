using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public CharacterScriptable characterStatus;
    //Current status
    public float currentHealth;
    public float currentEnergy;
    public float currentMoveSpeed;
    public float currentSprintSpeed;
    public float currentProjectileSpeed;
    public float currentMagnetRange;

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
        currentMagnetRange = characterStatus.MagnetRange;
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
        HealOverTime();
        EnergyOverTime();
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

    void HealOverTime()
    {
        if (currentHealth < characterStatus.HealthPoint)
        {
            currentHealth += 5 * Time.deltaTime;
            if (currentHealth > characterStatus.HealthPoint)
            {
                currentHealth = characterStatus.HealthPoint;
            }
        }
    }
    void EnergyOverTime()
    {
        if (currentEnergy < characterStatus.EnergyPoint)
        {
            currentEnergy += 5 * Time.deltaTime;
            if (currentEnergy > characterStatus.EnergyPoint)
            {
                currentEnergy = characterStatus.EnergyPoint;
            }
        }
    }
}