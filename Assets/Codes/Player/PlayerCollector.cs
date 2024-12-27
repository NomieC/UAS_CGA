using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollector : MonoBehaviour
{
    CharacterStatus player;          // Reference to player stats
    SphereCollider playerCollector;  // 3D sphere collider for magnet effect
    public float magnetStrength; // Strength of the magnet effect

    void Start()
    {
        // Get the CharacterStatus and SphereCollider components
        player = FindObjectOfType<CharacterStatus>();
        playerCollector = GetComponent<SphereCollider>();

        // Ensure the collider acts as a trigger
        playerCollector.isTrigger = true;
    }

    void Update()
    {
        // Adjust the magnet radius based on the player's current magnet range
        if (player != null && playerCollector != null)
        {
            playerCollector.radius = player.currentMagnetRange;
        }
    }

    // Detect collectibles that enter the magnet radius
    private void OnTriggerEnter(Collider col)
    {
        // Check if the object implements ICollectible
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = (transform.position - col.transform.position).normalized;
            rb.AddForce(direction * magnetStrength, ForceMode.Impulse);
            // Collect the item
            collectible.Collect();
        }
    }
}