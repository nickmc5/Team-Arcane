using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEditor.Progress;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem = "";
    public string buttonPrompt = "[E] to interact";

    [Serializable]
    public class interactEvent : UnityEvent {}

    [FormerlySerializedAs("onInteract")]
    [SerializeField]
    private interactEvent onInteract = new interactEvent();

    public void PlayerInteract()
    {
        if (PlayerInventory.playerInv.Contains(requiredItem) || requiredItem == "")
        {
            onInteract.Invoke();
        }
    }
}
