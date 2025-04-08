using UnityEngine;
using System.Collections;

public class Quest
{
    public string Text;
    public int partsCompleted;
    public int totalParts;

    public Quest(string Text, int partsCompleted, int totalParts)
    {
        this.Text = Text;
        this.partsCompleted = partsCompleted;
        this.totalParts = totalParts;
    }

    public void IncrementQuest()
    {
        partsCompleted++;
    }
}
