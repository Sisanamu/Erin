using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float UIWaitTime;

    public int Level { get; set; }
    public int currentHp{get; set;}
    public int MaxHp;
    public int currentSp { get; set;}
    public int MaxSp;
    public float str { get; set; }
    public float dex { get; set; }
    public float def { get; set; }
    public float P_speed { get; set; }
    public int StatusBonous;
    public int Damage;
    public float WeaponDamage;

    public int EXP { get; set; }
    public int totalEXP { get; set; }
    public int Gold { get; set; }

    [Header("Status_Effect")]
    public float PowerTime;
    public float ChargeTime;
    public string soundName;
    public Slot_QuickSlot[] sQuitck;
    public int slotIndex;
    [SerializeField] SaveNLoad theSave;
    #region SingleTon
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            loadingSceneManager.isdone = true;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public void Init()
    {
        Level = 1;
        currentHp = 100;
        MaxHp = 100;
        currentSp = 10;
        MaxSp = 10;
        str = 10;
        dex = 10;
        def = 0;
        P_speed = 15;
        StatusBonous = 0;
        Damage = 0;
        WeaponDamage = 0;
        EXP = 0;
        totalEXP = 100;
        Gold = 0;
    }
    public int GetDamage(float EnemyDEF)
    {
        float Rnd = Random.Range(0.8f + (dex / 1000), 1.2f);
        Damage = ((int)WeaponDamage + (int)(Mathf.Ceil((str * Rnd * 0.5f) - EnemyDEF * 0.25f)));

        return Damage;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < MaxHp)
        {
            currentHp += _count;
            SoundManager.instance.PlayEffects(soundName);
            sQuitck[slotIndex].SetSlotCount(-1);
        }
        else
        {
            currentHp = MaxHp;
            sQuitck[slotIndex].SetSlotCount(-1);
        }
    }
    public void IncreaseSP(int _count)
    {
        if (currentSp + _count < MaxSp)
        {
            currentSp += _count;
            SoundManager.instance.PlayEffects(soundName);
            sQuitck[slotIndex].SetSlotCount(-1);
        }
        else
        {
            currentSp = MaxSp;
            sQuitck[slotIndex].SetSlotCount(-1);
        }
    }
    public void IncreaseEXP(int _count)
    {
        EXP += _count;
    }
    public void ChangeGold(int _count)
    {
        Gold += _count;
        theSave.SaveData();
    }
    public void statusUP(string statusName)
    {
        switch (statusName)
        {
            case "MaxHp":
                if (StatusBonous != 0)
                {
                    StatusBonous--;
                    MaxHp += 5;
                    currentHp = MaxHp;
                    theSave.SaveData();
                }
                break;
            case "MaxSp":
                if (StatusBonous != 0)
                {
                    StatusBonous--;
                    MaxSp++;
                    currentSp = MaxSp;
                    theSave.SaveData();
                }
                break;
            case "STR":
                if (StatusBonous != 0)
                {
                    StatusBonous--;
                    str++;
                    theSave.SaveData();
                }
                break;
            case "DEX":
                if (StatusBonous != 0)
                {
                    dex += 1;
                    StatusBonous--;
                    theSave.SaveData();
                }
                break;
        }
    }
}