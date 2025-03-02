using UnityEngine;
using System.Collections;


public class MenuController : MonoBehaviour
{
    private Animator inventoryAnimator;
    public GameObject player;

    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject inventory;
    public static int currentMenu = 0; // 0 = HUD, 1 = Pause, 2 = Inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAnimator = inventory.GetComponent<Animator>();
        inventory.SetActive(true);
        switch (currentMenu)
        {
            case 0:
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                //inventory.SetActive(false);
                break;
            case 1:
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                //inventory.SetActive(false);
                break;
            case 2:
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                //inventory.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Menu switching system
        if (Input.GetKeyDown(KeyCode.Escape) && currentMenu == 0)
        {
            HUDToPause();
        }
        else if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) && currentMenu == 1)
        {
            PauseToHUD();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentMenu == 0)
        {
            HUDToInventory();

        }
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) && currentMenu == 2)
        {
            StartCoroutine("InventoryToHUD");
        }
    }

    public void HUDToPause()
    {
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        currentMenu = 1;
    }

    public void PauseToHUD()
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        currentMenu = 0;
    }

    public void HUDToInventory()
    {
        //inventory.SetActive(true);
        HUD.SetActive(false);
        currentMenu = 2;
        inventoryAnimator.SetInteger("currentMenu", MenuController.currentMenu);
        
    }

    IEnumerator InventoryToHUD()
    {
        //inventory.SetActive(false);
        currentMenu = 0;
        inventoryAnimator.SetInteger("currentMenu", MenuController.currentMenu);
        yield return new WaitForSeconds(1/4f); ;
        HUD.SetActive(true);
    }
}
