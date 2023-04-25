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
    [Header("dialogue")]
    public nomaldialogue Nomal;
    public stillProcessQuest stillQuest;
    public Questdialogue Reward;

    [SerializeField] private SaveNLoad theSave;
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
        Nomal = dialogue.GetComponentInChildren<nomaldialogue>();
        stillQuest = dialogue.GetComponentInChildren<stillProcessQuest>();
        Reward = dialogue.GetComponentInChildren<Questdialogue>();
    }

    protected override void Update()
    {
        base.Update();
        if (Qid == quests.Count)
            Qid = 0;
        if (dialogueOn)
        {
            dialogue.SetActive(true);
        }
        else
        {
            Nomal.dialogueIndex = 0;
            stillQuest.dialogueIndex = 0;
            Reward.dialogueIndex = 0;
            dialogue.SetActive(false);
        }
        if (Player != null)
        {
            ChangeDialogue();
        }
    }

    void ChangeDialogue()
    {
        if (Player.quest != null && Player.isGetQuest)
        {
            if (Player.returnNpc && Player.quest.isSuccess)
                GetReward();
            else
                StillQuest();

        }
        else
            nomalDialogue();
    }
    void nomalDialogue()
    {
        QuestInfo(Qid);
        Nomal.enabled = true;
        Reward.enabled = false;
        stillQuest.enabled = false;
    }
    void StillQuest()
    {
        Nomal.enabled = false;
        Reward.enabled = false;
        stillQuest.enabled = true;
    }
    void GetReward()
    {
        RewardInfo(Qid);
        Nomal.enabled = false;
        Reward.enabled = true;
        stillQuest.enabled = false;
    }


    public void AcceptQuest()
    {
        SoundManager.instance.PlayEffects("Confirm");
        Nomal.dialogueIndex = 0;
        dialogue.SetActive(false);
        questWindow.SetActive(false);
        Player.dialogueOn = false;
        dialogueOn = false;
        quests[Qid].Progress = true;
        Player.quest = quests[Qid];
        Player.isGetQuest = true;
        theSave.SaveData();
    }

    public void refuseQuest()
    {
        SoundManager.instance.PlayEffects("Confirm");
        Nomal.dialogueIndex = 0;
        Player.dialogueOn = false;
        dialogueOn = false;
        dialogue.SetActive(false);
        questWindow.SetActive(false);
    }
    void QuestInfo(int Qid)
    {
        titleText.text = quests[Qid].title;
        goalText.text = quests[Qid].goal();
        contents.text = quests[Qid].contents;
        RewardExp.text = quests[Qid].ExpReward();
        RewardItem.text = quests[Qid].ItemReward();
    }
    void RewardInfo(int Qid)
    {
        rewardTitle.text = quests[Qid].title;
        rewardgoalText.text = quests[Qid].goal();
        rewardcontents.text = quests[Qid].contents;
        rewardExp.text = quests[Qid].ExpReward();
        rewardItem.text = quests[Qid].ItemReward();
    }

    public void receiveReward()
    {
        quests[Qid].Complete();
        SoundManager.instance.PlayEffects("Complete");
        Reward.dialogueIndex = 0;
        Player.resetQuest();
        rewardWindow.SetActive(false);
        dialogueOn = false;
        Qid++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.GetComponent<playerController>();
            Player.NPC = gameObject.GetComponent<npcController>();
            if (questWindow.activeSelf)
                nameUI.SetActive(false);
            else
                nameUI.SetActive(true);
            isWalk = false;
            nameUI.SetActive(true);
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.meetNpcButton.SetActive(false);
            playerController.Instance.NPC = null;
            Player = null;
            isWalk = true;
            nameUI.SetActive(false);
            StartCoroutine(NextPatrol());
        }
    }
}