using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static List<string> playerInv = new List<string>();
    public void AddItem(string name)
    {
        playerInv.Add(name);
    }

    public void RemoveItem(string name)
    {
        playerInv.Remove(name);
    }
}
