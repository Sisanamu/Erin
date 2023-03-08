using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/Quest")]
[System.Serializable]
public class Quest
{
    public bool isSuccess;
    public bool Progress;

    public int QId;
    public string title; // 퀘스트 이름
    public string contents; // 퀘스트 내용
    public string EnemyName; // 잡아야할 몬스터의 이름

    public List<Item> RewardItem;
    public int rewardItemCount;
    public int RewardExp;
    public int RewardGold;
    public QuestGoal questGoal;

    private void Start()
    {
        isSuccess = false;
        Progress = false;
        questGoal.currentAmount = 0;
    }

    public void Complete()
    {
        isSuccess = false;
        Progress = false;
        questGoal.currentAmount = 0;
        GameManager.Instance.IncreaseEXP(RewardExp);
        GameManager.Instance.Gold += RewardGold;
        for (int i = 0; i < RewardItem.Count; i++)
        {
            Inventory.instance.AcquireItem(RewardItem[i], rewardItemCount, 0);
        }
    }
    public string goal()
    {
        return ($"목적 : {EnemyName} : ( {questGoal.currentAmount.ToString()} / {questGoal.requiredAmount.ToString()} )");
    }
    public string ExpReward()
    {
        string str = null;
        str += ($"보상 - 경험치 : {RewardExp.ToString()}\n" + $"                골드 : {RewardGold.ToString()}");

        return str;
    }
    public string ItemReward()
    {
        string s = null;
        for (int i = 0; i < RewardItem.Count; i++)
        {
            s += ($" {RewardItem[i].ItemName} {rewardItemCount} 개\n");
        }
        return s;
    }
}