using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptable weaponStatus;
    public float currentCooldown;
    public float currentSpecialCooldown;


    protected HeroMovement hm;

    // Input cooldown variables
    private float inputCooldown = 1f; // Minimum time between input presses
    private float currentInputCooldown = 0f; // Tracks the current time until the input can be registered again

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hm = FindObjectOfType<HeroMovement>();
        currentCooldown = weaponStatus.Cooldown;
        currentSpecialCooldown = weaponStatus.SpecialCooldown; // Initialize the special cooldown
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Regular cooldown update
        currentCooldown -= Time.deltaTime;

        // Check if the player presses space and the cooldown for regular shooting is done
        if (currentCooldown <= 0f)
        {
            Shoot(); // Regular shoot
        }

        // Special cooldown update
        currentSpecialCooldown -= Time.deltaTime;

        // Handle input cooldown
        currentInputCooldown -= Time.deltaTime;

        // Check for Space input and make sure the input cooldown is over and special cooldown is done
        if (Input.GetKeyDown(KeyCode.Space) && currentSpecialCooldown <= 0f && currentInputCooldown <= 0f)
        {
            StartCoroutine(ShootWithDelay()); // Start coroutine for special shoot with delay
            currentInputCooldown = inputCooldown; // Reset the input cooldown after the special shoot
        }
    }

    private IEnumerator ShootWithDelay()
    {
        // Handle the special shooting pattern with a delay
        Shoot(); // First shot
        yield return new WaitForSeconds(0.2f); // Wait for 0.3 seconds
        Shoot(); // Second shot after delay
        yield return new WaitForSeconds(0.2f); // Wait for 0.3 seconds
        Shoot(); // Third shot after delay
        yield return new WaitForSeconds(0.2f); // Wait for 0.3 seconds
        Shoot(); // Fourth shot after delay

        // Reset the special cooldown after using it
        currentSpecialCooldown = weaponStatus.SpecialCooldown; // Reset to the original special cooldown
    }

    protected virtual void Shoot()
    {
        // Handle the shooting logic here (e.g., spawn bullets, set direction, etc.)
        currentCooldown = weaponStatus.Cooldown; // Reset the cooldown after each shoot
    }
}
