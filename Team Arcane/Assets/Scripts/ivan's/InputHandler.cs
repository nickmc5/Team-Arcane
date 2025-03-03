// @ -1,81 +1,115 @@
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Tile startTile = null;
    private List<Tile> currentPath = new List<Tile>();
    private Color selecterColor = Color.white;
    private Dictionary<Tile, List<Tile>> paths = new Dictionary<Tile, List<Tile>>();
    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) // Start drawing
        if (Input.GetMouseButtonDown(0)) // Start drawing or clearing
        {
            Tile clickedTile = GetTileUnderMouse();
            if (clickedTile != null && clickedTile.isNode)
            {
                if (clickedTile.isConnected) // If it's already connected, clear path
                {
                    ClearPath(clickedTile);
                    return;
                }

                // Otherwise, start drawing a new path
                startTile = clickedTile;
                selecterColor = startTile.currentColor;
                Debug.Log("COLOR IS: " + selecterColor);
                currentPath.Clear();
                currentPath.Add(startTile);
                Debug.Log("This is the starting tile copy: " + startTile);
                clickedTile.SetConnected(true, selecterColor);
            }
        }

        if (Input.GetMouseButton(0) && startTile != null) // Dragging over tiles
        {
            Tile hoveredTile = GetTileUnderMouse();
            // if (hoveredTile != null && !currentPath.Contains(hoveredTile))
            if (hoveredTile != null && !currentPath.Contains(hoveredTile) && !hoveredTile.activated) // If it's a new tile
            {
                if(hoveredTile.isNode && hoveredTile.nodeColor != selecterColor){
                    return;
                }
                Tile lastTile = currentPath[currentPath.Count - 1];
                
                // Only connect adjacent tiles

                if (AreTilesAdjacent(lastTile, hoveredTile))
                {
                    Debug.Log("COLOR: "+ selecterColor);
                    currentPath.Add(hoveredTile);
                    hoveredTile.SetConnected(true, selecterColor);
                    lastTile.DrawWireTo(hoveredTile);
                    if(hoveredTile.isNode){
                        startTile = null;
                        currentPath.Clear();
                    }
                }
                
            }
        }

        if (Input.GetMouseButtonUp(0) && startTile != null) // Stop drawing
        {
            Tile endTile = currentPath[currentPath.Count - 1];
            // if (endTile.isNode)
            if (endTile.isNode && endTile.nodeColor == selecterColor)
            {
                Debug.Log("Valid Connection!");
            }
            else
            {
                // Clear path if invalid
                // foreach (Tile tile in currentPath)
                // {
                //     tile.SetConnected(false, selecterColor);
                //     tile.ClearWire();
                // }
                ClearPath(startTile); // Clear if invalid
                Debug.Log("Invalid Connection. Try again.");
            }

            startTile = null;
            currentPath.Clear();
        }
    }

    Tile GetTileUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.GetComponent<Tile>();
        }
        return null;
    }

    bool AreTilesAdjacent(Tile a, Tile b)
    {
        float distance = Vector3.Distance(a.transform.position, b.transform.position);
        return distance < 1.2f; // Adjust as needed for spacing
    }

    void ClearPath(Tile nodeTile)
    {
        List<Tile> tilesToClear = new List<Tile>();

        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (tile.isConnected)
            {
                tilesToClear.Add(tile);
            }
        }

        foreach (Tile tile in tilesToClear)
        {
            tile.SetConnected(false, selecterColor);
            tile.ClearWire();
        }

        Debug.Log("Path cleared.");
    }
}