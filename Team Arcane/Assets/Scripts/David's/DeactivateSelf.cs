using UnityEngine;

public class DeactivateSelf : MonoBehaviour
{
    private GameObject player;
    public string objectName;
    public Sprite objectSprite;

    private void Start()
    {
        if (PersistantGameManager.Instance.masterInventory.ContainsKey(objectName) || PersistantGameManager.Instance.placedObjects.Contains(objectName))
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
