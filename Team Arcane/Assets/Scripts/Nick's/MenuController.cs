using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;


public class MenuController : MonoBehaviour
{
    private Animator inventoryAnimator;

    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject inventory;
    public GameObject puzzle;
    public static int currentMenu = 0; // 0 = HUD, 1 = Pause, 2 = Inventory, 3 = Puzzle
    public AudioSource uiSound;
    // NEW UI ELEMENTS FOR DESCRIPTIONS
    public GameObject descriptionPanel;
    public TextMeshProUGUI descriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAnimator = inventory.GetComponent<Animator>();
        inventory.SetActive(true);
        // DESCRIPTION
        descriptionPanel.SetActive(false);
        switch (currentMenu)
        {
            case 0:
                HUD.SetActive(true);
                pauseMenu.SetActive(false);
                //inventory.SetActive(false);
                puzzle.SetActive(false);
                break;
            case 1:
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                //inventory.SetActive(false);
                puzzle.SetActive(false);
                break;
            case 2:
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                //inventory.SetActive(true);
                puzzle.SetActive(false);
                break;
            case 3:
                HUD.SetActive(false);
                pauseMenu.SetActive(false);
                puzzle.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Menu switching system
        if (Input.GetKeyDown(KeyCode.Escape) && currentMenu == 0)
        {
            uiSound.Play();
            HUDToPause();
        }
        else if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) && currentMenu == 1)
        {
            uiSound.Play();
            PauseToHUD();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentMenu == 0)
        {
            uiSound.Play();
            HUDToInventory();

        }
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) && currentMenu == 2)
        {
            uiSound.Play();
            StartCoroutine("InventoryToHUD");
        }
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) && currentMenu == 3)
        {
            uiSound.Play();
            PuzzleToHud();
        }
         // Close description panel when pressing a key (E or Space)
        else if (currentMenu == 4 && Input.GetMouseButtonDown(0))
        {
            HideDescription();
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
        // inventory.SetActive(false);
        currentMenu = 0;
        inventoryAnimator.SetInteger("currentMenu", MenuController.currentMenu);
        yield return new WaitForSeconds(1/4f); ;
        HUD.SetActive(true);
    }

    public void HUDToPuzzle()
    {
        inventory.SetActive(false);
        HUD.SetActive(false);
        puzzle.SetActive(true);
        descriptionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentMenu = 3;
    }

    public void PuzzleToHud()
    {
        puzzle.SetActive(false);
        inventory.SetActive(true);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentMenu = 0;
    }
    // NEW DESCRIPTION METHODS
    public void ShowDescription(string description)
    {
        inventory.SetActive(false);
        // HUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        descriptionText.text = description;
        descriptionPanel.SetActive(true);
        currentMenu = 4;
    }
    public void HideDescription()
    {
        inventory.SetActive(true);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        descriptionPanel.SetActive(false);
        currentMenu = 0;
    }
}
