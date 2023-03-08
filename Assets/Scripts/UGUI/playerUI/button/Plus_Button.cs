using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plus_Button : MonoBehaviour
{
    Text UpStatus;
    [SerializeField] private SaveNLoad theSave;
    Button Plus;
    public STATUS UPSTATUS;

    void Start()
    {
        UpStatus = GetComponent<Text>();
        Plus = GetComponent<Button>();

        Plus.onClick.AddListener(UP_STATUS);
    }

    void UP_STATUS()
    {
        switch (UPSTATUS)
        {
            case STATUS.MaxHp:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.MaxHp += 5;
                    GameManager.Instance.currentHp = GameManager.Instance.MaxHp;
                    theSave.SaveData();
                }
                break;
            case STATUS.MaxSp:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.MaxSp++;
                    GameManager.Instance.currentSp = GameManager.Instance.MaxSp;
                    theSave.SaveData();
                }
                break;
            case STATUS.STR:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.str++;
                    theSave.SaveData();
                }
                break;
            case STATUS.DEX:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.dex += 1;
                    theSave.SaveData();
                }
                break;
            case STATUS.DEF:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.def++;
                    theSave.SaveData();
                }
                break;
        }
    }
}