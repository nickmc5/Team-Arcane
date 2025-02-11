using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float spacing = 1.1f; // Spacing between tiles

    private Tile[,] grid; // Store the grid tiles
    private Tile selectedTile = null;
    void Start()
    {
        grid = new Tile[gridWidth, gridHeight];
        GenerateGrid();
        AssignStartAndEndTiles();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * spacing, 0, y * spacing);
                GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);

                Tile tile = tileObject.GetComponent<Tile>();
                if (tile == null)
                {
                    tile = tileObject.AddComponent<Tile>(); // Add if missing
                }
                grid[x, y] = tile;
                tile.name = $"Tile_{x}_{y}"; // Helpful for debugging
            }
        }
    }

    void AssignStartAndEndTiles()
    {
        // Example: Set top-left as Start, bottom-right as End
        Tile startTile = grid[0, 0];
        Tile endTile = grid[gridWidth - 1, gridHeight - 1];

        startTile.isStart = true;
        endTile.isEnd = true;

        startTile.GetComponent<Renderer>().material.color = Color.blue;  // Visual indicator
        endTile.GetComponent<Renderer>().material.color = Color.red;     // Visual indicator
    }
}
