using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HeroMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 7f;
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    public Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the Rigidbody from rotating
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

        // Adjust movement speed based on sprint
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Move the character
        MoveCharacter(move, currentSpeed);

        // Make the character face the mouse cursor
    }

    void MoveCharacter(Vector3 move, float speed)
    {
        // Apply movement to the Rigidbody
        Vector3 movement = move.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void MouseFollow()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to world space
        // Set the Z position to a value that makes sense in your game (e.g., 10 units away from the camera)
        mouseScreenPosition.z = mainCamera.nearClipPlane; // Adjust this as needed for your camera's setup
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calculate the angle to rotate the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the player's rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}