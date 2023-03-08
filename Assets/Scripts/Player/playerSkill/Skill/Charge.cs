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

    public GameObject Status;
    public GameObject Enemy;
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
        Enemy = Player.GetComponent<playerController>().Enemy;
        SoundManager.instance.PlayEffects(SoundName);
        temp = Enemy.GetComponent<enemyController>().def;
        Enemy.GetComponent<enemyController>().def = 0;
        GameManager.Instance.currentSp -= needSP;
        Status =  Enemy.gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Enemy_Status_Effect>().gameObject;
        Enemy_Status_Effect.instance.CreateDeBuff(type, DeBuffTime, Icon);
        StartCoroutine(returnDef());
    }

    IEnumerator returnDef()
    {
        Enemy.GetComponent<enemyController>().DeBuffEffect_On(true);
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