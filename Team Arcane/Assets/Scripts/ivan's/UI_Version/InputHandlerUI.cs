// @ -1,81 +1,115 @@
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InputHandlerUI : MonoBehaviour
{
    private TileUI startTile = null;
    private List<TileUI> currentPath = new List<TileUI>();
    private Color selecterColor = Color.white;
    private Dictionary<TileUI, List<TileUI>> paths = new Dictionary<TileUI, List<TileUI>>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Start drawing or clearing
        {
            TileUI clickedTile = GetTileUnderMouse();
            if (clickedTile != null && clickedTile.isNode)
            {
                // **Find and clear the path if the node is either a start or end node**
                TileUI pathKey = FindPathKey(clickedTile);
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
            // Debug.Log("Dragging");
            TileUI hoveredTile = GetTileUnderMouse();
            if (hoveredTile != null && !currentPath.Contains(hoveredTile) && !hoveredTile.activated)
            {
                // Debug.Log("Hovered over a tile");
                if (hoveredTile.isNode && hoveredTile.nodeColor != selecterColor)
                {
                    return;
                }
                // Debug.Log("Hovered over a tile2");
                TileUI lastTile = currentPath[currentPath.Count - 1];

                if (AreTilesAdjacent(lastTile, hoveredTile))
                {
                    // Debug.Log("Hovered over a tile3");
                    currentPath.Add(hoveredTile);
                    hoveredTile.SetConnected(true, selecterColor, startTile);
                    lastTile.DrawWireTo(hoveredTile);
                    if (hoveredTile.isNode)
                    {
                        // Store the path using both start and end node
                        paths[startTile] = new List<TileUI>(currentPath);
                        paths[hoveredTile] = paths[startTile]; // Store under both keys
                        startTile = null;
                        currentPath.Clear();
                        Debug.Log("Finished 2");
                        //INCREASE COUNT FOR WIN CONDITION
                        GridBehaviorUI.connectedNodes++;
                        //GridBehaviorUI.CheckWin();
                        this.GetComponent<GridBehaviorUI>().CheckWin();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && startTile != null) // Stop drawing
        {
            if (currentPath.Count > 1) // **Ensure there is more than one tile in the path**
            {
                TileUI endTile = currentPath[currentPath.Count - 1];

                if (endTile == startTile) // **Prevent same start and end node**
                {
                    ClearPath(startTile);
                }
                else if (endTile.isNode && endTile.nodeColor == selecterColor)
                {
                    // Store the path using both start and end nodes
                    Debug.Log("Finished 1");
                    paths[startTile] = new List<TileUI>(currentPath);
                    paths[endTile] = paths[startTile];
                    // gridBehavior.CheckWinCondition();
                }
                else
                {
                    // Ensure incomplete paths are cleared
                    foreach (TileUI tile in currentPath)
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

    TileUI GetTileUnderMouse()
    {
        // Get the event system (needed for UI raycasts)
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null) return null;

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition // Get mouse position
        };

        // Perform UI Raycast
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = FindFirstObjectByType<GraphicRaycaster>(); // Get the Canvas' raycaster
        if (raycaster != null)
        {
            raycaster.Raycast(pointerData, results);
        }

        // Check if we hit a TileUI
        foreach (RaycastResult result in results)
        {
            TileUI tile = result.gameObject.GetComponent<TileUI>();
            if (tile != null)
            {
                // Debug.Log("HIT a Tile UI!");
                return tile;
            }
        }

        return null;
    }

    bool AreTilesAdjacent(TileUI a, TileUI b)
{
    // Get the RectTransforms of both tiles
    RectTransform aRect = a.GetComponent<RectTransform>();
    RectTransform bRect = b.GetComponent<RectTransform>();

    // Calculate the distance between the two tiles' anchored positions
    float distance = Vector2.Distance(aRect.anchoredPosition, bRect.anchoredPosition);

    // Return whether the distance is less than the threshold (this was calculated by the size of the tiles + spacing lol)
    return distance < 150f;
}


    // **Find the correct path to clear when clicking on a node**
    TileUI FindPathKey(TileUI nodeTile)
    {
        if (paths.ContainsKey(nodeTile))
        {
            return nodeTile;
        }

        // Try finding it as an end node in another path
        foreach (var kvp in paths)
        {
            List<TileUI> pathTiles = kvp.Value;
            if (pathTiles.Contains(nodeTile) && (pathTiles[0] == nodeTile || pathTiles[pathTiles.Count - 1] == nodeTile))
            {
                return kvp.Key;
            }
        }
        return null;
    }

    // **Clear the entire path when either start or end node is clicked**

    //Used chat to help me edit this function...turns out I was having an issue bc I was storing both start and end keys as basically different paths, so while I deleted one, it did NOT delete the other. So even though they were not connected together, it still treated each node as connected after they were connected once...so now it deletes both!
    void ClearPath(TileUI nodeTile)
    {
        // Check if the node has an associated path
        if (paths.ContainsKey(nodeTile))
        {
            List<TileUI> pathTiles = paths[nodeTile];

            // Clear all tiles in the path
            foreach (TileUI tile in pathTiles)
            {
                tile.SetConnected(false, selecterColor, null, null);
                tile.ClearWire();
            }

            // Remove the path from the dictionary using both the start and end nodes
            TileUI otherTile = pathTiles[0] == nodeTile ? pathTiles[pathTiles.Count - 1] : pathTiles[0];

            // Remove both start and end node entries from the paths dictionary
            paths.Remove(nodeTile);  // Remove the path associated with the clicked node
            paths.Remove(otherTile); // Remove the path associated with the other node in the path

            Debug.Log("Path cleared and removed from paths dictionary.");
            
            // If the path contains more than one tile, decrement connectedNodes
            if (pathTiles.Count > 1)
            {
                GridBehaviorUI.connectedNodes--;
                Debug.Log($"Path had multiple nodes, current connections: {GridBehaviorUI.connectedNodes}");
            }
        }
        else
        {
            // Handle case where no path exists for the node, just clear the node
            nodeTile.SetConnected(false, selecterColor, null, null);
            nodeTile.ClearWire();
            Debug.Log("No path found for the clicked node, just clearing the node.");
        }
    }


}
