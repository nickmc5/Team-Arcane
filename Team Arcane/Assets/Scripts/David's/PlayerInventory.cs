using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInventory : MonoBehaviour
{
    // current scene player inventory
    public static Dictionary<string,Sprite> playerInv = new Dictionary<string, Sprite>();
    // plain array version of inventory of item strings for things like indexing for current item
    private List<string> indexedInv = new List<string>();

    // game objects of the inventory menu and hud icon
    public GameObject inventoryUIItems;
    public GameObject HUDItem;

    // current scene variable for which item is currently selected
    private int currentItem = 0;

    private void Update()
    {
        if (playerInv.Count != 0)
        {
            // scrolls down and wraps around (scrolling doesn't work well with trackpads)
            float scroll = Input.mouseScrollDelta.y;
            if (scroll < 0)
            {
                if (currentItem == 0)
                {
                    currentItem = playerInv.Count - 1;
                }
                else
                {
                    currentItem = currentItem - 1;
                }
                UpdateInventoryMenuSelectedItem();
            }
            // scrolls down and wraps around (tabbing does same as scrolling down)
            else if (scroll > 0 || Input.GetKeyDown(KeyCode.Tab))
            {
                currentItem = (currentItem + 1) % playerInv.Count;
                UpdateInventoryMenuSelectedItem();
            }
            // else ifs below checks for num keys and set current item to that number (can be expanded later when more items)
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentItem = 0;
                UpdateInventoryMenuSelectedItem();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && playerInv.Count > 1)
            {
                currentItem = 1;
                UpdateInventoryMenuSelectedItem();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && playerInv.Count > 2)
            {
                currentItem = 2;
                UpdateInventoryMenuSelectedItem();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && playerInv.Count > 3)
            {
                currentItem = 3;
                UpdateInventoryMenuSelectedItem();
            }
        }
    }

    // Updates the arrays for the menu with all the items the player has when coming from other scenes
    void Start()
    {
        // If not in first scene, load with player with previous inventory
        //if (PersistantGameManager.LevelEntryPoint != -1) // commented out because doesn't seem full fleshed out but may be needed later?
        {
            playerInv = PersistantGameManager.masterInventory;
            currentItem = PersistantGameManager.masterCurrentItem;

            if (playerInv.Count != 0)
            {
                foreach (var pair in playerInv)
                {
                    indexedInv.Add(pair.Key);
                }
                UpdateInventoryMenuSelectedItem();
            }
        }
    }

    public void AddItem(string n, Sprite i)
    {
        // check if current item was -1 (if inventory was empty before) and set it to 0 now
        currentItem = currentItem < 0 ? 0 : currentItem;
        // add items to inventory and update UI
        playerInv.Add(n, i);
        indexedInv.Add(n);
        UpdateInventoryMenuSelectedItem();
        // updates master inventory for going between scenes
        PersistantGameManager.masterInventory = playerInv;
    }

    public void RemoveItem(string name)
    {
        // clear the current inventory menu
        for (int i = 0; i < inventoryUIItems.transform.childCount; i++)
        {
            inventoryUIItems.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            inventoryUIItems.transform.GetChild(i).GetComponent<Image>().sprite = null;
            inventoryUIItems.transform.GetChild(i).GetComponent<Image>().color = Color.clear;
        }

        // remove the item
        playerInv.Remove(name);
        indexedInv.Remove(name);

        // if the current item is now out of range, set it to 0
        if (currentItem >= playerInv.Count)
        {
            currentItem = playerInv.Count - 1;

        }
        // if the inventory is now empty, set current item to out of range and set HUD icon to empty
        else if (playerInv.Count == 0)
        {
            currentItem = -1;
            UpdateHUDIcon("No Items", Resources.Load<Sprite>("DiscIcon"));
        }
        UpdateInventoryMenuSelectedItem();
        PersistantGameManager.masterInventory = playerInv;
    }

    // Updates the icon and text on the HUD that reflects the current selected item
    private void UpdateHUDIcon(string text, Sprite icon)
    {
        HUDItem.GetComponent<Image>().sprite = icon;
        HUDItem.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    // Updates which item is currently underlined within the inventory menu and the HUD icon
    private void UpdateInventoryMenuSelectedItem()
    {
        if (currentItem >= 0)
        {
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
            UpdateHUDIcon(indexedInv[currentItem], playerInv[indexedInv[currentItem]]);
        }
    }

    // can get current item string (for other scripts)
    public string getCurrentItem()
    {
        return indexedInv[currentItem];
    }
}
