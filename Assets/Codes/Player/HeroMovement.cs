using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroMovement : MonoBehaviour
{
    public float groundCheckDistance = 0.2f;
    private Rigidbody rb;
    private bool isGrounded;
    public Camera mainCamera;
    CharacterStatus player;

    private bool isSprinting;
    private Coroutine energyDrainCoroutine;

    void Start()
    {
        player = GetComponent<CharacterStatus>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating

        Light light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.renderMode = LightRenderMode.ForcePixel;
        }
    }

    void Update()
    {
        // Check if the player is grounded (if applicable)
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, LayerMask.GetMask("Ground"));

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Sprint logic
        if (Input.GetKey(KeyCode.LeftShift) && player.currentEnergy >= 10)
        {
            StartSprinting();
        }
        else
        {
            StopSprinting();
        }

        // Adjust movement speed based on sprint
        float currentSpeed = isSprinting ? player.currentSprintSpeed : player.currentMoveSpeed;

        // Move the character
        MoveCharacter(move, currentSpeed);
    }

    void MoveCharacter(Vector3 move, float speed)
    {
        // Apply movement to the Rigidbody
        Vector3 movement = move.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void StartSprinting()
    {
        if (!isSprinting)
        {
            isSprinting = true;
            
            if (energyDrainCoroutine == null)
            {
                energyDrainCoroutine = StartCoroutine(DrainEnergy());
            }
        }
    }

    void StopSprinting()
    {
        if (isSprinting)
        {
            isSprinting = false;
            player.energyRegenTimer = player.energyRegenCooldown;  // Start regen cooldown
        }

        if (energyDrainCoroutine != null)
        {
            StopCoroutine(energyDrainCoroutine);
            energyDrainCoroutine = null;
        }
    }

    IEnumerator DrainEnergy()
    {
        while (isSprinting && player.currentEnergy > 0)
        {
            // Drain energy gradually over time
            player.currentEnergy -= 10 * Time.deltaTime;  // Adjust drain speed here

            if (player.currentEnergy <= 0)
            {
                player.currentEnergy = 0;
                StopSprinting();
            }

            yield return null;  // Wait for the next frame
        }
    }
    
}