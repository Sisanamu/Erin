using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aggressiveEnemy : enemyController
{
    protected override void Update()
    {
        base.Update();
        AimUi();
    }
    public void FirstAttack()
    {
        if (target != null && (transform.position - target.transform.position).magnitude > AttackRange)
        {
            isWalk = true;
            isBattle = true;
            anim.ResetTrigger("IsFound");
            anim.SetBool("IsBattle", isBattle);
            Monster_Name.GetComponent<Text>().color = Color.red;
            Rotate(target.transform.position);
            Move();
        }
    }
    public override void Attack()
    {
        base.Attack();
        if (isFirst)
        {
            FirstAttack();
        }
    }
    public override void Idle()
    {
        base.Idle();
    }
    protected override void rndAttack()
    {
        if (isAttack)
        {
            int Rnd = UnityEngine.Random.Range(0, 11);
            if (Rnd <= 7)
                anim.SetTrigger("IsAttack");
            else if (Rnd > 7 && Rnd < 11)
            {
                StartCoroutine(StrongAttack());
            }
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}