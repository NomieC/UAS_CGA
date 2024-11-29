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
    }

    void MoveCharacter(Vector3 move, float speed)
    {
        // Apply movement to the Rigidbody
        Vector3 movement = move.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }


}