using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_window_Text : MonoBehaviour
{
    Text Statustext;
    public STATUS status;
    void Start()
    {
        Statustext = GetComponent<Text>();
    }

    void Update()
    {
        switch (status)
        {
            case STATUS.MaxHp:
                Statustext.text = "(" + GameManager.Instance.currentHp + "/" + GameManager.Instance.MaxHp + ")";
                break;
            case STATUS.MaxSp:
                Statustext.text = "(" + GameManager.Instance.currentSp + "/" + GameManager.Instance.MaxSp + ")";
                break;
            case STATUS.totalEXP:
                Statustext.text = "(" + GameManager.Instance.EXP + "/" + GameManager.Instance.totalEXP + ")";
                break;
        }
    }
}