using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles making any object being able to transport the player if touched. This Class both loads the scene and places the player in the correct orientation
public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string SceneName;
    public int LevelEntryPoint;
    void Start()
    {
        // Only perform below if new spawning place character in weird place
        // if (PersistantGameManager.LevelEntryPoint != -1) return;
        // SetPlayerPosAndRot();
        Debug.Log(SceneName + " is here");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + ": entered");
        PersistantGameManager.SetTargetLevel(this.SceneName, this.LevelEntryPoint);
        SceneManager.LoadScene(this.SceneName);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name + ": entered");
        PersistantGameManager.SetTargetLevel(this.SceneName, this.LevelEntryPoint);
        SceneManager.LoadScene(this.SceneName);
    }

    private void SetPlayerPosAndRot()
    {
        // Transform spawnPosition = EntryPoints[PersistantGameManager.LevelEntryPoint].transform;
        // Vector3 newPosition = spawnPosition.position + (spawnPosition.forward * distance);
        // player.transform.position = newPosition;
        // player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + 180, 0);
    }
}
