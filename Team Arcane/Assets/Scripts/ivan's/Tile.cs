// @ -1,75 +1,90 @@
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isStart = false;
    public bool isEnd = false;
    public bool isNode = false;
    public bool isConnected = false;
    public Color defaultColor = Color.white;
    // public Color connectedColor = Color.green;
    public Color nodeColor = Color.white;
    public bool activated = false;
    private Renderer rend;
    private LineRenderer lineRenderer;
    public Color currentColor;

    // Track the path from this tile
    public List<Tile> connectedPath = new List<Tile>();

    void Start()
    {
        rend = GetComponent<Renderer>();
        // rend.material.color = defaultColor;

        // // if (isStart) rend.material.color = Color.cyan;
        // // else if (isEnd) rend.material.color = Color.cyan;
        // if(isNode){
        //     r
        // }
        Initialize();

        // Set up LineRenderer for wires
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0;
    }
    public void createNode(Boolean isNode, Color color){
        this.isNode = isNode;
        SetNodeColor(color);
    }

    public void SetConnected(bool connected, Color selectedColor)
    {
        isConnected = connected;
        // if (!isStart && !isEnd)
        // {
            // rend.material.color = connected ? connectedColor : defaultColor;
            // lineRenderer.material.color = selectedColor;
        // }
        if(connected){
            activated = true;
        }else{
            activated = false;
        }
        lineRenderer.material.color = selectedColor;
    }

    public void Initialize()
    {
        // Debug.Log("Inititalizeng?");
        // if (isStart){
        //     rend.material.color = Color.cyan; 
        //     currentColor = Color.cyan;
        // }
        // else if (isEnd){
        //     rend.material.color = Color.cyan;
        //     currentColor = Color.cyan;
        if (isNode || isStart || isEnd)
        {
            rend.material.color = nodeColor;
            currentColor = nodeColor;
        }
        // else if(isNode){
        //     rend.material.color = Color.cyan;
        //     currentColor = Color.cyan;
        // }
        // else {
        else
        {
            rend.material.color = defaultColor;
            currentColor = defaultColor;
        }
    }
    public void SetNodeColor(Color color){
        nodeColor = color;
    }

    public void DrawWireTo(Tile otherTile)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position + Vector3.up * 0.1f);
        lineRenderer.SetPosition(1, otherTile.transform.position + Vector3.up * 0.1f);
        if(otherTile.isNode){
            otherTile.isConnected = true;
        }
    }

    public void ClearWire()
    {
        lineRenderer.positionCount = 0;
    }

    // **Clear the whole path associated with this tile**
    public void ClearPath()
    {
        foreach (Tile tile in connectedPath)
        {
            tile.SetConnected(false, defaultColor);
            tile.ClearWire();
        }
        connectedPath.Clear();
    }
}