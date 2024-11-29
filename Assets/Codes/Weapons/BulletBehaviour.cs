using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : ProjectileBehaviour
{
    private BulletController bc;
    private Vector3 direction;  // To store the bullet's movement direction

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        bc = FindObjectOfType<BulletController>();
    }

    // Method to set the bullet's direction
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;  // Normalize to ensure consistent speed
        transform.forward = direction;  // Make the bullet face the target direction
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the specified direction at the set speed
        if (direction != Vector3.zero)
        {
            transform.position += direction * bc.speed * Time.deltaTime;
        }
    }
}
