using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject inventory;
    public int currentMenu = 0; // 0 = HUD, 1 = Pause, 2 = Inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch(currentMenu)
        {
            case 0:
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                inventory.SetActive(false);
                break;
            case 1:
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                inventory.SetActive(false);
                break;
            case 2:
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                inventory.SetActive(true);
                break;
        }
            
                
    }

    // Update is called once per frame
    void Update()
    {
        // Menu switching system
        if (Input.GetKeyDown(KeyCode.Escape) && currentMenu == 0)
        {
            pauseMenu.SetActive(true);
            HUD.SetActive(false);
            currentMenu = 1;
        }
        else if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) && currentMenu == 1)
        {
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
            currentMenu = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentMenu == 0)
        {
            inventory.SetActive(true);
            HUD.SetActive(false);
            currentMenu = 2;
        }
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) && currentMenu == 2)
        {
            inventory.SetActive(false);
            HUD.SetActive(true);
            currentMenu = 0;
        }
    }
}
