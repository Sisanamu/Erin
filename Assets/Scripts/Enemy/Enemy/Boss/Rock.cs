using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected Golem Golem;
    [SerializeField] protected float R_Speed;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected float Damage;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
        Golem = GetComponentInParent<Golem>();
        target = Golem.target;
        Damage = Golem.Damage;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 _dir = (target.transform.position - transform.position).normalized;
            transform.position += _dir * R_Speed * Time.deltaTime;
            transform.Rotate(new Vector3(50, 0, 0) * 40 * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}