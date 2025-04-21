using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 2.5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float upDownRange = 35f;
    public float gravity = 0;
    private float sprintDelta = 1f;

    [SerializeField] private Camera playerCamera;
    private CharacterController characterController;
    [SerializeField] private Animator animator;
    private Transform playerBody;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private Transform modelAnchor;


    private float rotationX = 0;
    private Vector3 velocity; // Stores gravity effects

    void Start()
    {
        if (playerCamera == null)
            Debug.LogError("Player Camera not assigned!");

        if (animator == null)
            Debug.LogError("Animator not assigned!");

        if (footstep == null)
            Debug.LogError("Footstep not assigned!");
        else
            footstep.gameObject.SetActive(false);

        characterController = GetComponent<CharacterController>();
        playerBody = transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintDelta = 2f;
        }
        else
        {
            sprintDelta = 1f;
        }
        if (MenuController.currentMenu == 0 && SceneChanger.isFading == false)
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
            move *= (speed * sprintDelta);

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
            if (isWalking)
            {
                footstep.gameObject.SetActive(true);
            }
            else
            {
                footstep.gameObject.SetActive(false);
            }

            if (animator != null && modelAnchor != null)
            {
                animator.transform.position = modelAnchor.position;
                animator.transform.rotation = modelAnchor.rotation;
            }

        }
        else
        {
            animator.SetBool("isWalking", false);
            footstep.gameObject.SetActive(false);
        }
        //else if (MenuController.currentMenu == 1)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
    }
}
