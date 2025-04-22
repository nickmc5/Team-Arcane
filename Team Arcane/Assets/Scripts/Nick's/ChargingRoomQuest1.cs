using UnityEngine;

public class ChargingRoomQuest1 : MonoBehaviour
{
    public BoxCollider doorLock;
    public GameObject otherMirrorDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistantGameManager.Instance.currentQuest != 0)
        {
            doorLock.isTrigger = true;
            otherMirrorDialogue.SetActive(false);
        }
    }
}
