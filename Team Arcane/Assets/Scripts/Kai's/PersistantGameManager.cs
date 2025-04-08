using System.Collections.Generic;
using UnityEngine;

public class PersistantGameManager : MonoBehaviour
{
    public static Dictionary<string, Sprite> masterInventory = new Dictionary<string, Sprite>(); // Player Inventory (Persists Between Scenes)
    public static List<string> placedObjects = new();
    public static int masterCurrentItem;
    public static string LevelName = ""; // Name of the Loaded Scene
    public static int LevelEntryPoint = 1; // Specifc Level Entry Point

    public static List<Quest> Quests = new()
    {
        { new Quest("Place All The Books", 0, 3) },
        { new Quest("Enter The Library", 0, 1) }
    };
    public static int currentQuest = 0;

    // Function for other code to be able to update the current scene (if a scene changes)
    public static void SetTargetLevel(string level, int entryPoint)
    {
        LevelName = level;
        LevelEntryPoint = entryPoint;
    }

    public static void addPlacedObject(string obj)
    {
        placedObjects.Add(obj);
    }

}
