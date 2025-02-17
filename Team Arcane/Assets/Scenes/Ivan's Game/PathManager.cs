using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private LineRenderer currentLine;
    private List<Vector3> pathPoints = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add line renderer component to the game object
        currentLine = gameObject.AddComponent<LineRenderer>();
        currentLine.positionCount = 0; //start with no points
        currentLine.material = new Material(Shader.Find("Sprites/Default"));
        currentLine.startColor = Color.white;
        currentLine.endColor = Color.white;
        currentLine.startWidth = 0.5f;
        currentLine.endWidth = 0.5f;
        
    }
    public void StartDrawing(Vector3 startPoint) {
        pathPoints.Clear();
        pathPoints.Add(startPoint);
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, startPoint);
    }
    //call this method to keep drawing the path
    public void UpdateDrawing(Vector3 newPoint){
        if(pathPoints.Count > 1){
            pathPoints.RemoveAt(pathPoints.Count - 1);
        }
        pathPoints.Add(newPoint);
        currentLine.positionCount = pathPoints.Count;
        currentLine.SetPositions(pathPoints.ToArray());
    }
    //call lthis to finish the drwaing
    public void FinishDrawing(){
        currentLine.positionCount = 0;
    }
    public List<Vector3> GetPathPoints()
    {
        return new List<Vector3>(pathPoints);  // Return a copy to prevent modification
    }

    // Setter method (optional, if you need to modify the list from outside)
    public void SetPathPoints(List<Vector3> points)
    {
        pathPoints = points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
