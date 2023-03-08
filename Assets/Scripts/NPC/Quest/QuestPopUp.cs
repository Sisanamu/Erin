using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUp : MonoBehaviour
{
    public GameObject Player;
    public GameObject CompleteQuestText;
    public Outline CompleteQuestLine;
    public Text titleBoard;
    public Text goalsBoard;

    void Update()
    {
        if (Player.GetComponent<playerController>().quest != null)
        {
            titleBoard.text = Player.GetComponent<playerController>().quest.title;
            goalsBoard.text = goal();

            if (Player.GetComponent<playerController>().quest.isSuccess)
            {
                CompleteQuestText.SetActive(true);
                CompleteQuestLine.enabled = true;
            }
            else
            {
                CompleteQuestText.SetActive(false);
                CompleteQuestLine.enabled = false;
            }
        }
    }
    string goal()
    {
        return ($"{Player.GetComponent<playerController>().quest.EnemyName} : ({Player.GetComponent<playerController>().quest.questGoal.currentAmount} / {Player.GetComponent<playerController>().quest.questGoal.requiredAmount})");
    }
}