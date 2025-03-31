using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 2.5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float upDownRange = 35f;
    public float gravity = 0;

    private Camera playerCamera;
    private CharacterController characterController;
    private Animator animator;
    private Transform playerBody;
    private AudioSource footstep;

    private float rotationX = 0;
    private Vector3 velocity; // Stores gravity effects

    void Start()
    {
        // Get references
        playerCamera = GetComponentInChildren<Camera>();  
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        footstep = GetComponentInChildren<AudioSource>();
        footstep.gameObject.SetActive(false);
        playerBody = transform; 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (MenuController.currentMenu == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // Camera Rotation
            float rotationY = Input.GetAxis("Mouse X") * lookSpeedX;
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);

            // Apply rotation
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            playerBody.Rotate(Vector3.up * rotationY);

            // Handle Movement
            float moveDirectionX = Input.GetAxis("Horizontal");
            float moveDirectionZ = Input.GetAxis("Vertical");

            Vector3 move = playerBody.right * moveDirectionX + playerBody.forward * moveDirectionZ;
            move *= speed;

            // Apply gravity
            if (characterController.isGrounded)
            {
                velocity.y = -0.5f; // Small downward force to ensure grounding
            }
            else
            {
                velocity.y -= gravity * Time.deltaTime; // Apply gravity when in the air
            }

            // Move Character
            characterController.Move((move + velocity) * Time.deltaTime);
            
            // Update Animator
            bool isWalking = moveDirectionX != 0 || moveDirectionZ != 0;
            animator.SetBool("isWalking", isWalking);
            if(isWalking){
                footstep.gameObject.SetActive(true);
            }else{
                footstep.gameObject.SetActive(false);
            }
        }
        //else if (MenuController.currentMenu == 1)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
    }
}
