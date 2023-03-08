using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plus_Button : MonoBehaviour
{
    Text UpStatus;

    Button Plus;
    public STATUS UPSTATUS;

    void Start()
    {
        UpStatus = GetComponent<Text>();
        Plus = GetComponent<Button>();

        Plus.onClick.AddListener(UP_STATUS);
    }

    // Update is called once per frame
    void Update()
    {

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
                }
                break;
            case STATUS.MaxSp:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.MaxSp++;
                    GameManager.Instance.currentSp = GameManager.Instance.MaxSp;
                }
                break;
            case STATUS.STR:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.str++;
                }
                break;
            case STATUS.DEX:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.dex += 1;
                }
                break;
            case STATUS.DEF:
                if (GameManager.Instance.StatusBonous != 0)
                {
                    GameManager.Instance.StatusBonous--;
                    GameManager.Instance.def++;
                }
                break;
        }
    }
}