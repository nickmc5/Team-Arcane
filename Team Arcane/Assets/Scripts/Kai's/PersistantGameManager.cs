using System.Collections.Generic;
using UnityEngine;

public class PersistantGameManager : MonoBehaviour
{
    public static Dictionary<string, Sprite> masterInventory = new Dictionary<string, Sprite>(); // Player Inventory (Persists Between Scenes)
    public static string LevelName = ""; // Name of the Loaded Scene
    public static int LevelEntryPoint = 1; // Specifc Level Entry Point
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function for other code to be able to update the current scene (if a scene changes)
    public static void SetTargetLevel(string level, int entryPoint)
    {
        LevelName = level;
        LevelEntryPoint = entryPoint;
    }
}
