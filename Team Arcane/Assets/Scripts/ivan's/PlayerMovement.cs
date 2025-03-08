// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float speed = 3.0f;
//     public Animator animator;
//     private CharacterController controller;
//     private Vector3 moveDirection;

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//     }

//     void Update()
//     {
//         float moveX = Input.GetAxis("Horizontal"); // A/D
//         float moveZ = Input.GetAxis("Vertical");   // W/S

//         // Move in the direction of input
//         moveDirection = new Vector3(moveX, 0, moveZ).normalized;

//         if (moveDirection.magnitude > 0)
//         {
//             // Rotate the character to face movement direction
//             transform.forward = moveDirection;

//             // Play walking animation
//             animator.SetBool("isWalking", true);
//         }
//         else
//         {
//             // Stop walking animation
//             animator.SetBool("isWalking", false);
//         }

//         // Move the character
//         controller.Move(moveDirection * speed * Time.deltaTime);
//     }
// }
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotationSpeed = 1.0f;
    public float verticalAngleLimit = 85.0f;
    public static Vector3 currentRotation;
    
    Rigidbody rb;
    public Animator animator;
    private CharacterController controller;
    private Vector3 moveDirection;

    // Reference to the player mesh (the child object)
    public Transform playerMesh;  // Assign the character mesh in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void MovePlayer()
{
    Vector3 direction = new Vector3(0, 0, 0);

    // Reset the "isWalking" flag initially
    animator.SetBool("isWalking", false);

    if (Input.GetKey(KeyCode.W))
    {
        direction += Camera.main.transform.forward;
    }
    if (Input.GetKey(KeyCode.S))
    {
        direction -= Camera.main.transform.forward;
    }
    if (Input.GetKey(KeyCode.A))
    {
        direction -= Camera.main.transform.right;
    }
    if (Input.GetKey(KeyCode.D))
    {
        direction += Camera.main.transform.right;
    }

    // If the player is moving, play walking animation
    if (direction.magnitude > 0)
    {
        animator.SetBool("isWalking", true);
    }

    Vector3 velocity = direction.normalized * speed;
    rb.linearVelocity = velocity; // Update Rigidbody's velocity
}

    void Rotate()
    {
        //Get "strength" of horizontal and verical mouse movements
        currentRotation.x += Input.GetAxis("Mouse X") * rotationSpeed;
        currentRotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed;

        //X rotation is looped based on 360 degrees
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);

        //Y is clamped so the camera never flips
        currentRotation.y = Mathf.Clamp(currentRotation.y, -verticalAngleLimit, verticalAngleLimit);

        //rotate the player's view
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
    void Update()
    {
        MovePlayer();
        Rotate();   
    }
}
