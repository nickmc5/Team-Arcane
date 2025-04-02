using UnityEngine;
using System.Collections.Generic;

public class LevelDetailsScript : MonoBehaviour
{
     public List<string> requiredItems;  // List of required items to activate something
    public GameObject objectToActivate; // The object to activate once all items are collected
    public GameObject lightToActivate; // The light to activate once all items are collected
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          if (CheckAllItemsCollected())
        {
            ActivateObject();
        }
        
    }
      private void Update()
    {
        // Continuously check if all required items are placed
        if (CheckAllItemsCollected())
        {
            Debug.Log("All required items found! Activating object: ");
            ActivateObject();
        }
    }   
    private bool CheckAllItemsCollected()
    {
        // Show all the items in the placedObjects list
        Debug.Log("Current Items in placedObjects: " + string.Join(", ", PersistantGameManager.placedObjects));
        foreach (string item in requiredItems)
        {
            if (!PersistantGameManager.placedObjects.Contains(item))
            {
                Debug.Log("Missing item: " + item);
                return false; // If any required item is missing, return false
            }
        }
        return true; // All required items are placed
    }
     private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            lightToActivate.SetActive(true);
            Debug.Log("All required items found! Activating object: " + objectToActivate.name);
            
            // Optional: Disable this script to stop checking after activation
            enabled = false;
        }
    }
}
