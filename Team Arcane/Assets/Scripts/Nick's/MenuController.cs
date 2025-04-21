using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;


public class MenuController : MonoBehaviour
{
    // public Gameobjects for the different canvases
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject inventory;
    public GameObject puzzle;
    public GameObject descriptionPanel;

    public TextMeshProUGUI descriptionText;
    public AudioSource uiSound;
    private Animator inventoryAnimator;

    // Keeping track of which canvas is currently being displayed
    public static int currentMenu = 0; // 0 = HUD, 1 = Pause, 2 = Inventory, 3 = Puzzle

    void Start()
    {
        currentMenu = 0;

        // Set everyting inactive except for HUD and inventory (for the animation)
        HUD.SetActive(true);
        inventory.SetActive(true);
        inventoryAnimator = inventory.GetComponent<Animator>();
        descriptionPanel.SetActive(false);
        pauseMenu.SetActive(false);
        puzzle.SetActive(false);
    }

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
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)) && currentMenu == 3 && GridBehaviorUI.connectedNodes >= GridBehaviorUI.staticNumPairs)
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseToHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        GridBehaviorUI.connectedNodes = 0;
        GridBehaviorUI.staticNumPairs = 10;
        currentMenu = 0;
    }

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
