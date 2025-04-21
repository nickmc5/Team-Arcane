using UnityEngine;

public class SpawnPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Despawns Object if already picked up in player inventory
        if (PersistantGameManager.Instance.masterInventory.ContainsKey(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
