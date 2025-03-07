using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string,Sprite> playerInv = new Dictionary<string, Sprite>();
    public GameObject inventoryUIItems;

    void Start()
    {
        // If not in first scene, load with player with previous inventory
        if (PersistantGameManager.LevelEntryPoint != -1)
        {
            foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
            {
                AddItem(pair.Key, pair.Value);
            }
        }
    }

    public void AddItem(string n, Sprite i)
    {
        playerInv.Add(n, i);
        int j = 0;
        foreach (var item in playerInv)
        {
            inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().sprite = item.Value;
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().color = Color.white;
            j++;
        }
        PersistantGameManager.masterInventory = playerInv;
    }

    public void RemoveItem(string name)
    {
        playerInv.Remove(name);
        for (int i = 0; i < inventoryUIItems.transform.childCount; i++)
        {
            inventoryUIItems.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            inventoryUIItems.transform.GetChild(i).GetComponent<Image>().sprite = null;
            inventoryUIItems.transform.GetChild(i).GetComponent<Image>().color = Color.clear;
        }
        int j = 0;
        foreach (var item in playerInv)
        {
            inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().sprite = item.Value;
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().color = Color.white;
            j++;
        }
        PersistantGameManager.masterInventory = playerInv;
    }
    
}
