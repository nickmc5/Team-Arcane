using System.Collections.Generic;
using UnityEngine;

public class PersistantGameManager : MonoBehaviour
{
    public static Dictionary<string, Sprite> masterInventory = new Dictionary<string, Sprite>(); // Player Inventory (Persists Between Scenes)
    public static List<string> placedObjects = new();
    public static int masterCurrentItem;
    public static string LevelName = ""; // Name of the Loaded Scene
    public static int LevelEntryPoint = 1; // Specifc Level Entry Point
    public static string SpawnPointName = ""; 

    public static List<Quest> Quests = new()
    {
        { new Quest("Prepare Breakfast for the kids", 0, 3) },
        { new Quest("Place breakfast on the table", 0, 1) },
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Go to library", 0, 1)},
        { new Quest("Organize Mr. Magnates bookshelves", 0, 6) },
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Go to Living Room", 0, 1)},
        { new Quest("Inspect Living Room", 0, 1)},
        { new Quest("Go to Master Bedroom", 0, 1)},
        { new Quest("Fix Mrs. Magnates mirror", 0, 6)},
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Go to Basement", 0, 1)},
    };
    public static int currentQuest = 0;

    // Function for other code to be able to update the current scene (if a scene changes)
    public static void SetTargetLevel(string level, int entryPoint, string spawnPointName)
    {
        LevelName = level;
        LevelEntryPoint = entryPoint;
        SpawnPointName = spawnPointName;
    }

    public static void addPlacedObject(string obj)
    {
        placedObjects.Add(obj);
    }

}
