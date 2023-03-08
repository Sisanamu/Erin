using System.Collections;
using System.Collections.Generic;
using SkillController;
using UnityEngine;
using UnityEngine.UI;

public class Defence : Skill_Controller
{
    private void Start()
    {
        SkillButton.onClick.AddListener(ClickSkill);
    }

    void ClickSkill()
    {
        if (CanUse) Defence_On();
        else StartCoroutine(CannotuseSkill());
    }

    public void Defence_On()
    {
        StartCool();
        Player.GetComponent<playerController>().isDefence = !Player.GetComponent<playerController>().isDefence;
        Player.GetComponent<playerController>().currentTime = 0;
        GameManager.Instance.currentSp -= needSP;
        anim.SetBool("IsDefence", Player.GetComponent<playerController>().isDefence);
    }
}
