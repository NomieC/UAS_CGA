using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoBehaviour
{
    public CharacterScriptable characterStatus;
    public Image healthFill;
    public Image energyFill;

    // Current status
    public float currentHealth;
    public float maxHealth;
    private Coroutine currentHealthBarCoroutine;
    public float currentEnergy;
    public float maxEnergy;
    private Coroutine currentEnergyBarCoroutine;
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
    public float totalExperience = 0;  // Track total XP as score

    [Header("player UI")]
    [SerializeField] GameObject YouDied;

    AudioManager audioManager;

    [SerializeField] HealthBarPlayerUI healthBarUI;

    [SerializeField] EnergyBarPlayerUI energyBarUI;

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

    [Header("Energy Regeneration")]
    public float energyRegenCooldown = 2f;  // Cooldown after sprint stops
    public float energyRegenTimer = 0f;
    public bool isSprinting = false;
    

    // Initialization
    void Awake()
    {
        // Initialize stats based on CharacterScriptable values
        maxHealth = characterStatus.HealthPoint;
        currentHealth = maxHealth;

        // Initialize energy
        maxEnergy = characterStatus.EnergyPoint;
        currentEnergy = maxEnergy;

        currentMoveSpeed = characterStatus.MoveSpeed;
        currentSprintSpeed = characterStatus.SprintSpeed;
        currentProjectileSpeed = characterStatus.ProjectileSpeed;
        currentMagnetRange = characterStatus.MagnetRange;

        healthBarUI = GetComponentInChildren<HealthBarPlayerUI>();
        energyBarUI = GetComponentInChildren<EnergyBarPlayerUI>();

        UpdateHealthBar();
        UpdateEnergyBar();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    void Start()
    {
        // Set initial experience required for level up
        maxExperience = levelRanges[0].maxExperienceIncrease;
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
        energyBarUI.UpdateEnergyBar(currentEnergy, maxEnergy);
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

        if (!isSprinting)
        {
            if (energyRegenTimer > 0)
            {
                energyRegenTimer -= Time.deltaTime;  // Countdown cooldown
            }
            else
            {
                EnergyOverTime();  // Regen only after cooldown
            }
        }

        HealOverTime();

        UpdateHealthBar();
        UpdateEnergyBar();
        energyBarUI.UpdateEnergyBar(currentEnergy, maxEnergy);
    }

    // Add experience and handle level up
    public void AddExperience(int exp)
    {
        Debug.Log("XP Received: " + exp);
        experience += exp;
        totalExperience += exp;

        Debug.Log("Current XP: " + experience + "/" + maxExperience);

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

            healthBarUI.UpdateHealthBar(currentHealth, maxHealth);

            UpdateHealthBar();

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
        // Hentikan BGM
        if (audioManager != null)
        {
            audioManager.StopMusic();
            audioManager.PlaySFX(audioManager.PlayerDieSound);
        }

        Debug.Log("You died");

        // Tampilkan UI 'You Died'
        if (YouDied != null)
        {
            YouDied.SetActive(true);
        }

        // Pause game (Freeze Time)
        Time.timeScale = 0f;

        // Mulai Coroutine untuk kembali ke Main Menu setelah 6 detik
        StartCoroutine(LoadMainSceneWithDelay(6f));
    }

    IEnumerator LoadMainSceneWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
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
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
        UpdateHealthBar();
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
        // Preserve the current health and energy percentage during scaling
        float healthPercentage = currentHealth / maxHealth;
        float energyPercentage = currentEnergy / maxEnergy;

        // Scale max health and energy by level
        maxHealth = characterStatus.HealthPoint * Mathf.Pow(1.2f, level);
        maxEnergy = characterStatus.EnergyPoint * Mathf.Pow(1.1f, level);

        currentHealth = maxHealth * healthPercentage;  // Maintain proportional health
        currentEnergy = maxEnergy * energyPercentage;  // Maintain proportional energy

        UpdateHealthBar();
        UpdateEnergyBar();
    }

    // Heal instantly with health pickup
    public void HP_Regen()
    {
        currentHealth += maxHealth * 1f;  // Restore 25% of base health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
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

    private void UpdateHealthBar()
    {
        UpdateBar(healthFill, currentHealth, maxHealth, ref currentHealthBarCoroutine);
    }

    private void UpdateEnergyBar()
    {
        UpdateBar(energyFill, currentEnergy, maxEnergy, ref currentEnergyBarCoroutine);
    }

    // Generic method to update any bar (health, energy)
   private void UpdateBar(Image barFill, float currentValue, float maxValue, ref Coroutine currentCoroutine)
    {
        if (barFill != null)
        {
            float percentage = currentValue / maxValue;
            float fullWidth = barFill.transform.parent.GetComponent<RectTransform>().rect.width;
            float targetWidth = fullWidth * percentage;

            // Stop the previous coroutine for this bar, if any
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            // Start a new smooth transition coroutine
            currentCoroutine = StartCoroutine(SmoothBarTransition(barFill, targetWidth));
        }
    }

    // Coroutine to smoothly transition the bar
    IEnumerator SmoothBarTransition(Image barFill, float targetWidth)
    {
        float currentWidth = barFill.rectTransform.rect.width;
        float elapsedTime = 0f;
        float duration = 0.3f; // Adjust this for faster/slower transitions

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newWidth = Mathf.Lerp(currentWidth, targetWidth, elapsedTime / duration);
            barFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            yield return null;
        }

        // Ensure it lands exactly at the target width
        barFill.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
    }


}