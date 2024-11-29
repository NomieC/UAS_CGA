using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxMana = 100f;
    public float currentMana;

    void Start()
    {
        // Initialize health and mana
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
        // // For demonstration, you can add input to test health and mana changes
        // if (Input.GetKeyDown(KeyCode.H)) // Press 'H' to take damage
        // {
        //     TakeDamage(10f);
        // }

        // if (Input.GetKeyDown(KeyCode.M)) // Press 'M' to use mana
        // {
        //     UseMana(10f);
        // }

        // if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to regenerate health
        // {
        //     RegenerateHealth(5f);
        // }

        // if (Input.GetKeyDown(KeyCode.T)) // Press 'T' to regenerate mana
        // {
        //     RegenerateMana(5f);
        // }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("Current Health: " + currentHealth);
    }

    public void UseMana(float amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Ensure mana doesn't go below 0

        Debug.Log("Current Mana: " + currentMana);
    }

    public void RegenerateHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed max

        Debug.Log("Current Health: " + currentHealth);
    }

    public void RegenerateMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Ensure mana doesn't exceed max

        Debug.Log("Current Mana: " + currentMana);
    }

    private void Die()
    {
        Debug.Log("Character has died.");
        // Add death logic here (e.g., disable character, play animation, etc.)
    }
}