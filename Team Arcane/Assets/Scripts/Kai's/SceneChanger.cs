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
        Debug.Log("SETTING PLAYER IN SCENE: " + this.SceneName);
        // Uncomment to spawn user in specific place
        if (PersistantGameManager.LevelEntryPoint == -1) return;
        // Initial Game Spawn and all new Scene automatically have a -1 entry point
        Debug.Log("SETTING PLAYER TO PROPER SPAWN POSITION");
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
        float distanceForward = 1.0f;
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
           // SpawnPoint.transform.position = new Vector3(-7.57f, 6.129f, 11.09f);
           // Player.transform.position = new Vector3(-7.57f, 6.129f, 11.09f);
           Vector3 spawnOffset = SpawnPoint.transform.up * distanceForward;
           // spawnOffset += (Vector3.forward * 10);
           Vector3 targetPosition = SpawnPoint.transform.position + spawnOffset;
           Player.transform.position = targetPosition;
           Player.transform.rotation = Quaternion.LookRotation(SpawnPoint.transform.up, Vector3.up);
           // Player.transform.position = SpawnPoint.transform.position + new Vector3(0.7f, 3f, 0);
           // Player.transform.transform.Rotate(0, 200, 0);
           Debug.Log("Moving Player to " + Player.transform.position);
       }
    }
}
