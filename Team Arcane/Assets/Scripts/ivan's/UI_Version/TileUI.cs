// // @ -1,75 +1,90 @@
// using System;
// using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
// using UnityEngine;
// using UnityEngine.UI;
// public class TileUI : MonoBehaviour
// {
//     public bool isStart = false;
//     public bool isEnd = false;
//     public bool isNode = false;
//     public bool isConnected = false;
//     public Color defaultColor = Color.white;
//     public Color nodeColor = Color.white;
//     public bool activated = false;
//     // private Renderer rend; THIS IS EXCLUSIVELY FOR OBJECTS?
//     // Image img;
//     private LineRenderer lineRenderer;
//     public Color currentColor;

//     // **Track the node that initiated this path**
//     public TileUI startNode = null;
//     public TileUI endNode = null;

//     // Track the path from this tile
//     public List<TileUI> connectedPath = new List<TileUI>();

//     void Start()
//     {
//         // rend = GetComponent<Renderer>();
//         UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image>();
//         Initialize();

//         // Set up LineRenderer for wires
//         lineRenderer = gameObject.AddComponent<LineRenderer>();
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//         lineRenderer.startWidth = 0.1f;
//         lineRenderer.endWidth = 0.1f;
//         lineRenderer.positionCount = 0;
//     }

//     public void createNode(bool isNode, Color color)
//     {
//         this.isNode = isNode;
//         SetNodeColor(color);
//     }

//     public void SetConnected(bool connected, Color selectedColor, TileUI start, TileUI end = null)
//     {
//         isConnected = connected;
//         if (connected)
//         {
//             activated = true;
//             startNode = start;  // Store the node that started this path
//             endNode = end;
//         }
//         else
//         {
//             activated = false;
//             startNode = null;  // Reset when disconnected
//             endNode = null;
//         }
//         lineRenderer.material.color = selectedColor;
//     }

//     public void Initialize()
//     {
//         if (isNode || isStart || isEnd)
//         {
//             // rend.material.color = nodeColor;
//             currentColor = nodeColor;
//         }
//         else
//         {
//             // rend.material.color = defaultColor;
//             currentColor = defaultColor;
//         }
//     }

//     public void SetNodeColor(Color color)
//     {
//         nodeColor = color;
//     }

//     public void DrawWireTo(TileUI otherTile)
//     {
//         lineRenderer.positionCount = 2;
//         lineRenderer.SetPosition(0, transform.position + Vector3.up * 0.1f);
//         lineRenderer.SetPosition(1, otherTile.transform.position + Vector3.up * 0.1f);
//         if (otherTile.isNode)
//         {
//             otherTile.isConnected = true;
//         }
//     }

//     public void ClearWire()
//     {
//         lineRenderer.positionCount = 0;
//     }

//     // **Clear only the path associated with this node**
//     public void ClearPath(TileUI startNode)
//     {
//         List<TileUI> tilesToClear = new List<TileUI>();

//         foreach (TileUI tile in connectedPath)
//         {
//             if (tile.startNode == startNode)  // Only clear tiles that belong to the selected path
//             {
//                 tilesToClear.Add(tile);
//             }
//         }

//         foreach (TileUI tile in tilesToClear)
//         {
//             tile.SetConnected(false, defaultColor, null);
//             tile.ClearWire();
//         }

//         connectedPath.RemoveAll(t => t.startNode == startNode);
//     }
// }
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    public bool isStart = false;
    public bool isEnd = false;
    public bool isNode = false;
    public bool isConnected = false;
    public Color defaultColor = Color.white;
    public Color nodeColor = Color.white;
    public bool activated = false;

    private Image img;
    public Color currentColor;

    // Rectangle wire
    private RectTransform wireRectTransform;
    private Image wireImage;

    // **Track the node that initiated this path**
    public TileUI startNode = null;
    public TileUI endNode = null;

    // Track the path from this tile
    public List<TileUI> connectedPath = new List<TileUI>();

    void Start()
    {
        img = GetComponent<Image>();
        Initialize();
    }

    public void createNode(bool isNode, Color color)
    {
        this.isNode = isNode;
        SetNodeColor(color);
    }

    public void SetConnected(bool connected, Color selectedColor, TileUI start, TileUI end = null)
    {
        isConnected = connected;
        if (connected)
        {
            activated = true;
            startNode = start;
            endNode = end;
        }
        else
        {
            activated = false;
            startNode = null;
            endNode = null;
        }

        if (connected && end != null)
        {
            DrawWireTo(end);
        }
        else
        {
            ClearWire();
        }
    }

    public void Initialize()
    {
        if (isNode || isStart || isEnd)
        {
            img.color = nodeColor;
            currentColor = nodeColor;
        }
        else
        {
            img.color = defaultColor;
            currentColor = defaultColor;
        }
    }

    public void SetNodeColor(Color color)
    {
        nodeColor = color;
        // img.color = color;
    }

    // Draw a rectangle wire between two tiles
    public void DrawWireTo(TileUI otherTile)
    {
        Debug.Log("DRAWING THE WIRE/RECT");
        if (wireRectTransform == null)
        {
            // Create the wire image (rectangle) dynamically
            wireImage = new GameObject("Wire", typeof(Image)).GetComponent<Image>();
            wireRectTransform = wireImage.GetComponent<RectTransform>();
            wireImage.transform.SetParent(transform.parent, false); // Make sure it's under the same Canvas
            wireImage.color = new Color32(54, 240, 151, 255);  // Set the wire color
            wireImage.rectTransform.pivot = new Vector2(0.5f, 0.5f); // Center the pivot of the wire
        }

        Vector3 startPos = transform.position;
        Vector3 endPos = otherTile.transform.position;

        // Set position and size
        wireRectTransform.position = (startPos + endPos) / 2; // Place wire at the midpoint
        Vector3 dir = endPos - startPos;
        float distance = dir.magnitude;

        wireRectTransform.sizeDelta = new Vector2(distance, 5); // Width of 5 for wire thickness (you can change this)
        wireRectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg); // Rotate to match the direction
    }

    public void ClearWire()
    {
        if (wireImage != null)
        {
            Destroy(wireImage.gameObject);
            wireImage = null;
        }
    }

    public void ClearPath(TileUI startNode)
    {
        List<TileUI> tilesToClear = new List<TileUI>();

        foreach (TileUI tile in connectedPath)
        {
            if (tile.startNode == startNode)
            {
                tilesToClear.Add(tile);
            }
        }

        foreach (TileUI tile in tilesToClear)
        {
            tile.SetConnected(false, defaultColor, null);
            tile.ClearWire();
        }

        connectedPath.RemoveAll(t => t.startNode == startNode);
    }
}
