using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Update : MonoBehaviour
{
    Text StatusText;
    public STATUS status;

    void Start()
    {
        StatusText = GetComponent<Text>();
    }

    void Update()
    {
        switch (status)
        {
            case STATUS.MaxHp:
                StatusText.text = GameManager.Instance.MaxHp.ToString();
                break;
            case STATUS.MaxSp:
                StatusText.text = GameManager.Instance.MaxSp.ToString();
                break;
            case STATUS.STR:
                StatusText.text = GameManager.Instance.str.ToString();
                break;
            case STATUS.DEX:
                StatusText.text = GameManager.Instance.dex.ToString();
                break;
            case STATUS.DEF:
                StatusText.text = GameManager.Instance.def.ToString();
                break;
            case STATUS.Level:
                StatusText.text = GameManager.Instance.Level.ToString();
                break;
            case STATUS.StatusBonus:
                StatusText.text = GameManager.Instance.StatusBonus.ToString();
                break;
        }
    }
}
