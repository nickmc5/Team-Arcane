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

    // If not in first scene, load with player with previous inventory
    void Start()
    {
        
        playerInv.Clear(); // Clear current local inventory
        
        // If Not coming from an initial level or inventory clearing level
        if (PersistantGameManager.LevelEntryPoint != -1)
        {
            // Loop through current global inventroy and add to local inventory
            foreach (KeyValuePair<string, Sprite> pair in PersistantGameManager.masterInventory)
            {
                    AddItem(pair.Key, pair.Value);
            }
        }
    }

    public void AddItem(string n, Sprite i)
    {
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
        
        // If persistant game manager doesn't already contain key, then add it
        if (!PersistantGameManager.masterInventory.ContainsKey(n))
        {
            PersistantGameManager.masterInventory.Add(n, i);
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
