using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField]private SaveNLoad thesave;
    public Image HP;
    public Image SP;
    public Image EXP;
    public Animator anim;
    public GameObject LevelUpEf;
    public Text Level;
    public GameObject Joystick;

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

        if (ratio_HP <= 0)
            ratio_HP = 0;
        HP.GetComponent<Image>().fillAmount = Mathf.Lerp(HP.GetComponent<Image>().fillAmount, ratio_HP, Time.deltaTime * 0.75f);
    }
    void UI_SP()
    {
        ratio_SP = (float)GameManager.Instance.currentSp / GameManager.Instance.MaxSp;
        if (ratio_SP <= 0)
            ratio_SP = 0;
        SP.GetComponent<Image>().fillAmount = Mathf.Lerp(SP.GetComponent<Image>().fillAmount, ratio_SP, Time.deltaTime * 0.75f);
    }
    void UI_EXP()
    {
        ratio_EXP = (float)GameManager.Instance.EXP / GameManager.Instance.totalEXP;

        EXP.GetComponent<Image>().fillAmount = ratio_EXP;

        if(GameManager.Instance.EXP <= 0)
        {
            GameManager.Instance.EXP = 0;
        }
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
            GameManager.Instance.StatusBonous += 5;
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