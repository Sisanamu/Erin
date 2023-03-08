using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillController;

public class PowerUp : Skill_Controller
{
    [Header("Buff")]
    public Status_Effect type;
    public Sprite Icon;
    public string SoundName;

    public GameObject PowerEffect;
    public int PowerUpRatio;
    float PowerUpTime;

    void Start()
    {
        SkillButton.onClick.AddListener(ClickSkill);
        PowerUpTime = GameManager.Instance.PowerTime;
        Icon = GetComponent<Image>().sprite;
    }

    void ClickSkill()
    {
        if (CanUse) Can_PowerUp();
        else StartCoroutine(CannotuseSkill());
    }

    public void Can_PowerUp()
    {
        StartCool();
        Player.GetComponent<playerController>().currentTime = 0;
        StartCoroutine(PowerUpEffect());
        Buff_Array.instance.CreateBuff(type, PowerUpTime, Icon);
        GameManager.Instance.currentSp -= (int)needSP;
        StartCoroutine(PowerUpSTR());
    }

    IEnumerator PowerUpEffect()
    {
        anim.SetTrigger("IsPowerUP");
        SoundManager.instance.PlayEffects(SoundName);
        PowerEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        PowerEffect.SetActive(false);
    }
    IEnumerator PowerUpSTR()
    {
        float temp = GameManager.Instance.str * PowerUpRatio;
        GameManager.Instance.str = temp + GameManager.Instance.str;
        yield return new WaitForSeconds(PowerUpTime);
        GameManager.Instance.str = GameManager.Instance.str - temp;
    }
}
