using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    private GameObject player;
    public string objectName;
    public Sprite objectSprite;

    private void Start()
    {
        if (PersistantGameManager.masterInventory.ContainsKey(objectName) || PersistantGameManager.placedObjects.Contains(objectName))
        {
            gameObject.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Deactivate()
    {
        player.GetComponent<PlayerInventory>().AddItem(objectName, objectSprite);
        gameObject.SetActive(false);
    }
}
