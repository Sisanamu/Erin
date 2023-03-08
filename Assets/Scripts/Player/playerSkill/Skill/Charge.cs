using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillController;

public class Charge : Skill_Controller
{
    [Header("DeBuff")]
    public Status_Effect type;
    public Sprite Icon;
    public string SoundName;

    GameObject Enemy;
    float temp;
    float DeBuffTime;

    void Start()
    {
        SkillButton.onClick.AddListener(ClickSkill);
        DeBuffTime = GameManager.Instance.ChargeTime;
    }

    void ClickSkill()
    {
        if (CanUse) Charge_On();
        else StartCoroutine(CannotuseSkill());
    }

    public void Charge_On()
    {
        StartCool();
        SoundManager.instance.PlayEffects(SoundName);
        Enemy = Player.GetComponent<playerController>().Enemy;
        temp = Enemy.GetComponent<enemyController>().def;
        Enemy.GetComponent<enemyController>().def = 0;
        GameManager.Instance.currentSp -= needSP;
        StartCoroutine(returnDef());
    }

    IEnumerator returnDef()
    {
        Enemy.GetComponent<enemyController>().DeBuffEffect_On(true);
        Enemy_Status_Effect.instance.CreateDeBuff(type, DeBuffTime, Icon);
        yield return new WaitForSeconds(DeBuffTime);
        Enemy.GetComponent<enemyController>().DeBuffEffect_On(false);
        if (Enemy != null)
        {
            Enemy.GetComponent<enemyController>().def = temp;
        }
        else if (Enemy == null)
        {
            Enemy.GetComponent<enemyController>().DeBuffEffect_On(false);
            temp = 0;
        }
        temp = 0;
    }
}