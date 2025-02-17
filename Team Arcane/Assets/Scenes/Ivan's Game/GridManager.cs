using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 5;  // Grid width
    public int height = 5; // Grid height
    public float cellSize = 1.0f;  // Default size (can be adjusted based on the prefab)
    public float cellPadding = 0.1f; // Additional padding between cells
    public GameObject gridPrefab;  // Prefab for visualizing the grid

    private GameObject[,] grid; // Array to store the grid cells (private)
    private List<Vector2> points = new List<Vector2>();  // List of points for pairs (private)
    private List<Color> pairColors = new List<Color>();  // Pair colors (private)

    private Vector3 startPoint = Vector3.zero;  // Start point of the path

    private void Start()
    {
        // Calculate the prefab size using its mesh renderer bounds
        Vector3 prefabSize = gridPrefab.GetComponent<Renderer>().bounds.size;
        Debug.Log("Prefab Size: " + prefabSize);

        // Update cellSize to match the prefab size or scale it down to avoid overlap
        cellSize = Mathf.Max(prefabSize.x, prefabSize.z, prefabSize.y);  // Use the largest dimension as the cell size
        Debug.Log("Using cellSize: " + cellSize);

        GenerateGrid();
        DefinePairs();
        MarkPoints();
    }

    // Mark the grid cells corresponding to the defined points
    void MarkPoints()
    {
        for (int i = 0; i < points.Count; i += 2)
        {
            Vector2 point1 = points[i];
            Vector2 point2 = points[i + 1];

            // Get the color for the current pair
            Color pairColor = pairColors[i / 2]; // Every two points correspond to one pair

            // Mark the points with the color
            grid[(int)point1.x, (int)point1.y].GetComponent<Renderer>().material.color = pairColor;
            grid[(int)point2.x, (int)point2.y].GetComponent<Renderer>().material.color = pairColor;
        }
    }

    // Define pairs of points
    void DefinePairs()
    {
        points.Add(new Vector2(0, 0));  // Point 1
        points.Add(new Vector2(4, 4));  // Point 1 (End)

        points.Add(new Vector2(1, 2));  // Point 2
        points.Add(new Vector2(3, 1));  // Point 2 (End)

        points.Add(new Vector2(2, 4));  // Point 3
        points.Add(new Vector2(0, 2));  // Point 3 (End)

        // Assign random colors for each pair (you can use specific colors if you want)
        pairColors.Add(Color.red);
        pairColors.Add(Color.green);
        pairColors.Add(Color.blue);
    }

    // Generate the grid of cells
    void GenerateGrid()
    {
        grid = new GameObject[width, height];  // Initialize the grid array

        Debug.Log("Generating Grid...");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Add padding to ensure no overlap
                Vector3 pos = new Vector3(x * (cellSize + cellPadding), 0, y * (cellSize + cellPadding));
                GameObject cell = Instantiate(gridPrefab, pos, Quaternion.identity, transform);
                grid[x, y] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Detect mouse input for starting/updating paths
        if (Input.GetMouseButtonDown(0))  // Mouse down to start
        {
            Vector3 mousePosition = GetMousePositionInGrid();
            startPoint = mousePosition; // Set the starting point
            ChangeCellColor(mousePosition, Color.yellow); // Highlight the start point
        }
        else if (Input.GetMouseButton(0)) // Mouse drag to update path
        {
            Vector3 mousePosition = GetMousePositionInGrid();
            ChangeCellColor(mousePosition, Color.yellow); // Update the path
        }
        else if (Input.GetMouseButtonUp(0)) // Mouse release to finish path
        {
            FinishPath();
        }
    }

    // Get the mouse position in the grid (raycast from mouse to grid)
    // Get the mouse position in the grid (raycast from mouse to grid)
   // Get the mouse position in the grid (raycast from mouse to grid)
Vector3 GetMousePositionInGrid()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
        // Calculate the snapped position on the grid
        float snappedX = Mathf.Floor(hit.point.x / (cellSize + cellPadding)) * (cellSize + cellPadding);
        float snappedZ = Mathf.Floor(hit.point.z / (cellSize + cellPadding)) * (cellSize + cellPadding);
        
        // Return the snapped position (with y = 0 for grid height)
        return new Vector3(snappedX, 0f, snappedZ);
    }
    return Vector3.zero;
}



    // Change the color of the grid cell that corresponds to the mouse position
    void ChangeCellColor(Vector3 position, Color color)
    {
        // Convert world position to grid cell coordinates
        int x = Mathf.FloorToInt(position.x / (cellSize + cellPadding));
        int y = Mathf.FloorToInt(position.z / (cellSize + cellPadding));

        // Check if the coordinates are valid and within the grid bounds
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            grid[x, y].GetComponent<Renderer>().material.color = color;  // Change the color of the grid cell
        }
    }

    // Finish the path and check for validity
    void FinishPath()
    {
        // Reset the colors after path is drawn or complete your logic for path validation
        Debug.Log("Path Finished!");
    }

    // Optional: Public function to get the grid (read-only)
    public GameObject GetGridCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x, y];
        }
        return null;
    }

    // Optional: Public function to get points (read-only)
    public List<Vector2> GetPoints()
    {
        return new List<Vector2>(points);  // Return a copy to avoid direct modification
    }
}
