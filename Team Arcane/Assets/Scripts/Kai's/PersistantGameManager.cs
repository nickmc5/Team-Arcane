using System.Collections.Generic;
using UnityEngine;

public class PersistantGameManager : MonoBehaviour
{
    public static Dictionary<string, Sprite> masterInventory;
    public static string LevelName = "";
    public static int LevelEntryPoint = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetTargetLevel(string level, int entryPoint)
    {
        LevelName = level;
        LevelEntryPoint = entryPoint;
    }
}
