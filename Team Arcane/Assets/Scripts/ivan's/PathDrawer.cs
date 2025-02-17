using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer for the path
    private Vector3[] pathPoints; // Array of path points (tile positions)

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Draw path between tiles
    public void DrawPath(Vector3[] points)
    {
        pathPoints = points;
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
