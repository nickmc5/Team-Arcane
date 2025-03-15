using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEditor.Progress;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem = "";
    public string buttonPrompt = "[E] to interact";
    // FOR DESCRIPTIONS
    public string objectDescription;
    private MenuController menuController;

    
    private void Start()
    {
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
            // FOR DESCRIPTIONS
            if (!string.IsNullOrEmpty(objectDescription)) // Check if description is not empty or null
            {
                Debug.Log(objectDescription);
                menuController.ShowDescription(objectDescription); // Show description if available
            }
        }
    }
}
