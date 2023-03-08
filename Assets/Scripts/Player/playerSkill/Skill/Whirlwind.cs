using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillController;

public class Whirlwind : Skill_Controller
{
    void Start()
    {
        SkillButton.onClick.AddListener(ClickSkill);
    }

    void ClickSkill()
    {
        if (CanUse) WhirlWind_On();
        else StartCoroutine(CannotuseSkill());
    }

    public void WhirlWind_On()
    {
        StartCool();
        anim.SetTrigger("IsWhirlwind");
        Player.GetComponent<playerController>().currentTime = 0;
        GameManager.Instance.currentSp -= needSP;
        Player.GetComponent<playerController>().isWhirlwind = true;
    }
}