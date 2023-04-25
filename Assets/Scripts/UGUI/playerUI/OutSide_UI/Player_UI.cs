using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private SaveNLoad thesave;
    public Image HP;
    public Image SP;
    public Image EXP;
    public Animator anim;
    public GameObject LevelUpEf;
    public Text Level;

    public float ratio_HP;
    public float ratio_SP;
    public float ratio_EXP;

    void Update()
    {
        UI_HP();
        UI_SP();
        UI_EXP();
        UI_Level();
    }

    void UI_HP()
    {
        ratio_HP = (float)GameManager.Instance.currentHp / GameManager.Instance.MaxHp;
        HP.GetComponent<Image>().fillAmount = Mathf.Lerp(HP.GetComponent<Image>().fillAmount, ratio_HP, 0.5f);
    }
    void UI_SP()
    {
        ratio_SP = (float)GameManager.Instance.currentSp / GameManager.Instance.MaxSp;
        SP.GetComponent<Image>().fillAmount = Mathf.Lerp(SP.GetComponent<Image>().fillAmount, ratio_SP, 0.5f);
    }
    void UI_EXP()
    {
        ratio_EXP = (float)GameManager.Instance.EXP / GameManager.Instance.totalEXP;
        EXP.GetComponent<Image>().fillAmount = ratio_EXP;
        LevelUP();
    }
    void UI_Level()
    {
        Level.text = GameManager.Instance.Level.ToString();
    }
    void LevelUP()
    {
        if (ratio_EXP >= 1)
        {
            GameManager.Instance.EXP = GameManager.Instance.EXP - GameManager.Instance.totalEXP;
            GameManager.Instance.Level += 1;
            GameManager.Instance.StatusBonus += 5;
            GameManager.Instance.totalEXP += 50;
            GameManager.Instance.currentHp = GameManager.Instance.MaxHp;
            GameManager.Instance.currentSp = GameManager.Instance.MaxSp;
            StartCoroutine(LevelUpEffect());
            thesave.SaveData();
        }
    }
    IEnumerator LevelUpEffect()
    {
        LevelUpEf.SetActive(true);
        yield return new WaitForSeconds(2f);
        LevelUpEf.SetActive(false);
    }
}