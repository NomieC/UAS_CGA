using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    CharacterStatus player;          
    SphereCollider playerCollector;  
    public float magnetStrength = 10f;  // Strength of the magnet effect
    public float stopDistance = 1f;     // Distance at which collectible stops moving towards player

    private HashSet<Rigidbody> attractedObjects = new HashSet<Rigidbody>();

    void Start()
    {
        player = FindObjectOfType<CharacterStatus>();
        playerCollector = GetComponent<SphereCollider>();
        playerCollector.isTrigger = true;  // Ensure the collider acts as a trigger
    }

    void Update()
    {
        // Adjust the magnet radius based on the player's current magnet range
        if (player != null && playerCollector != null)
        {
            playerCollector.radius = player.currentMagnetRange;
        }

        // Apply force to all collected objects to pull them continuously
        AttractCollectibles();
    }

    private void AttractCollectibles()
    {
        foreach (Rigidbody rb in attractedObjects)
        {
            if (rb != null)
            {
                Vector3 direction = (transform.position - rb.position).normalized;
                float distance = Vector3.Distance(transform.position, rb.position);

                if (distance > stopDistance)
                {
                    rb.AddForce(direction * magnetStrength * Time.deltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    // Collect the item when close enough
                    if (rb.TryGetComponent(out ICollectible collectible))
                    {
                        collectible.Collect();
                        attractedObjects.Remove(rb);
                        Destroy(rb.gameObject);  // Optionally destroy the collectible after collection
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                attractedObjects.Add(rb);
            }
        }
    }

    // private void OnTriggerExit(Collider col)
    // {
    //     if (col.gameObject.TryGetComponent(out ICollectible collectible))
    //     {
    //         Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
    //         if (rb != null && attractedObjects.Contains(rb))
    //         {
    //             attractedObjects.Remove(rb);  // Stop attracting if it leaves the magnet radius
    //         }
    //     }
    // }
}