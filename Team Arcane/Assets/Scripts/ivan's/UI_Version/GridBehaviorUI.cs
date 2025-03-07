using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using TMPro;

public class GridBehaviorUI : MonoBehaviour
{
    public GameObject tilePrefab;   // Assign your tile prefab with an Image component
    public Canvas worldCanvas;      // Assign a world-space Canvas
    public GameObject nodePrefab1;   // Assign your node prefab with an Image component
    public GameObject nodePrefab2;
    public GameObject nodePrefab3;
    public GameObject nodePrefab4;
    public GameObject background1;
    public static int connectedNodes = 0;

    private Dictionary<NodeType, GameObject> nodeVariants;

    public int gridWidth = 5;       // Number of tiles horizontally
    public int gridHeight = 5;      // Number of tiles vertically
    public float spacing = 110f;    // Adjust spacing for world scale

    private TileUI[,] grid;           // Changed to Tile array instead of Image
    public enum NodeType
    {
        nodePrefab1,
        nodePrefab2,
        nodePrefab3,
        nodePrefab4
    }

    void Start()
    {
        grid = new TileUI[gridWidth, gridHeight];
        nodeVariants = new Dictionary<NodeType, GameObject>
        {
            { NodeType.nodePrefab1, nodePrefab1 },
            { NodeType.nodePrefab2, nodePrefab2 },
            { NodeType.nodePrefab3, nodePrefab3 },
            { NodeType.nodePrefab4, nodePrefab4 }
        };

        //Background Layer
       
        //GRID LAYER
        GameObject gridHolder = new GameObject("GridHolder");
        RectTransform gridHolderTransform = gridHolder.AddComponent<RectTransform>();
        gridHolderTransform.SetParent(worldCanvas.transform, false); // Parent to canvas
        float totalWidth = (gridWidth - 1) * spacing;
        float totalHeight = (gridHeight - 1) * spacing;

        // Center the grid holder inside the canvas
        gridHolderTransform.anchoredPosition = new Vector2(-totalWidth / 2, totalHeight / 2);
        GenerateGrid(gridHolderTransform);
        AssignStartAndEndTiles();
    }

    public void CheckWin(){
        //if connected nodes reaches max connections!
        Debug.Log("Current Conenctions: "+ connectedNodes);
        if (connectedNodes >= 4)
        {
            Debug.Log("You Win!");
            this.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Puzzle Completed Press esc to exit";
        }
    }

    void GenerateGrid(Transform parent)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Instantiate the tile inside the worldCanvas
                GameObject tileObject = Instantiate(tilePrefab, parent);

                // Get the RectTransform
                RectTransform rectTransform = tileObject.GetComponent<RectTransform>();

                if (rectTransform == null)
                {
                    Debug.LogError("Tile prefab is missing a RectTransform!");
                    return;
                }

                // Set the anchored position instead of localPosition
                rectTransform.anchoredPosition = new Vector2(x * spacing, -y * spacing); // Negative y to position correctly

                // Ensure it has a Tile component (not Image directly)
                TileUI tileScript = tileObject.GetComponent<TileUI>();
                if (tileScript == null)
                {
                    Debug.LogError("Tile prefab is missing a Tile component!");
                    // tileScript = tileObject.AddComponent<Tile>(); // Add if missing
                    return;
                }

                // Store tile in grid
                grid[x, y] = tileScript;
                tileObject.name = $"Tile_{x}_{y}"; // Debugging purposes
            }
        }
    }

    void AssignStartAndEndTiles()
    {
        // Example: Set top-left as Start, bottom-right as End
        // Tile startTile = grid[0, 0];
        // Tile endTile = grid[gridWidth - 1, gridHeight - 1];
        // Tile startTile2 = grid[1, 4];
        // Tile endTile2 = grid[1, 1];

        SetTileAsNode(0, 4, Color.cyan, NodeType.nodePrefab1); // Set as node and start
        SetTileAsNode(3, 3, Color.cyan, NodeType.nodePrefab1); // Set as node and end

        SetTileAsNode(0, 3, Color.green, NodeType.nodePrefab3); // Set as node and start
        SetTileAsNode(4, 0, Color.green, NodeType.nodePrefab3); // Set as node and end

        SetTileAsNode(4, 1, Color.red, NodeType.nodePrefab2); // Set as node and start
        SetTileAsNode(2, 3, Color.red, NodeType.nodePrefab2); // Set as node and end

        SetTileAsNode(2, 2, Color.blue, NodeType.nodePrefab4); // Set as node and start
        SetTileAsNode(4, 4, Color.blue, NodeType.nodePrefab4); // Set as node and end

    }

    void SetTileAsNode(int x, int y, Color color, NodeType nodeType)
    {
        TileUI tile = grid[x, y];

        GameObject nodePrefab = nodeVariants[nodeType];
        // Overlay node asset on the tile
        GameObject nodeObject = Instantiate(nodePrefab, tile.transform); // Child of tile
        RectTransform nodeTransform = nodeObject.GetComponent<RectTransform>();
        
        // Center node in tile
        nodeTransform.anchoredPosition = Vector2.zero; // Centered inside parent

        // Adjust size if necessary
        nodeTransform.sizeDelta = new Vector2(75, 75); // Adjust size

        // Set the node color
        tile.createNode(true, color); // This calls the `Tile.createNode` method to apply node-related functionality

       
    }


}
