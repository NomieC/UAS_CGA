using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereToShoot : MonoBehaviour
{
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
    }

    void MouseFollow()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to world space
        // We will calculate the correct Z value for the ground level (e.g., y = 0 or player height)
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Plane aligned with the XZ plane (Y = 0)
        float distanceToGround;
        
        // Calculate where the ray intersects the ground plane
        if (groundPlane.Raycast(ray, out distanceToGround))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distanceToGround);

            // Get the direction to the mouse position (on the XZ plane)
            Vector3 direction = mouseWorldPosition - transform.position;
            direction.y = 0; // Keep the player facing in the same vertical plane (XZ plane only)

            // Rotate the player to face the mouse on the ground plane
            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}
