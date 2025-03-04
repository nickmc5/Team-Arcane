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
        if (Input.GetMouseButtonDown(0)) // Start drawing or clearing
        {
            Tile clickedTile = GetTileUnderMouse();
            if (clickedTile != null && clickedTile.isNode)
            {
                // **Find and clear the path if the node is either a start or end node**
                Tile pathKey = FindPathKey(clickedTile);
                if (pathKey != null)
                {
                    ClearPath(pathKey);
                    return;
                }

                // Otherwise, start drawing a new path
                startTile = clickedTile;
                selecterColor = startTile.currentColor;
                currentPath.Clear();
                currentPath.Add(startTile);
                clickedTile.SetConnected(true, selecterColor, startTile);
            }
        }

        if (Input.GetMouseButton(0) && startTile != null) // Dragging over tiles
        {
            Tile hoveredTile = GetTileUnderMouse();
            if (hoveredTile != null && !currentPath.Contains(hoveredTile) && !hoveredTile.activated)
            {
                if (hoveredTile.isNode && hoveredTile.nodeColor != selecterColor)
                {
                    return;
                }

                Tile lastTile = currentPath[currentPath.Count - 1];

                if (AreTilesAdjacent(lastTile, hoveredTile))
                {
                    currentPath.Add(hoveredTile);
                    hoveredTile.SetConnected(true, selecterColor, startTile);
                    lastTile.DrawWireTo(hoveredTile);
                    if (hoveredTile.isNode)
                    {
                        // Store the path using both start and end node
                        paths[startTile] = new List<Tile>(currentPath);
                        paths[hoveredTile] = paths[startTile]; // Store under both keys
                        startTile = null;
                        currentPath.Clear();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && startTile != null) // Stop drawing
        {
            if (currentPath.Count > 1) // **Ensure there is more than one tile in the path**
            {
                Tile endTile = currentPath[currentPath.Count - 1];

                if (endTile == startTile) // **Prevent same start and end node**
                {
                    ClearPath(startTile);
                }
                else if (endTile.isNode && endTile.nodeColor == selecterColor)
                {
                    // Store the path using both start and end nodes
                    paths[startTile] = new List<Tile>(currentPath);
                    paths[endTile] = paths[startTile];
                    // gridBehavior.CheckWinCondition();
                }
                else
                {
                    // Ensure incomplete paths are cleared
                    foreach (Tile tile in currentPath)
                    {
                        tile.SetConnected(false, selecterColor, null, null);
                        tile.ClearWire();
                    }
                }
            }
            else
            {
                // If the path is just a single click, clear it
                ClearPath(startTile);
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
        return distance < 1.2f;
    }

    // **Find the correct path to clear when clicking on a node**
    Tile FindPathKey(Tile nodeTile)
    {
        if (paths.ContainsKey(nodeTile))
        {
            return nodeTile;
        }

        // Try finding it as an end node in another path
        foreach (var kvp in paths)
        {
            List<Tile> pathTiles = kvp.Value;
            if (pathTiles.Contains(nodeTile) && (pathTiles[0] == nodeTile || pathTiles[pathTiles.Count - 1] == nodeTile))
            {
                return kvp.Key;
            }
        }
        return null;
    }

    // **Clear the entire path when either start or end node is clicked**
   void ClearPath(Tile nodeTile)
{
    if (paths.ContainsKey(nodeTile))
    {
        List<Tile> pathTiles = paths[nodeTile];

        // **Check if path is valid before accessing it**
        if (pathTiles.Count > 0)
        {
            foreach (Tile tile in pathTiles)
            {
                tile.SetConnected(false, selecterColor, null, null);
                tile.ClearWire();
            }

            // Only remove if there is a valid end node
            if (pathTiles.Count > 1) 
            {
                Tile endNode = pathTiles[pathTiles.Count - 1];
                paths.Remove(endNode);
            }
        }

        paths.Remove(nodeTile);
    }
}

}
