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

    
    private void Start()
    {
        // places object if placed already and switched scenes
        if (PersistantGameManager.placedObjects.Contains(requiredItem) && placeableObject != null)
        {
            placeableObject.SetActive(true);
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
            // FOR DESCRIPTIONS
            if (!string.IsNullOrEmpty(objectDescription)) // Check if description is not empty or null
            {
                Debug.Log(objectDescription);
                menuController.ShowDescription(objectDescription); // Show description if available
            }
        }
    }
}
