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
        base.Attack();
        if (target != null && (target.transform.position - transform.position).magnitude >= rangeAttackRange && !isAttack)
        {
            anim.ResetTrigger("IsFound");
            isWalk = false;
            anim.SetBool("IsWalk", isWalk);
            transform.LookAt(target.transform.position);
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
            SpawnTime = Mathf.Infinity;
        }
    }
}