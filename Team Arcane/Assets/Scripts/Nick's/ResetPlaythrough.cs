using UnityEngine;

public class ResetPlaythrough : MonoBehaviour
{
    public void ResetGame()
    {
        PersistantGameManager.Instance.masterInventory = new();
        PersistantGameManager.Instance.placedObjects = new();
        PersistantGameManager.Instance.masterCurrentItem = 0;
        PersistantGameManager.Instance.currentQuest = 0;
        foreach(var quest in PersistantGameManager.Instance.Quests)
        {
            quest.partsCompleted = 0;
        }
    }
}
