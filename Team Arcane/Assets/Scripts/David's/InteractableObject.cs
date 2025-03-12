using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEditor.Progress;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem = "";
    public string buttonPrompt = "[E] to interact";
    public string objectDescription;
    
    private void Start()
    {
        objectDescription = ObjectDescriptions.GetDescription(gameObject.name);
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
            if(objectDescription != null){
                Debug.Log(objectDescription);
            }
        }
    }
}
