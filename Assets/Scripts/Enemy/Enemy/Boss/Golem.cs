using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Golem : Boss
{
    [SerializeField] protected GameObject Rock;
    [SerializeField] protected float rangeAttackRange;
    [SerializeField] protected GameObject ThrowHand;

    public override void Attack()
    {
        if(target == null)
        {
            E_STATE = creature_STATE.IDLE;
        }
        if (target != null && (transform.position - target.transform.position).magnitude > FindRange+1f)
        {
            isBattle = false;
            isAttack = false;
            anim.SetBool("IsBattle", isBattle);
            anim.ResetTrigger("IsThrowAttack");
            Monster_Name.GetComponent<Text>().color = Color.white;
            target = null;
        }
        if (target != null && (target.transform.position - transform.position).magnitude <= FindRange
            &&  (target.transform.position - transform.position).magnitude >= rangeAttackRange && !isAttack)
        {
            anim.ResetTrigger("IsFound");
            isWalk = false;
            anim.SetBool("IsWalk", isWalk);
            Rotate(target.transform.position);
            isBattle = true;
            anim.SetBool("IsBattle", isBattle);
            Monster_Name.GetComponent<Text>().color = Color.red;
            isAttack = true;
            if (isAttack)
            {
                anim.SetTrigger("IsThrowAttack");
            }

        }
        if (target != null &&
        (target.transform.position - transform.position).magnitude < rangeAttackRange &&
        (target.transform.position - transform.position).magnitude > AttackRange)
        {
            anim.ResetTrigger("IsThrowAttack");
            isAttack = false;
            isWalk = true;
            Rotate(target.transform.position);
            Move();
            isBattle = false;
            anim.SetBool("IsBattle", false);
        }
        if (target != null &&
                    (transform.position - target.transform.position).magnitude < AttackRange
                    && isAttack == false && !isDie)
        {
            isBattle = true;
            isWalk = false;
            anim.ResetTrigger("IsThrowAttack");
            anim.SetBool("IsBattle", isBattle);
            anim.SetBool("IsWalk", false);
            Monster_Name.GetComponent<Text>().color = Color.red;
            anim.SetBool("IsWalk", false);
            transform.LookAt(target.transform.position);
            isAttack = true;
            rndAttack();
        }
        if (target != null && target.GetComponent<playerController>().isDie)
        {
            isBattle = false;
            CanSearch = false;
            E_STATE = creature_STATE.IDLE;
            anim.SetBool("IsBattle", isBattle);
        }
    }
    protected override void rndAttack()
    {
        if (isAttack)
        {
            anim.SetTrigger("IsAttack");
        }
    }
    public void throwRock()
    {
        Instantiate(Rock, ThrowHand.transform.position, Quaternion.identity, transform);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (CurrentHp <= 0)
        {
            target.GetComponent<playerController>().ClearBoss = true;
            SpawnTime = Mathf.Infinity;
        }
    }

}