using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class playerController : MonoBehaviour
{
    readonly int Attack01 = Animator.StringToHash("Attack01");
    readonly int Attack02 = Animator.StringToHash("Attack02");
    readonly int Attack03 = Animator.StringToHash("Attack03");

    public string SceneName;
    [Header("Combo")]
    public int ComboStep;

    [SerializeField] public SkinnedMeshRenderer[] skinRen;
    public creature_STATE P_STATE = creature_STATE.IDLE;
    Animator anim;
    Rigidbody rgd;

    public Quest quest;
    public bool returnNpc;

    public int MaxHP;
    public int MaxSP;

    public GameObject TargettingImage;
    public GameObject Enemy;
    public npcController NPC;
    public GameObject HitBox;
    public GameObject chaseEnemyButton;
    public GameObject meetNpcButton;
    public JoyStick JoyController;
    public Image CamController;
    public GameObject[] SkillIcon;
    public GameObject[] SkillBackGround;
    public GameObject Damagetxt;
    public GameObject questPopup;
    public GameObject revivePlayer;
    public GameObject noHaveSavePoint;
    public GameObject QuickSlot;
    public GameObject ClearBossWindow;
    public GameObject Canvas;
    public Vector3 revivePoint;

    public LayerMask EnemyMask;
    public LayerMask NPCMask;
    public LayerMask ETCMask;
    public float SearchRange;
    float AttackRange;
    public float attackDelay;
    public float attackRange;
    public float currentTime;
    public float PowerUpTime;
    private float npcSearchRange;
    private int PowerUpSRT;
    public int enemyDamage;
    public bool Chasetarget;
    private bool isAttack;
    public bool isGetQuest;
    public bool isWalk;
    public bool isBattle;
    public bool isDie;
    public bool dialogueOn;
    public bool ClearBoss;
    public float currentEnemy;
    float currentNPC;
    public float enemyDist;
    float npcDist;
    int temp;

    [Header("Skill")]
    public bool isDefence;
    public bool isWhirlwind;

    [Header("Effect")]
    public GameObject DefenceEffect;
    public GameObject HitEffect;
    public Collider[] enemylist;
    public List<GameObject> Enemys = new List<GameObject>();
    public string[] soundName;
    #region SingleTon
    private static playerController instance = null;
    public static playerController Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion
    void Start()
    {
        P_STATE = creature_STATE.IDLE;
        anim = GetComponent<Animator>();
        rgd = GetComponent<Rigidbody>();
        quest = null;
    }

    void Update()
    {
        SceneName = SceneManager.GetActiveScene().name;
        currentTime += Time.deltaTime;
        switch (P_STATE)
        {
            case creature_STATE.IDLE:
                Idle();
                break;
            case creature_STATE.ATTACK:
                Attack();
                break;
            case creature_STATE.Death:
                Die();
                Enemy = null;
                break;
        }
        if (currentTime > attackDelay)
        {
            isAttack = false;
            currentTime = 0;
        }
        if (quest != null)
        {
            if (quest.Progress)
                questPopup.SetActive(true);
            else if (!quest.Progress)
            {
                returnNpc = false;
                quest = null;
            }
        }
        else if (quest == null)
        {
            questPopup.SetActive(false);
        }
        if (NPC != null)
        {
            meetNpcButton.SetActive(true);
            if (dialogueOn)
                Rotate(NPC.gameObject);
        }
        else
            meetNpcButton.SetActive(false);
        if(ClearBoss)
            StartCoroutine(ClearBossUI());
        if(loadingSceneManager.isdone)
        {
            Canvas.SetActive(true);
            rgd.isKinematic = false;
        }
        if (isBattle)
        {
            StopCoroutine(FindTarget());
        }
        else if (!isBattle)
        {
            StartCoroutine(FindTarget());
        }
    }

    private void Idle()
    {
        anim.SetBool("State_Battle", false);
        chaseEnemyButton.SetActive(false);
        anim.ResetTrigger("IsHit");
        anim.ResetTrigger("IsCharge");
        anim.ResetTrigger("IsWhirlwind");
        anim.ResetTrigger("IsDefenceHit");
        ComboStep = 0;
        TargettingImage = null;
        Chasetarget = false;
        isBattle = false;
        revivePlayer.SetActive(false);
        Enemy = null;
        chaseEnemyButton.SetActive(false);
        Skill_Button_off();
        isAttack = false;
        StartCoroutine(FindTarget());
    }

    private void Attack()
    {
        if (!isDie)
        {
            if (Enemy.GetComponent<enemyController>().CurrentHp <= 0 && Enemy.GetComponent<enemyController>().isDie)
            {
                P_STATE = creature_STATE.IDLE;
                StartCoroutine(FindTarget());
                ComboStep = 0;
                isAttack = false;
                currentTime = 0;
                TargettingImage.SetActive(false);
                Chasetarget = false;
            }
            if (Enemy != null && !Chasetarget && (transform.position - Enemy.transform.position).magnitude < SearchRange)
            {
                if(!SkillIcon[0].GetComponent<Image>().enabled)
                {
                    Skill_Button_On();
                }
                TargettingImage.SetActive(true);
                chaseEnemyButton.SetActive(true);
                JoyController.gameObject.SetActive(!Chasetarget);
                anim.SetBool("State_Battle", true);
            }
            else if (Enemy != null && Chasetarget && (transform.position - Enemy.transform.position).magnitude > attackRange)
            {
                JoyController.gameObject.SetActive(false);
                Rotate(Enemy);
                Move();
            }
            else if (Enemy != null &&
                (transform.position - Enemy.transform.position).magnitude <= attackRange)
            {
                anim.SetBool("IsMove", false);
                if (isAttack == false)
                {
                    transform.LookAt(Enemy.transform);
                    ComboAttack();
                    currentTime = 0;
                }
            }
        }
        else
            P_STATE = creature_STATE.Death;
    }

    void Die()
    {
        Chasetarget = false;
        anim.SetBool("Diestay", true);
        anim.SetBool("State_Battle", false);
        anim.ResetTrigger("IsHit");
        if(revivePlayer.activeSelf)
        {
            StopAllCoroutines();
        }
    }
    IEnumerator FindTarget()
    {
        yield return new WaitForSeconds(0.5f);
        Enemys.Clear();
        enemylist = Physics.OverlapSphere(transform.position, SearchRange, EnemyMask);

        for (int i = 0; i < enemylist.Length; i++)
        {
            Transform Enemytrans = enemylist[i].transform;
            Vector3 dirEnemy = (Enemytrans.position - transform.position).normalized;
            Enemys.Add(Enemytrans.transform.gameObject);
        }
        if (Enemys.Count != 0)
        {
            P_STATE = creature_STATE.ATTACK;
            Enemy = Enemys[0];
            currentEnemy = Vector3.Distance(transform.position, Enemys[0].transform.position);

            for (int i = 0; i < Enemys.Count; i++)
            {
                enemyDist = Vector3.Distance(transform.position, Enemys[i].transform.position);
                if (enemyDist < currentEnemy)
                {
                    Enemy = Enemys[i];
                    currentEnemy = enemyDist;
                }
                TargettingImage = Enemy.transform.Find("Canvas").transform.gameObject;
            }
        }
        else if (enemylist.Length == 0 && TargettingImage != null)
        {
            TargettingImage = null;
            P_STATE = creature_STATE.IDLE;
        }
    }

    void Move()
    {
        anim.SetBool("IsMove", true);
        transform.Translate(Vector3.forward * GameManager.Instance.P_speed * Time.deltaTime);
    }

    public void Rotate(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(dir), 30f * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(50, 50, 50, 50);
        Gizmos.DrawSphere(transform.position, SearchRange);
    }

    void ComboAttack()
    {
        switch (ComboStep)
        {
            case 0:
                if (!isAttack && !isDefence)
                {
                    anim.Play(Attack01);
                    SoundManager.instance.PlayEffects(soundName[2]);
                    ComboStep++;
                    isAttack = true;
                }
                break;
            case 1:
                if (!isAttack && !isDefence)
                {
                    anim.Play(Attack02);
                    SoundManager.instance.PlayEffects(soundName[2]);
                    ComboStep++;
                    isAttack = true;
                }
                break;
            case 2:
                if (!isAttack && !isDefence)
                {
                    anim.Play(Attack03);
                    SoundManager.instance.PlayEffects(soundName[2]);
                    ComboStep = 0;
                    isAttack = true;
                }
                break;
        }

    }
    void EndAttackEvent()
    {
        isAttack = false;
    }

    void OnHitBox()
    {
        HitBox.SetActive(true);
    }
    void OffHitBox()
    {
        HitBox.SetActive(false);
    }

    public void nowPlaceRevive(int EXP)
    {
        MaxHP = GameManager.Instance.MaxHp;
        MaxSP = GameManager.Instance.MaxSp;
        GameManager.Instance.EXP -= EXP;
        GameManager.Instance.currentHp = MaxHP;
        GameManager.Instance.currentSp = MaxSP;
        isDie = false;
        P_STATE = creature_STATE.IDLE;
        revivePlayer.SetActive(false);
        anim.SetBool("Diestay", false);
        anim.SetTrigger("GetUp");
    }
    public void lastPlaceRevive(int EXP)
    {
        MaxHP = GameManager.Instance.MaxHp;
        MaxSP = GameManager.Instance.MaxSp;
        if (revivePoint.x != 0)
        {
            transform.position = revivePoint;
            GameManager.Instance.EXP -= EXP;
            GameManager.Instance.currentHp = MaxHP;
            GameManager.Instance.currentSp = MaxSP;
            isDie = false;
            P_STATE = creature_STATE.IDLE;
            revivePlayer.SetActive(false);
            anim.SetBool("Diestay", false);
            anim.SetTrigger("GetUp");
        }
        else if (revivePoint.y == 0)
        {
            StartCoroutine(noHaveSave());
        }
    }

    void Skill_Button_On()
    {
        for (int i = 0; i < SkillBackGround.Length; i++)
        {
            SkillBackGround[i].GetComponent<Image>().enabled = true;
            SkillIcon[i].GetComponent<Image>().enabled = true;
            SkillIcon[i].GetComponent<Button>().enabled = true;
        }
    }
    void Skill_Button_off()
    {
        for (int i = 0; i < SkillBackGround.Length; i++)
        {
            SkillBackGround[i].GetComponent<Image>().enabled = false;
            SkillIcon[i].GetComponent<Image>().enabled = false;
            SkillIcon[i].GetComponent<Button>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warp"))
        {
            StartCoroutine(LoadandActive());
            revivePoint = other.GetComponent<warpController>().warpPos;
            anim.SetBool("IsMove", false);
        }

        if (other.CompareTag("enemyHitBox"))
        {
            enemyDamage = other.GetComponentInParent<enemyController>().Damage;

            if (!isDefence)
            {
                GameObject go = Instantiate(Damagetxt, transform.position, Quaternion.identity);
                anim.SetTrigger("IsHit");
                HitEffect.transform.position = other.transform.position;
                StartCoroutine(HitEffectOn());
                GameManager.Instance.currentHp -= enemyDamage;
                go.GetComponent<DamageText>().Damage = enemyDamage;
            }
            if (isDefence)
            {
                GameObject go = Instantiate(Damagetxt, transform.position, Quaternion.identity);
                anim.SetTrigger("IsDefenceHit");
                StartCoroutine(DefenceEffectOn());
                if (enemyDamage > GameManager.Instance.def)
                {
                    GameManager.Instance.currentHp -= enemyDamage - (int)GameManager.Instance.def;
                    go.GetComponent<DamageText>().Damage = enemyDamage - (int)GameManager.Instance.def;
                }
                else if (enemyDamage <= GameManager.Instance.def)
                {
                    GameManager.Instance.currentHp -= 1;
                    go.GetComponent<DamageText>().Damage = 1;
                }
                currentTime = 0;
                isDefence = false;
                anim.SetBool("IsDefence", isDefence);
            }
            if (GameManager.Instance.currentHp <= 0)
            {
                isDie = true;
                P_STATE = creature_STATE.Death;
                anim.SetTrigger("Die");
                StartCoroutine(reviveWindow());
            }
        }
    }

    IEnumerator DefenceEffectOn()
    {
        DefenceEffect.SetActive(true);
        SoundManager.instance.PlayEffects(soundName[1]);
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("IsDefenceHit");
        DefenceEffect.SetActive(false);
    }
    IEnumerator HitEffectOn()
    {
        HitEffect.SetActive(true);
        SoundManager.instance.PlayEffects(soundName[0]);
        yield return new WaitForSeconds(1f);
        anim.ResetTrigger("IsHit");
        HitEffect.SetActive(false);
    }
    IEnumerator reviveWindow()
    {
        SoundManager.instance.PlayEffects(soundName[3]);
        yield return new WaitForSeconds(2f);
        revivePlayer.SetActive(true);
    }
    IEnumerator noHaveSave()
    {
        noHaveSavePoint.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        noHaveSavePoint.SetActive(false);
    }
    IEnumerator ClearBossUI()
    {
        ClearBossWindow.SetActive(true);
        ClearBoss = false;
        yield return new WaitForSeconds(2f);
        ClearBossWindow.SetActive(false);
    }
    IEnumerator LoadandActive()
    {
        JoyController.StartPos = Vector3.zero;
        JoyController.isDrag = false;
        JoyController.JoyBG.SetActive(false);
        Canvas.SetActive(false);
        rgd.isKinematic = true;
        yield return null;
    }
}