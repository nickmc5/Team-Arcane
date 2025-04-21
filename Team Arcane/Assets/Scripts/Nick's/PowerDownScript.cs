using UnityEngine;

public class PowerDownScript : MonoBehaviour
{
    private GameObject child;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistantGameManager.Quests[PersistantGameManager.currentQuest].Text == "Go to Charging Room and Power Down")
        {
            child.SetActive(true);
        }
    }
}
