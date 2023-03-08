using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Boss
{
    [Header("DragonIdleTime")]
    [SerializeField] float IdleWaitTime;
    [SerializeField] float Idle2WaitTime;
    [SerializeField] float sleepWaitTime;
    [SerializeField] GameObject FlameBreath;
    [SerializeField] GameObject Warp;
    bool isIdle = true;

    public override void Idle()
    {
        findTarget();
        if (isIdle)
        {
            int IdleRnd = UnityEngine.Random.Range(0, 3);
            switch (IdleRnd)
            {
                case 0:
                    anim.SetBool("IsIdle", true);
                    anim.SetBool("IsIdle2", false);
                    anim.SetBool("IsSleep", false);
                    isIdle = false;
                    StartCoroutine(SetIdle(IdleWaitTime));
                    break;
                case 1:
                    anim.SetBool("IsIdle2", true);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsSleep", false);
                    isIdle = false;
                    StartCoroutine(SetIdle(Idle2WaitTime));
                    break;
                case 2:
                    anim.SetBool("IsSleep", true);
                    anim.SetBool("IsIdle", false);
                    anim.SetBool("IsIdle2", false);
                    isIdle = false;
                    StartCoroutine(SetIdle(sleepWaitTime));
                    break;
            }
        }
        
    }
    public override void Attack()
    {
        if (target != null && (transform.position - target.transform.position).magnitude > AttackRange)
        {
            isBattle = true;
            anim.SetBool("IsSleep", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsIdle2", false);
            anim.SetBool("IsBattle", isBattle);
        }
        base.Attack();
    }

    protected override void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("playerHitBox"))
        {
            int GetDamage = GameManager.Instance.GetDamage(def);
            anim.ResetTrigger("IsAttack");
            if (CanDefence)
            {
                GameObject go = Instantiate(Damagetxt, transform.position, Quaternion.identity);
                if (isDefence)
                {
                    anim.ResetTrigger("IsAttack");
                    if (GetDamage > def)
                    {
                        CurrentHp -= Mathf.Abs(GetDamage - def);
                        go.GetComponent<DamageText>().Damage = Mathf.Abs(GetDamage - (int)def);
                    }
                    else if (GetDamage <= def)
                    {
                        CurrentHp -= 1;
                        go.GetComponent<DamageText>().Damage = 1;
                    }
                    isDefence = false;
                    anim.SetBool("IsDefence", false);
                }
                else
                {
                    anim.SetTrigger("IsHit");
                    StartCoroutine(HitEffectOn());
                    go.GetComponent<DamageText>().Damage = GetDamage;
                    CurrentHp -= GetDamage;
                    Defence();
                }

            }
            if (!CanDefence)
            {
                GameObject go = Instantiate(Damagetxt, transform.position, Quaternion.identity);
                anim.SetTrigger("IsHit");
                StartCoroutine(HitEffectOn());
                go.GetComponent<DamageText>().Damage = GetDamage;
                CurrentHp -= GetDamage;
            }
            if (CurrentHp <= 0)
            {
                Instantiate(Warp, transform.position, Quaternion.identity);
                Warp.GetComponent<warpController>().SceneName = "Game";
                Warp.GetComponent<warpController>().warpPos = new Vector3(60, 22.5f, 45);

                if (target.GetComponent<playerController>().quest == null)
                {
                    StartCoroutine(EnemyRevive());
                }
                else
                {
                    questEnemy();
                    StartCoroutine(EnemyRevive());
                }
            }
        }
    }
    protected override void rndAttack()
    {
        int attackRnd = Random.Range(0, 3);
        switch (attackRnd)
        {
            case 0:
                anim.SetTrigger("IsAttack");
                break;
            case 1:
                anim.SetTrigger("IsClaw");
                break;
            case 2:
                anim.SetTrigger("IsFlame");
                break;
        }
    }
    IEnumerator SetIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isIdle = true;
    }
    void flameBreathAttackOn()
    {
        FlameBreath.SetActive(true);
    }
    void flameBreathAttackOff()
    {
        FlameBreath.SetActive(false);
    }
}