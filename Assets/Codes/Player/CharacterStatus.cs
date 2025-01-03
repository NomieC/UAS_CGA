using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public CharacterScriptable characterStatus;

    // Current status
    public float currentHealth;
    public float maxHealth;
    public float currentEnergy;
    public float currentMoveSpeed;
    public float currentSprintSpeed;
    public float currentProjectileSpeed;
    public float currentMagnetRange;
    public bool isDamageBuffed = false;
    private Coroutine buffCoroutine;
    public float damageMultiplier = 1f;

    [Header("Experience System")]
    public float experience = 0;
    public float level = 1;
    public float maxExperience;

    // [SerializeField] HealthBarPlayer healthBar;
    [SerializeField] HealthBarPlayerUI healthBarUI;

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
    

    // Initialization
    void Awake()
    {
        // Initialize stats based on CharacterScriptable values
        maxHealth = characterStatus.HealthPoint;  // Initialize max health with scaling
        currentHealth = maxHealth;  // Set current health to max at start
        currentEnergy = characterStatus.EnergyPoint;
        currentMoveSpeed = characterStatus.MoveSpeed;
        currentSprintSpeed = characterStatus.SprintSpeed;
        currentProjectileSpeed = characterStatus.ProjectileSpeed;
        currentMagnetRange = characterStatus.MagnetRange;
        // healthBar = GetComponentInChildren<HealthBarPlayer>();
        healthBarUI = GetComponentInChildren<HealthBarPlayerUI>();
    }

    void Start()
    {
        // Set initial experience required for level up
        maxExperience = levelRanges[0].maxExperienceIncrease;
        // healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    void Update()
    {
        // Handle invincibility frames countdown
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

    // Add experience and handle level up
    public void AddExperience(int exp)
    {
        experience += exp;

        while (experience >= maxExperience)
        {
            level++;
            experience -= maxExperience;

            // Notify all enemies to scale up based on new level
            EnemyStats[] enemies = FindObjectsOfType<EnemyStats>();
            foreach (EnemyStats enemy in enemies)
            {
                enemy.ScaleStatsByLevel();
            }

            // Increase max experience for next level
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    maxExperience += range.maxExperienceIncrease;
                    break;
                }
            }

            // Scale player stats on level up
            ScaleStatsByLevel();
        }
    }

    // Take damage with invincibility and death checks
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            // healthBar.UpdateHealthBar(currentHealth, maxHealth);
            healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
            invincibilityTimer = invincibilityTime;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // Player death logic
    public void Die()
    {
        Debug.Log("You died");
        // Implement player respawn or restart logic here
    }

    // Heal health over time
    void HealOverTime()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 5 * Time.deltaTime;  // Heal 5 HP per second
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    // Regenerate energy over time with scaling by level
    void EnergyOverTime()
    {
        if (currentEnergy < characterStatus.EnergyPoint)
        {
            currentEnergy += 5 * Mathf.Pow(1.1f, level) * Time.deltaTime;
            if (currentEnergy > characterStatus.EnergyPoint)
            {
                currentEnergy = characterStatus.EnergyPoint;
            }
        }
    }

    // Scale player stats when leveling up
    public void ScaleStatsByLevel()
    {
        // Preserve the current health percentage during scaling
        float healthPercentage = currentHealth / maxHealth;

        // Scale max health, energy, and other stats by level
        maxHealth = characterStatus.HealthPoint * Mathf.Pow(1.2f, level);
        currentHealth = maxHealth * healthPercentage;  // Keep current health proportional to the new max
        currentEnergy = characterStatus.EnergyPoint * Mathf.Pow(1.1f, level);
    }

    // Heal instantly with health pickup
    public void HP_Regen()
    {
        currentHealth += maxHealth * 1f;  // Restore 25% of base health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void ApplyDamageBuff(float duration)
{
    if (buffCoroutine != null)
    {
        StopCoroutine(buffCoroutine);  // Stop any active buff coroutine
    }
    buffCoroutine = StartCoroutine(DamageBuffTimer(duration));
}

IEnumerator DamageBuffTimer(float duration)
{
    isDamageBuffed = true;
    damageMultiplier = 3f;  // Apply double damage
    yield return new WaitForSeconds(duration);
    isDamageBuffed = false;
    damageMultiplier = 1f;  // Reset to normal damage
}
}