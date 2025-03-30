using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInventory : MonoBehaviour
{
    public static Dictionary<string,Sprite> playerInv = new Dictionary<string, Sprite>();

    private List<string> indexedInv = new List<string>();

    public GameObject inventoryUIItems;
    public GameObject HUDItem;

    private int currentItem = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && playerInv.Count != 0)
        {
            currentItem = (currentItem + 1) % playerInv.Count;
            HUDItem.GetComponent<Image>().sprite = playerInv[indexedInv[currentItem]];
            HUDItem.GetComponentInChildren<TextMeshProUGUI>().text = indexedInv[currentItem];

            int j = 0;
            foreach (var item in playerInv)
            {
                if (item.Key == indexedInv[currentItem])
                {
                    inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<u>" + item.Key + "</u>";
                }
                else
                {
                    inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
                }
                j++;
            }
        }
    }

    void Start()
    {
        // If not in first scene, load with player with previous inventory
        Debug.Log("Inventory Loading into new scene");
        foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
        {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
        }
        playerInv.Clear();
        foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
        {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
        }
        Debug.Log("Level Entry Point: " + PersistantGameManager.LevelEntryPoint);
        if (PersistantGameManager.LevelEntryPoint != -1)
        {
            foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
            {
                // if (!PersistantGameManager.masterInventory.ContainsKey(pair.Key))
                // {
                    AddItem(pair.Key, pair.Value);
                    Debug.Log(pair.Key + ": was added");
                // }
            }
        }
    }

    public void AddItem(string n, Sprite i)
    {
        Debug.Log("HERE");
        if (playerInv.Count == 0)
        {
            UpdateHUDIcon(n, i);
        }

        playerInv.Add(n, i);
        indexedInv.Add(n);
        int j = 0;
        foreach (var item in playerInv)
        {
            if (item.Key == indexedInv[currentItem])
            {
                inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "<u>" + item.Key + "</u>";
            }
            else
            {
                inventoryUIItems.transform.GetChild(j).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Key;
            }
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().sprite = item.Value;
            inventoryUIItems.transform.GetChild(j).GetComponent<Image>().color = Color.white;
            j++;
        }

        // foreach (KeyValuePair<string, Sprite> pair in playerInv)
        // {
        //     PersistantGameManager.masterInventory[pair.Key] = pair.Value;
        // }
        
        // If persistant game manager already contains key
        if (!PersistantGameManager.masterInventory.ContainsKey(n))
        {
            PersistantGameManager.masterInventory.Add(n, i);
        }
        
        // PersistantGameManager.masterInventory = playerInv;
        Debug.Log("MASTER Inventory UPDATED");
        foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
        {
            Debug.Log(pair.Key);
            Debug.Log(pair.Value);
        }
    }

    public void RemoveItem(string name)
    {
        if (indexedInv[currentItem] == name)
        {
            currentItem = (currentItem + 1) % playerInv.Count;
            UpdateHUDIcon(indexedInv[currentItem], playerInv[indexedInv[currentItem]]);
        }

        playerInv.Remove(name);
        indexedInv.Remove(name);

        if (currentItem > playerInv.Count)
        {
            currentItem = 0;
            UpdateHUDIcon(indexedInv[currentItem], playerInv[indexedInv[currentItem]]);
        }
        else if (playerInv.Count == 0)
        {
            currentItem = 0;
            UpdateHUDIcon("No Items", Resources.Load<Sprite>("DiscIcon"));
        }


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

    private void UpdateHUDIcon(string text, Sprite icon)
    {
        HUDItem.GetComponent<Image>().sprite = icon;
        HUDItem.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public string getCurrentItem()
    {
        return indexedInv[currentItem];
    }
}
