using UnityEngine;

public class ResetPlaythrough : MonoBehaviour
{
    public void ResetGame()
    {
        PersistantGameManager.masterInventory = new();
        PersistantGameManager.placedObjects = new();
        PersistantGameManager.masterCurrentItem = -1;
        PersistantGameManager.currentQuest = 0;
        foreach(var quest in PersistantGameManager.Quests)
        {
            quest.partsCompleted = 0;
        }
    }
}
