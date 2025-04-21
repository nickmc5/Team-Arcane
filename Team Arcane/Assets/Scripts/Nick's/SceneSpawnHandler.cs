using UnityEngine;
using System.Collections;
using System.Linq;

public class SceneSpawnHandler : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitUntilReadyAndSpawn());
    }

    IEnumerator WaitUntilReadyAndSpawn()
    {
        string spawnName = PersistantGameManager.Instance.SpawnPointName;

        if (string.IsNullOrEmpty(spawnName))
        {
            Debug.Log("[SpawnHandler] No SpawnPointName set. Skipping spawn.");
            yield break;
        }

        Debug.Log("[SpawnHandler] Waiting for Player and SpawnPoint...");

        GameObject player = null;
        GameObject spawnPoint = null;
        float timeout = 5f; // max wait time in seconds
        float elapsed = 0f;

        while ((player == null || spawnPoint == null) && elapsed < timeout)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint")
                                   .FirstOrDefault(sp => sp.name == spawnName);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (player != null && spawnPoint != null)
        {
            Vector3 offset = spawnPoint.transform.forward; // small offset
            player.transform.position = spawnPoint.transform.position + offset;
            player.transform.rotation = Quaternion.LookRotation(spawnPoint.transform.forward);
            Debug.Log($"[SpawnHandler] Successfully moved player to {spawnName}");

            PersistantGameManager.Instance.SpawnPointName = ""; // prevent respawning
        }
        else
        {
            Debug.LogError($"[SpawnHandler] FAILED TO FIND player or spawnPoint after {timeout} seconds.");
        }
    }
}
