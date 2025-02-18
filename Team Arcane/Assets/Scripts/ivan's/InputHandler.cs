using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    private Tile startTile = null;
    private List<Tile> currentPath = new List<Tile>();
    public Camera mainCamera;
    private GameObject selectedTile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Tile clickedTile = GetTileUnderMouse();
            if(clickedTile != null && clickedTile.isStart){
                startTile = clickedTile;
                currentPath.Clear();
                currentPath.Add(startTile);
                clickedTile.SetConnected(true);
            }
        }
        if (Input.GetMouseButton(0) && startTile != null)
        {
            Tile hoveredTile = GetTileUnderMouse();
            if (hoveredTile != null && !currentPath.Contains(hoveredTile))
            {
                currentPath.Add(hoveredTile);
                hoveredTile.SetConnected(true);
            }
        }

         if (Input.GetMouseButtonUp(0) && startTile != null)
        {
            Tile endTile = currentPath[currentPath.Count - 1];
            if (endTile.isEnd)
            {
                Debug.Log("Valid Connection!");
            }
            else
            {
                // Clear the path if invalid
                foreach (Tile tile in currentPath)
                {
                    tile.SetConnected(false);
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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.GetComponent<Tile>();
        }
        return null;
    }
}
