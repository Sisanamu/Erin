using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float UIWaitTime;

    [Header("Status")]
    public int Level = 1;
    public int currentHp = 100;
    public int MaxHp = 100;
    public int currentSp = 10;
    public int MaxSp = 10;
    public float str = 10;
    public float dex = 10;
    public float def = 5;
    public float P_speed;
    public int StatusBonous;
    public int Damage;
    public float WeaponDamage;

    public int EXP;
    public int totalEXP = 100;
    public int Gold;

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
            currentSp = MaxSp;
        sQuitck[slotIndex].SetSlotCount(-1);
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
}