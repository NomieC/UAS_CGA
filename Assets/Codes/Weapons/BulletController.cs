using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Shoot()
    {
        base.Shoot();

        // Instantiate the bullet prefab
        GameObject spawnedBullet = Instantiate(prefab);
        spawnedBullet.transform.position = transform.position;

        // Calculate the direction to the mouse position
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition); // Create a ray from the camera to the mouse position
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);  // Plane at ground level (Y=0)
        float distanceToGround;
        
        // Check where the ray intersects the ground
        if (groundPlane.Raycast(ray, out distanceToGround))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distanceToGround); // The intersection point in world space

            // Calculate the direction vector from the bullet to the mouse position
            Vector3 direction = mouseWorldPosition - transform.position;
            direction.y = 0;  // Ensure that the bullet only moves along the XZ plane

            // Get the BulletBehaviour component and set its direction
            BulletBehaviour bulletBehaviour = spawnedBullet.GetComponent<BulletBehaviour>();
            if (bulletBehaviour != null)
            {
                bulletBehaviour.SetDirection(direction.normalized);  // Normalize the direction vector for consistent speed
            }

            // Optionally, set the rotation of the bullet to face the direction
            spawnedBullet.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
