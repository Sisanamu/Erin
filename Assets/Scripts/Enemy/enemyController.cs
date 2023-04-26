using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected Item[] dropItem;

    [SerializeField] protected Vector3 WaitPos;
    [SerializeField] protected GameObject TargettingImage;
    [SerializeField] protected GameObject Enemy_HP;
    [SerializeField] protected Text Monster_Name;
    [SerializeField] protected GameObject Canvas;
    [SerializeField] protected GameObject HitEffect;

    [SerializeField] protected creature_STATE E_STATE = creature_STATE.IDLE;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rgd;
    [SerializeField] public GameObject target;
    [SerializeField] protected playerController Player;
    [SerializeField] protected GameObject HitBox;
    [SerializeField] protected Transform SpawnPoint;
    [SerializeField] protected Collider[] Target;
    [SerializeField] protected List<GameObject> targets = new List<GameObject>();
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected LayerMask ETCMask;

    [SerializeField] public SkinnedMeshRenderer[] skinRen;
    [SerializeField] protected Collider col;

    [Header("Float Type")]
    [SerializeField] protected float FindRange;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float currentTime;
    [SerializeField] protected float ratio;
    [SerializeField] private float CurrentHp;
    public float currentHP
    {
        get { return CurrentHp; }
        set
        {
            if (CurrentHp >= MaxHp)
                CurrentHp = MaxHp;
        }
    }
    [SerializeField] public float MaxHp;
    [SerializeField] protected float StartPatrolTime;
    [SerializeField] protected float WaitTime;
    [SerializeField] protected float E_Speed;
    [SerializeField] protected float UIWaitTime = 2f;
    [SerializeField] protected float SpawnTime;
    [SerializeField] protected float DeathTime;
    [SerializeField] protected float PatrolRange;
    [Header("Bool Type")]
    [SerializeField] protected bool isAttack = false;
    [SerializeField] protected bool isWalk = false;
    [SerializeField] protected bool isFirst;
    [SerializeField] protected bool isBattle = false;
    [SerializeField] protected bool CanSearch = true;
    [SerializeField] protected bool CanDefence = false;
    [SerializeField] protected bool isDefence = false;
    [SerializeField] public bool isDie = false;

    [Header("DamageText")]
    [SerializeField] protected GameObject Damagetxt;
    public int SpawnCount;
    public List<GameObject> Textlist = new List<GameObject>();
    GameObject TextParent;

    [Header("Status_Effect")]
    [SerializeField] protected GameObject DeBuffEffect;

    [SerializeField] protected int Gold;
    [SerializeField] protected int exp;
    [SerializeField] public float def;
    [SerializeField] public int Damage;


    private void Awake()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            GameObject obj = Instantiate(Damagetxt, transform);
            obj.SetActive(false);
            Textlist.Add(obj);
        }
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        rgd = GetComponent<Rigidbody>();
        skinRen = GetComponentsInChildren<SkinnedMeshRenderer>();
        Monster_Name.text = Name.ToString();
        for (int i = 0; i < SpawnCount; i++)
        {
            GameObject obj = Instantiate(Damagetxt, transform);
            obj.SetActive(false);
            Textlist.Add(obj);
        }
    }

    protected void Start()
    {

        HitEffect.SetActive(false);
        E_STATE = creature_STATE.FIND;
        WaitTime = StartPatrolTime;
        SpawnPoint = transform.parent;
        UIWaitTime = GameManager.Instance.UIWaitTime;
        E_STATE = creature_STATE.IDLE;
    }
    protected virtual void Update()
    {
        UpdateUI();
        switch (E_STATE)
        {
            case creature_STATE.IDLE:
                Idle();
                break;
            case creature_STATE.FIND:
                findTarget();
                break;
            case creature_STATE.ATTACK:
                Attack();
                break;
        }
    }
    public virtual void Attack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            isAttack = false;
            currentTime = 0;
        }
        if (target == null)
        {
            Monster_Name.GetComponent<Text>().color = Color.white;
            E_STATE = creature_STATE.IDLE;
        }
        if (target != null && (transform.position - target.transform.position).magnitude > FindRange + 1f)
        {
            isBattle = false;
            anim.SetBool("IsBattle", isBattle);
            Monster_Name.GetComponent<Text>().color = Color.white;
            targets.Clear();
            target = null;
        }
        if (target != null &&
            (transform.position - target.transform.position).magnitude < AttackRange
            && isAttack == false && !isDie)
        {
            isBattle = true;
            isWalk = false;
            anim.SetBool("IsBattle", isBattle);
            anim.SetBool("IsWalk", false);
            Monster_Name.GetComponent<Text>().color = Color.red;
            anim.SetBool("IsWalk", false);
            transform.LookAt(target.transform.position);
            isAttack = true;
            rndAttack();
        }
        if (target != null && Player.isDie)
        {
            isBattle = false;
            CanSearch = false;
            E_STATE = creature_STATE.IDLE;
            anim.SetBool("IsBattle", isBattle);
        }
    }
    public virtual void Idle()
    {
        if (Canvas.activeSelf)
        {
            Destroy(Enemy_Status_Effect.instance.go);
            Canvas.SetActive(false);
        }
        anim.SetBool("IsBattle", false);
        if (CanDefence)
        {
            anim.SetBool("IsDefence", false);
        }
        findTarget();
        isWalk = false;
        CanSearch = true;
        anim.SetBool("IsWalk", false);
        anim.SetTrigger("IsFound");
        StartCoroutine(WaitPoint());
    }
    protected void findTarget()
    {
        targets.Clear();
        Target = Physics.OverlapSphere(transform.position, FindRange, targetMask);

        for (int i = 0; i < Target.Length; i++)
        {
            Transform targettrans = Target[i].transform;
            Vector3 dirtarget = (targettrans.position - transform.position).normalized;
            targets.Add(targettrans.transform.gameObject);
            if (targets.Count != 0)
            {
                target = targets[0];
                Player = target.GetComponent<playerController>();
                E_STATE = creature_STATE.ATTACK;
            }
        }
        if (targets.Count == 0 || Player.isDie)
        {
            isAttack = false;
            E_STATE = creature_STATE.IDLE;
            Patrol();
        }
    }
    protected virtual void rndAttack()
    {
        if (isAttack)
        {
            isWalk = false;
            int Rnd = UnityEngine.Random.Range(0, 11);
            if (Rnd <= 7)
            {
                anim.SetTrigger("IsAttack");
                SoundManager.instance.PlayEffects("Attack");
            }
            else if (Rnd > 7 && Rnd < 11)
            {
                StartCoroutine(StrongAttack());
            }
        }
    }
    protected void Move()
    {
        if (isWalk)
        {
            anim.ResetTrigger("IsFound");
            anim.SetBool("IsWalk", true);
            transform.Translate(Vector3.forward * E_Speed * Time.deltaTime);
        }
    }
    protected void Rotate(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(dir), E_Speed);
    }
    protected void Patrol()
    {
        E_STATE = creature_STATE.FIND;
        if (WaitTime > 0 && isWalk)
        {
            Move();
            Monster_Name.GetComponent<Text>().color = Color.white;
            WaitTime -= Time.deltaTime;
        }
        else if (WaitTime <= 0)
        {
            E_STATE = creature_STATE.IDLE;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(50, 50, 50, 50);
        Gizmos.DrawSphere(transform.position, FindRange);
    }

   protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("playerHitBox"))
        {
            int GetDamage = GameManager.Instance.GetDamage(def);
            anim.ResetTrigger("IsAttack");
            if (CanDefence)
            {
                if (isDefence)
                {
                    anim.ResetTrigger("IsAttack");
                    anim.ResetTrigger("IsStrongAttack");
                    anim.SetTrigger("DefenceHit");
                    SoundManager.instance.PlayEffects("DefenceHit");
                    if (GetDamage > def)
                    {
                        SpawnDamageText(Mathf.Abs(GetDamage - (int)def), transform.position);
                    }
                    else if (GetDamage <= def)
                    {
                        SpawnDamageText(1, transform.position);
                    }
                    isDefence = false;
                    anim.SetBool("IsDefence", false);
                }
                else
                {
                    anim.SetTrigger("IsHit");
                    SoundManager.instance.PlayEffects("Hit");
                    StartCoroutine(HitEffectOn());
                    SpawnDamageText(GetDamage, transform.position);
                    Defence();
                }

            }
            if (!CanDefence)
            {
                anim.SetTrigger("IsHit");
                StartCoroutine(HitEffectOn());
                SoundManager.instance.PlayEffects("Hit");
                if (GetDamage > def)
                    SpawnDamageText(GetDamage, transform.position);
                else if (GetDamage <= def)
                    SpawnDamageText(1, transform.position);
            }
            if (CurrentHp <= 0)
            {
                Canvas.SetActive(false);
                if (Player.quest == null)
                    StartCoroutine(EnemyRevive());
                else
                {
                    questEnemy();
                    StartCoroutine(EnemyRevive());
                }
            }
        }
    }

    void SpawnDamageText(int GetDamage, Vector3 SpawnPos)
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            if (!Textlist[i].activeSelf)
            {
                Textlist[i].transform.position = SpawnPos;
                Textlist[i].SetActive(true);
                Textlist[i].GetComponent<DamageText>().Damage = GetDamage;
                CurrentHp -= GetDamage;
                return;
            }
        }
    }
    protected virtual void AimUi()
    {
        TargettingImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.5f, 0));
        Enemy_HP.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
    }

    void UpdateUI()
    {
        ratio = CurrentHp / MaxHp;
        Enemy_HP.GetComponent<Image>().fillAmount = ratio;
    }

    void OnHitBox()
    {
        HitBox.SetActive(true);
    }
    void OffHitBox()
    {
        HitBox.SetActive(false);
    }

    public void DeBuffEffect_On(bool Turn_on)
    {
        DeBuffEffect.SetActive(Turn_on);
    }


    public void questEnemy()
    {
        if (Player.quest.Progress && Name == Player.quest.EnemyName)
        {
            Player.quest.questGoal.EnemyKilled();
            if (Player.quest.questGoal.IsReached())
            {
                Player.returnNpc = true;
                Player.quest.isSuccess = true;
            }
        }
    }

    protected IEnumerator EnemyRevive()
    {
        Vector3 StartPos = SpawnPoint.transform.position;
        Vector3 SpawnPos = new Vector3(
            StartPos.x + UnityEngine.Random.Range(-5, 5)
            , StartPos.y,
            StartPos.z + UnityEngine.Random.Range(-5, 5));
        Destroy(Enemy_Status_Effect.instance.go);


        DeBuffEffect.SetActive(false);
        isDie = true;
        rgd.isKinematic = true;
        CanSearch = false;
        isWalk = false;
        col.enabled = false;
        CurrentHp = 0;
        anim.SetBool("IsBattle", false);
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(DeathTime);
        GetReWard();
        ResetTrigger();
        skinRensoff();
        isDie = false;
        target = null;
        transform.position = SpawnPos;
        CurrentHp = MaxHp;
        yield return new WaitForSeconds(SpawnTime);
        E_STATE = creature_STATE.IDLE;
        col.enabled = true;
        rgd.isKinematic = false;
        CanSearch = true;
        skinRenson();
    }
    IEnumerator WaitPoint()
    {
        Canvas.SetActive(false);
        float StopTime = UnityEngine.Random.Range(2f, 3f);
        WaitTime = StartPatrolTime;
        WaitPos = new Vector3(SpawnPoint.position.x + UnityEngine.Random.Range(-PatrolRange, PatrolRange),
            transform.position.y, SpawnPoint.position.z + UnityEngine.Random.Range(-PatrolRange, PatrolRange));
        Rotate(WaitPos);
        yield return new WaitForSeconds(StopTime);
        E_STATE = creature_STATE.FIND;
        isWalk = true;
    }
    void ResetTrigger()
    {
        anim.ResetTrigger("IsHit");
        anim.ResetTrigger("IsAttack");
        anim.ResetTrigger("IsFound");
    }
    void GetReWard()
    {
        GameManager.Instance.IncreaseEXP(exp);
        GameManager.Instance.ChangeGold(Gold);
        DropItem();
    }

    protected IEnumerator HitEffectOn()
    {
        HitEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HitEffect.SetActive(false);
    }

    protected void Defence()
    {
        int Rnd = UnityEngine.Random.Range(0, 2);
        if (Rnd == 0)
        {
            isDefence = true;
            anim.SetBool("IsDefence", true);
        }
        else isDefence = false;
    }
    protected void DropItem()
    {
        int Rnd = UnityEngine.Random.Range(0, 100);
        if (Rnd < 30) StartCoroutine(GetItemUI(dropItem[0]));
        else if (Rnd > 29 && Rnd < 50) StartCoroutine(GetItemUI(dropItem[1]));
        else if (Rnd > 49 && Rnd < 70) StartCoroutine(GetItemUI(dropItem[2]));
        else if (Rnd > 69 && Rnd < 90) StartCoroutine(GetItemUI(dropItem[3]));
        else if (Rnd > 89) PickUP.instance.GetComponent<PickUP>().gameObject.SetActive(false);
    }
    protected IEnumerator GetItemUI(Item _item)
    {
        GameObject go = PickUP.instance.GetComponent<PickUP>().gameObject;
        ItemPickUp.instance.item = _item;
        Inventory.instance.AcquireItem(_item, 1, 0);
        go.SetActive(true);
        yield return new WaitForSeconds(UIWaitTime);
        go.SetActive(false);
    }
    protected IEnumerator StrongAttack()
    {
        int TempDamage = Damage;
        Damage = Damage * (int)1.5f;
        anim.SetTrigger("IsStrongAttack");
        SoundManager.instance.PlayEffects("Attack");
        yield return new WaitForSeconds(attackDelay);
        Damage = TempDamage;
    }

    void skinRensoff()
    {
        for (int i = 0; i < skinRen.Length; i++)
            skinRen[i].enabled = false;
    }
    void skinRenson()
    {
        for (int i = 0; i < skinRen.Length; i++)
            skinRen[i].enabled = true;
    }
}