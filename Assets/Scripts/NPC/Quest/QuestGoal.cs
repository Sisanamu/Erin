using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public QuestGoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }
    public void EnemyKilled()
    {
        if (goalType == QuestGoalType.Kill)
        {
            currentAmount++;
        }
    }
    public void ItemCollected()
    {
        if(goalType == QuestGoalType.Gathering)
        {
            currentAmount++;
        }
    }
}