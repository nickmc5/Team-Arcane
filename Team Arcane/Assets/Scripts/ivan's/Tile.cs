using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isStart = false;
    public bool isEnd = false;
    public bool isConnected = false;
    public Color defaultColor = Color.white;
    public Color connectedColor = Color.green;
    private Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = defaultColor;
    }
    public void SetConnected(bool connected){
        isConnected = connected;
        rend.material.color = connected ? connectedColor : defaultColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
