using UnityEngine;

public class CameraTargetFollow : MonoBehaviour
{
    public Transform characterRoot; // Assign the main player object (not the head bone)
    public Vector3 offset = new Vector3(0, 1.6f, 0); // Adjust height for the head level

    void LateUpdate()
    {
        if (characterRoot != null)
        {
            transform.position = characterRoot.position + offset; // Smoothly follow
            transform.rotation = Quaternion.Euler(0, characterRoot.eulerAngles.y, 0); // Optional: Only follow Y rotation
        }
    }
}
