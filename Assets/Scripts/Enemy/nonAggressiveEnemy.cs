using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nonAggressiveEnemy : enemyController
{
    private void LastAttack()
    {
        if (target != null && (transform.position - target.transform.position).magnitude > AttackRange)
        {
            isBattle = true;
            anim.SetBool("IsBattle", isBattle);
            Monster_Name.GetComponent<Text>().color = Color.green;
            Rotate(target.transform.position);
        }
    }

    public override void Idle()
    {
        base.Idle();
    }
    public override void Attack()
    {
        base.Attack();
        if (!isFirst)
        {
            LastAttack();
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}