using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem = "";
    public string buttonPrompt = "[E] to interact";
    // FOR DESCRIPTIONS
    public string objectDescription;
    [Tooltip("Leave empty if this script is not attached to a place to put an object")]
    public GameObject placeableObject;
    private MenuController menuController;
    public bool isDoorLock;

    // below is only needed if the name of the object changes and needs to stay active (for example the bookshelf where a book is placed)
    public string nameAfterPlacement;


    private void Start()
    {
        // places object if placed already and switched scenes
        if (PersistantGameManager.placedObjects.Contains(requiredItem) && placeableObject != null)
        {
            placeableObject.SetActive(true);
            if (nameAfterPlacement != null)
            {
                gameObject.name = nameAfterPlacement;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (PersistantGameManager.placedObjects.Contains(requiredItem) && isDoorLock)
        {
            gameObject.SetActive(false);
        }

        // FOR DESCRIPTIONS
        objectDescription = ObjectDescriptions.GetDescription(gameObject.name);
        menuController = FindFirstObjectByType<MenuController>();
    }

    [Serializable]
    public class interactEvent : UnityEvent {}

    [FormerlySerializedAs("onInteract")]
    [SerializeField]
    private interactEvent onInteract = new interactEvent();

    public void PlayerInteract()
    {
        if ((PersistantGameManager.masterInventory.ContainsKey(requiredItem) && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().getCurrentItem() == requiredItem) || requiredItem == "")
        {
            onInteract.Invoke();
            if (requiredItem != "")
            {
                // adds object to placed object list so it wont spawn in incorrectly when going between scenes
                PersistantGameManager.addPlacedObject(requiredItem);
            }
            objectDescription = ObjectDescriptions.GetDescription(gameObject.name);
            
        }
        // FOR DESCRIPTIONS
        // IVAN CHANGE: I moved this outside of that if statement so that it will show the description even if the object is not in players inventory
        // This way, the description will show up even if the object is not interactable... hope that is ok!
        if (!string.IsNullOrEmpty(objectDescription)) // Check if description is not empty or null
        {
            Debug.Log(objectDescription);
            menuController.ShowDescription(objectDescription); // Show description if available
        }
        for (int i = 0; i < PersistantGameManager.placedObjects.Count; i++)
        {
            Debug.Log($"Placed object {i}: " + PersistantGameManager.placedObjects[i]);
        }
    }
}
