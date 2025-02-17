using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isStart = false;
    public bool isEnd = false;
    public bool isNode = false;
    public bool isConnected = false;
    public Color defaultColor = Color.white;
    public Color connectedColor = Color.green;
    private Renderer rend;
    private LineRenderer lineRenderer;
    public Color currentColor;
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

    public void SetConnected(bool connected, Color selectedColor)
    {
        isConnected = connected;
        // if (!isStart && !isEnd)
        // {
            // rend.material.color = connected ? connectedColor : defaultColor;
            lineRenderer.material.color = selectedColor;
        // }
    }
    public void Initialize()
    {
        Debug.Log("Inititalizeng?");
        if (isStart){
            rend.material.color = Color.cyan; 
            currentColor = Color.cyan;
        }
        else if (isEnd){
            rend.material.color = Color.cyan;
            currentColor = Color.cyan;
        }
        else if(isNode){
            rend.material.color = Color.cyan;
            currentColor = Color.cyan;
        }
        else {
            rend.material.color = defaultColor;
            currentColor = defaultColor;
        }
    }

    public void DrawWireTo(Tile otherTile)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position + Vector3.up * 0.1f);
        lineRenderer.SetPosition(1, otherTile.transform.position + Vector3.up * 0.1f);
    }

    public void ClearWire()
    {
        lineRenderer.positionCount = 0;
    }
}
