using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour
{
    public RectTransform lineRect;
    public Image lineImage;

    // Call this function to draw a line between two UI points
    public void DrawUILine(Vector2 startPos, Vector2 endPos, float thickness, Color color)
    {
        // Set the position of the line
        Vector2 direction = (endPos - startPos).normalized;
        float distance = Vector2.Distance(startPos, endPos);

        // Ensure the Image and RectTransform exist
        if (lineImage == null) lineImage = GetComponent<Image>();
        if (lineRect == null) lineRect = GetComponent<RectTransform>();

        // Set size and rotation
        lineRect.sizeDelta = new Vector2(distance, thickness);
        lineRect.position = (startPos + endPos) / 2f; // Position in between start and end
        lineRect.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        // Set color
        lineImage.color = color;
    }
}
