using System.Collections.Generic;
using UnityEngine;

public class PersistantGameManager : MonoBehaviour
{
    public static PersistantGameManager Instance;

    public Dictionary<string, Sprite> masterInventory = new Dictionary<string, Sprite>(); // Player Inventory (Persists Between Scenes)
    public List<string> placedObjects = new();
    public int masterCurrentItem;
    public string LevelName = ""; // Name of the Loaded Scene
    public int LevelEntryPoint = 1; // Specifc Level Entry Point
    public string SpawnPointName = ""; 

    public List<Quest> Quests = new()
    {
        { new Quest("Inspect Mirror", 0, 1) },
        { new Quest("Prepare Breakfast for the Kids", 0, 3) },
        { new Quest("Place Breakfast on the Table", 0, 1) },
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Pick up Library Key", 0, 1) },
        { new Quest("Unlock Library", 0, 1)},
        { new Quest("Organize Mr. Magnates Bookshelves", 0, 6) },
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Pick up Living Room Key", 0, 1) },
        { new Quest("Unlock Living Room", 0, 1)},
        { new Quest("Inspect Living Room", 0, 1)},
        { new Quest("Pick up Master Bedroom Key", 0, 1) },
        { new Quest("Unlock Master Bedroom", 0, 1)},
        { new Quest("Fix Mrs. Magnates mirror", 0, 6)},
        { new Quest("Collect Memory Fragment", 0, 1) },
        { new Quest("Pick up Basement Key", 0, 1) },
        { new Quest("Unlock Basement", 0, 1)},
        { new Quest("Collect Final Memory Fragment ", 0, 1) },
        { new Quest("Go to Charging Room and Power Down", 0, 1) }
    };
    public int currentQuest = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function for other code to be able to update the current scene (if a scene changes)
    public void SetTargetLevel(string level, int entryPoint, string spawnPointName)
    {
        LevelName = level;
        LevelEntryPoint = entryPoint;
        SpawnPointName = spawnPointName;
    }

    public void addPlacedObject(string obj)
    {
        placedObjects.Add(obj);
    }

}
