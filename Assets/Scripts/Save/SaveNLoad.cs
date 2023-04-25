using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string SceneName;
    public int Level;
    public int currentHp;
    public int MaxHp;
    public int currentSp;
    public int MaxSp;
    public float str;
    public float dex;
    public int StatusBonous;
    public int EXP;
    public int totalEXP;
    public int Gold;
    public Quest quest;
    public int Qid;
    public bool returnNpc;
    public bool isGetQuest;

    public Vector3 playerPos;
    public Vector3 playerRotation;
    public List<int> invenArrayNum = new List<int>();
    public List<int> invenCount = new List<int>();
    public List<int> invenReinfoece = new List<int>();
    public List<string> invenItemName = new List<string>();

    public List<int> quickArrayNum = new List<int>();
    public List<string> quickItemName = new List<string>();
    public List<int> quickItemCount = new List<int>();
}

public class SaveNLoad : MonoBehaviour
{
    private SaveData saveDate = new SaveData();
    public string File_Path;
    public string getSceneName;
    private GameManager theState;
    private playerController thePlayer;
    private Inventory theInven;
    private Quester theQuest;
    private QuickSlot theQuick;

    private void Start()
    {
        File_Path = Application.persistentDataPath + "/save.json";
    }
    public void SaveData()
    {
        thePlayer = FindObjectOfType<playerController>();
        theInven = FindObjectOfType<Inventory>();
        theState = FindObjectOfType<GameManager>();
        theQuick = FindObjectOfType<QuickSlot>();

        saveDate.SceneName = thePlayer.SceneName;
        saveDate.playerPos = thePlayer.transform.position;
        saveDate.playerRotation = thePlayer.transform.eulerAngles;
        saveDate.quest = thePlayer.quest;
        saveDate.returnNpc = thePlayer.returnNpc;
        saveDate.isGetQuest = thePlayer.isGetQuest;

        saveDate.Level = theState.Level;
        saveDate.currentHp = theState.currentHp;
        saveDate.currentSp = theState.currentSp;
        saveDate.MaxHp = theState.MaxHp;
        saveDate.MaxSp = theState.MaxSp;
        saveDate.str = theState.str;
        saveDate.dex = theState.dex;
        saveDate.StatusBonous = theState.StatusBonus;
        saveDate.EXP = theState.EXP;
        saveDate.totalEXP = theState.totalEXP;
        saveDate.Gold = theState.Gold;

        if (FindObjectOfType<Quester>())
        {
            theQuest = FindObjectOfType<Quester>();
            saveDate.Qid = theQuest.Qid;
        }

        Slot[] slots = theInven.Getslots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveDate.invenArrayNum.Clear();
                saveDate.invenItemName.Clear();
                saveDate.invenCount.Clear();
                saveDate.invenReinfoece.Clear();
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveDate.invenArrayNum.Add(i);
                saveDate.invenItemName.Add(slots[i].item.ItemName);
                saveDate.invenCount.Add(slots[i].itemCount);
                saveDate.invenReinfoece.Add(slots[i].Reinforce);
            }
        }
        Slot_QuickSlot[] quickSlots = theQuick.GetQuick();
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item != null)
            {
                saveDate.quickArrayNum.Clear();
                saveDate.quickItemCount.Clear();
                saveDate.quickItemName.Clear();
            }
        }
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item != null)
            {
                saveDate.quickArrayNum.Add(i);
                saveDate.quickItemCount.Add(quickSlots[i].itemCount);
                saveDate.quickItemName.Add(quickSlots[i].item.ItemName);
            }
        }

        string json = JsonUtility.ToJson(saveDate);
        File.WriteAllText(File_Path, json);
        Debug.Log(json);
        Debug.Log(File_Path);
        Debug.Log("저장 완료");
    }
    public void LoadData()
    {
        if (File.Exists(File_Path))
        {
            if (SceneManager.GetActiveScene().name == "GameStart")
            {
                string loadJson = File.ReadAllText(File_Path);
                saveDate = JsonUtility.FromJson<SaveData>(loadJson);
                getSceneName = saveDate.SceneName;
                Debug.Log(File_Path);
                Debug.Log($"로드 완료 : {getSceneName}");
            }
            else
            {
                string loadJson = File.ReadAllText(File_Path);
                saveDate = JsonUtility.FromJson<SaveData>(loadJson);
                thePlayer = FindObjectOfType<playerController>();
                theInven = FindObjectOfType<Inventory>();
                theState = FindObjectOfType<GameManager>();
                theQuick = FindObjectOfType<QuickSlot>();

                thePlayer.transform.position = saveDate.playerPos;
                thePlayer.transform.eulerAngles = saveDate.playerRotation;
                thePlayer.returnNpc = saveDate.returnNpc;
                thePlayer.isGetQuest = saveDate.isGetQuest;

                theState.Level = saveDate.Level;
                theState.currentHp = saveDate.currentHp;
                theState.currentSp = saveDate.currentSp;
                theState.MaxHp = saveDate.MaxHp;
                theState.MaxSp = saveDate.MaxSp;
                theState.str = saveDate.str;
                theState.dex = saveDate.dex;
                theState.StatusBonus = saveDate.StatusBonous;
                theState.EXP = saveDate.EXP;
                theState.totalEXP = saveDate.totalEXP;
                theState.Gold = saveDate.Gold;
                thePlayer.quest = saveDate.quest;
                if (FindObjectOfType<Quester>())
                {
                    theQuest = FindObjectOfType<Quester>();
                    theQuest.Qid = saveDate.Qid;
                }

                for (int i = 0; i < saveDate.invenItemName.Count; i++)
                {
                    theInven.LoadtoInven(saveDate.invenArrayNum[i], saveDate.invenItemName[i], saveDate.invenCount[i], saveDate.invenReinfoece[i]);
                }
                for (int i = 0; i < saveDate.quickItemName.Count; i++)
                {
                    theQuick.LoadtoQuick(saveDate.quickArrayNum[i], saveDate.quickItemName[i], saveDate.quickItemCount[i]);
                }
                Debug.Log("로드 완료");
            }
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다");
        }
    }
}