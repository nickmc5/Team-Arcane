using UnityEngine;
using TMPro;

public class QuestUpdater : MonoBehaviour
{

    private TextMeshProUGUI questTMP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questTMP = GetComponentInChildren<TextMeshProUGUI>();
        updateHud();
    }

    public void IncrementCurrentQuest()
    {
        PersistantGameManager.Quests[PersistantGameManager.currentQuest].IncrementQuest();
        if (checkCurrentQuest() &&  (PersistantGameManager.currentQuest + 1) < PersistantGameManager.Quests.Count)
        {
            PersistantGameManager.currentQuest++;
        }
        updateHud();
    }

    private void updateHud()
    {
        questTMP.text = "TODO: \n";

        // uncomment to see all quests in the HUD
        //for (int i = 0; i < PersistantGameManager.Quests.Count; i++)
        //{
        //    questTMP.text += $"/// {PersistantGameManager.Quests[i].Text} {PersistantGameManager.Quests[i].partsCompleted}/{PersistantGameManager.Quests[i].totalParts}\n";
        //}

        questTMP.text += $"/// {PersistantGameManager.Quests[PersistantGameManager.currentQuest].Text} {PersistantGameManager.Quests[PersistantGameManager.currentQuest].partsCompleted}/{PersistantGameManager.Quests[PersistantGameManager.currentQuest].totalParts}";
    }

    private bool checkCurrentQuest()
    {
        if (PersistantGameManager.Quests[PersistantGameManager.currentQuest].partsCompleted == PersistantGameManager.Quests[PersistantGameManager.currentQuest].totalParts)
        {
            return true;
        }
        return false;
    }
}
