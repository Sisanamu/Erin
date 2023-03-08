using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkillController
{
    public class Skill_Controller : MonoBehaviour
    {
        public GameObject Player;
        protected Animator anim;
        public GameObject Cannot_Use_Skill;

        [SerializeField] protected Image CoolTime;
        public Text cool_text;
        public GameObject Skill_Window;
        public Button SkillButton;
        public float coolTime;
        public float StartTime;
        public float ratio;
        public bool CanUse = true;
        [SerializeField] protected int needSP;

        void Awake()
        {
            anim = Player.GetComponent<Animator>();
            cool_text = GetComponentInChildren<Text>();
            SkillButton = GetComponent<Button>();
            Skill_Window = GetComponentInParent<GridLayoutGroup>().gameObject;
        }

        void Update()
        {
            StartTime -= Time.deltaTime;

            if (StartTime <= 0 || !SkillButton.GetComponent<Image>().enabled)
            {
                StartTime = 0;
                ratio = 0;
                cool_text.enabled = false;
            }

            ratio = StartTime / coolTime;
            CoolTime.fillAmount = ratio;
            if (ratio == 0 && SkillButton.GetComponent<Image>().enabled)
            {
                SkillButton.enabled = true;
                cool_text.enabled = false;
            }

            if (GameManager.Instance.currentSp < needSP)
                CanUse = false;
            else
                CanUse = true;

            cool_text.text = StartTime.ToString("F1");
        }

        public void StartCool()
        {
            StartTime = coolTime;
            SkillButton.enabled = false;
            cool_text.enabled = true;
        }
        protected IEnumerator CannotuseSkill()
        {
            Cannot_Use_Skill.SetActive(true);
            yield return new WaitForSeconds(1f);
            Cannot_Use_Skill.SetActive(false);
        }
    }
}