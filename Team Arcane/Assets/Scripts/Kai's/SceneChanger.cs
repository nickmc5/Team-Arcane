using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles making any object being able to transport the player if touched. This Class both loads the scene and places the player in the correct orientation


public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string SceneName;
    public int LevelEntryPoint = 1;
    public string SpawnPointName;
    public static bool useSpawnPosition = false;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (useSpawnPosition)
        {
            SetPlayerPosAndRot();
            useSpawnPosition = false;
        }
    }

    void Start()
    {
        Debug.Log("SETTING PLAYER");
        // Uncomment to spawn user in specific place
        if (PersistantGameManager.LevelEntryPoint == -1) return;
        // Initial Game Spawn and all new Scene automatically have a -1 entry point
        Debug.Log("SETTING PLAYER");
        SetPlayerPosAndRot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + " : entered");
            PersistantGameManager.SetTargetLevel(this.SceneName, this.LevelEntryPoint, this.SpawnPointName);
            SceneManager.LoadScene(this.SceneName);
        }
    }

    private void SetPlayerPosAndRot()
    {
       // Set player it designated spawn point here
       // NOTE: You can leverage persistant game manager to get check current loaded in scene
       string SpawnName = PersistantGameManager.SpawnPointName;
       Debug.Log("SpawnName: " + SpawnName);
       GameObject SpawnPoint = GameObject.Find(SpawnName);
       Debug.Log("SpawnPoint: " + SpawnPoint);
       if (SpawnPoint != null)
       {
           GameObject Player = GameObject.Find("Player");
           Debug.Log("Original Position: " + Player.transform.position);
           SpawnPoint.transform.position = new Vector3(-7.57f, 6.129f, 11.09f);
           Player.transform.position = SpawnPoint.transform.position;
           Debug.Log("Moving Player to " + Player.transform.position);
       }
    }
}
