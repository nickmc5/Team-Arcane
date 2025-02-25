using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static Dictionary<string,Image> playerInv = new Dictionary<string, Image>();
    public void AddItem(string n, Image i)
    {
        playerInv.Add(n, i);
    }

    public void RemoveItem(string name)
    {
        playerInv.Remove(name);
    }
}
