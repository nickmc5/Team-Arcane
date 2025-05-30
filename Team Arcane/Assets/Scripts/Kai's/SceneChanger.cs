using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Handles making any object being able to transport the player if touched. This Class both loads the scene and places the player in the correct orientation


public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string SceneName;
    public int LevelEntryPoint = 1;
    public string SpawnPointName;
    public static bool useSpawnPosition = false;
    public string DisplayText;
    public Image fadeImage;
    public Animator anim;
    public static bool isFading = false;

    //IEnumerator WaitAndSetPlayerPos()
    //{
    //    yield return new WaitForSeconds(0.1f); // Give objects time to spawn

    //    // Wait until SpawnPoints are found
    //    GameObject[] spawnPoints;
    //    GameObject[] player;
    //    do
    //    {
    //        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    //        player = GameObject.FindGameObjectsWithTag("Player");
    //        yield return null;
    //    } while (spawnPoints.Length == 0 && player.Length == 0);

    //    SetPlayerPosAndRot();
    //}

    void Start()
    {
        //Debug.Log("SETTING PLAYER");
        isFading = false;
        // Uncomment to spawn user in specific place
        if (PersistantGameManager.Instance.LevelEntryPoint == -1) return;
        // Initial Game Spawn and all new Scene automatically have a -1 entry point
        //Debug.Log("SETTING PLAYER");
        SetPlayerPosAndRot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player") && !isFading)
        {
            Debug.Log(other.gameObject.name + " : entered");
            PersistantGameManager.Instance.SetTargetLevel(this.SceneName, this.LevelEntryPoint, this.SpawnPointName);
            StartCoroutine(Fading());
        }
    }

    private void SetPlayerPosAndRot()
    {
       // Set player it designated spawn point here
       // NOTE: You can leverage persistant game manager to get check current loaded in scene

       string SpawnName = PersistantGameManager.Instance.SpawnPointName;
       //Debug.Log("SpawnName: " + SpawnName);
       //GameObject SpawnPoint = GameObject.Find(SpawnName);

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject SpawnPoint = spawnPoints.FirstOrDefault(sp => sp.name == SpawnName);

        if (string.IsNullOrEmpty(SpawnName))
        {
            //Debug.Log("SpawnName is null or empty.");
            return;
        }
        //Debug.Log("SpawnPoint: " + SpawnPoint);
        if (SpawnPoint != null)
       {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            //Debug.Log("Original Position: " + Player.transform.position);
           // SpawnPoint.transform.position = new Vector3(-7.57f, 6.129f, 11.09f);
            Vector3 spawnOffset = SpawnPoint.transform.forward; // Adjust 1.0f to taste
            Vector3 targetPosition = SpawnPoint.transform.position + spawnOffset;

            // Move the player to the adjusted position
            Player.transform.position = targetPosition;
           Player.transform.rotation = Quaternion.LookRotation(SpawnPoint.transform.forward, Vector3.up);
           //Debug.Log("Moving Player to " + Player.transform.position);
       }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        isFading = true;
        useSpawnPosition = true;
        yield return new WaitUntil(() => fadeImage.color.a >= 0.99f); // safer
        SceneManager.LoadScene(this.SceneName);
    }
}
