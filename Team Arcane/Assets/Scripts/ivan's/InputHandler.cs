using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Tile startTile = null;
    private List<Tile> currentPath = new List<Tile>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Start drawing
        {
            Tile clickedTile = GetTileUnderMouse();
            if (clickedTile != null && clickedTile.isStart)
            {
                startTile = clickedTile;
                currentPath.Clear();
                currentPath.Add(startTile);
                clickedTile.SetConnected(true);
            }
        }

        if (Input.GetMouseButton(0) && startTile != null) // Dragging over tiles
        {
            Tile hoveredTile = GetTileUnderMouse();
            if (hoveredTile != null && !currentPath.Contains(hoveredTile))
            {
                Tile lastTile = currentPath[currentPath.Count - 1];
                
                // Only connect adjacent tiles
                if (AreTilesAdjacent(lastTile, hoveredTile))
                {
                    currentPath.Add(hoveredTile);
                    hoveredTile.SetConnected(true);
                    lastTile.DrawWireTo(hoveredTile);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && startTile != null) // Stop drawing
        {
            Tile endTile = currentPath[currentPath.Count - 1];
            if (endTile.isEnd)
            {
                Debug.Log("Valid Connection!");
            }
            else
            {
                // Clear path if invalid
                foreach (Tile tile in currentPath)
                {
                    tile.SetConnected(false);
                    tile.ClearWire();
                }
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
}
