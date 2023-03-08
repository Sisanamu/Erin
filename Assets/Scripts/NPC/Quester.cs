using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quester : npcController
{
    public static Quester instance;
    public List<Quest> quests;
    public int Qid;
    public GameObject questWindow;
    public GameObject rewardWindow;
    [Header("Quest")]
    public Text titleText;
    public Text goalText;
    public Text contents;
    public Text RewardExp;
    public Text RewardItem;

    [Header("Reward")]
    public Text rewardTitle;
    public Text rewardcontents;
    public Text rewardgoalText;
    public Text rewardExp;
    public Text rewardItem;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    protected override void Update()
    {
        base.Update();
        if (Qid == quests.Count)
        {
            Qid = 0;
        }
        titleText.text = quests[Qid].title;
        goalText.text = quests[Qid].goal();
        contents.text = quests[Qid].contents;
        RewardExp.text = quests[Qid].ExpReward();
        RewardItem.text = quests[Qid].ItemReward();

        rewardTitle.text = quests[Qid].title;
        rewardgoalText.text = quests[Qid].goal();
        rewardcontents.text = quests[Qid].contents;
        rewardExp.text = quests[Qid].ExpReward();
        rewardItem.text = quests[Qid].ItemReward();
        if (dialogueOn)
        {
            dialogue.SetActive(true);
        }
        else
        {
            dialogue.GetComponentInChildren<nomaldialogue>().dialogueIndex = 0;
            dialogue.GetComponentInChildren<stillProcessQuest>().dialogueIndex = 0;
            dialogue.GetComponentInChildren<Questdialogue>().dialogueIndex = 0;
            dialogue.SetActive(false);
        }
        if (Player != null)
        {
            if (Player.quest != null)
            {
                if (Player.returnNpc && Player.quest.isSuccess
                            && Player.quest.Progress && Player.isGetQuest)
                {
                    dialogue.GetComponentInChildren<nomaldialogue>().enabled = false;
                    dialogue.GetComponentInChildren<Questdialogue>().enabled = true;
                    dialogue.GetComponentInChildren<stillProcessQuest>().enabled = false;
                }
                if (!Player.returnNpc && Player.isGetQuest
                && Player.quest.Progress && !Player.quest.isSuccess)
                {
                    dialogue.GetComponentInChildren<nomaldialogue>().enabled = false;
                    dialogue.GetComponentInChildren<Questdialogue>().enabled = false;
                    dialogue.GetComponentInChildren<stillProcessQuest>().enabled = true;
                }
            }
            else
            {
                if (!Player.isGetQuest)
                {
                    dialogue.GetComponentInChildren<nomaldialogue>().enabled = true;
                    dialogue.GetComponentInChildren<Questdialogue>().enabled = false;
                    dialogue.GetComponentInChildren<stillProcessQuest>().enabled = false;
                }
            }
        }
    }

    public void AcceptQuest()
    {
        SoundManager.instance.PlayEffects("Confirm");
        dialogue.GetComponentInChildren<nomaldialogue>().dialogueIndex = 0;
        dialogue.SetActive(false);
        questWindow.SetActive(false);
        Player.dialogueOn = false;
        dialogueOn = false;
        quests[Qid].Progress = true;
        Player.quest = quests[Qid];
        Player.isGetQuest = true;
    }

    public void refuseQuest()
    {
        SoundManager.instance.PlayEffects("Confirm");
        dialogue.GetComponentInChildren<nomaldialogue>().dialogueIndex = 0;
        Player.dialogueOn = false;
        dialogueOn = false;
        dialogue.SetActive(false);
        questWindow.SetActive(false);
    }

    public void receiveReward()
    {
        SoundManager.instance.PlayEffects("Complete");
        dialogue.GetComponentInChildren<Questdialogue>().dialogueIndex = 0;
        dialogue.SetActive(false);
        Player.isGetQuest = false;
        Player.quest = null;
        Player.returnNpc = false;
        quests[Qid].Complete();
        rewardWindow.SetActive(false);
        Player.dialogueOn = false;
        dialogueOn = false;
        Qid++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.GetComponent<playerController>();
            if (questWindow.activeSelf)
            {
                nameUI.SetActive(false);
            }
            else
            {
                nameUI.SetActive(true);
            }
            Player.NPC = gameObject.GetComponent<npcController>();
            isWalk = false;
            nameUI.SetActive(true);
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.Instance.NPC = null;
            Player = null;
            other.GetComponent<playerController>().meetNpcButton.SetActive(false);
            isWalk = true;
            nameUI.SetActive(false);
            StartCoroutine(NextPatrol());
        }
    }
}