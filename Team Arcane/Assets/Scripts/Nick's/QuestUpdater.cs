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
        PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].IncrementQuest();
        if (checkCurrentQuest() &&  (PersistantGameManager.Instance.currentQuest + 1) < PersistantGameManager.Instance.Quests.Count)
        {
            PersistantGameManager.Instance.currentQuest++;
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

        questTMP.text += $"/// {PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].Text} {PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].partsCompleted}/{PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].totalParts}";
    }

    private bool checkCurrentQuest()
    {
        if (PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].partsCompleted == PersistantGameManager.Instance.Quests[PersistantGameManager.Instance.currentQuest].totalParts)
        {
            return true;
        }
        return false;
    }
}
