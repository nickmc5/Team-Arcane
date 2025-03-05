using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Assign the character root in Inspector

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera up/down (clamping to prevent flipping)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent looking too far up/down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player body left/right
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
