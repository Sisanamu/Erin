using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshPro Damagetxt;
    public int Damage;

    void Start()
    {
        Damagetxt = GetComponent<TextMeshPro>();
        Damagetxt.text = Damage.ToString();
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);   
    }
    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50, 50),Random.Range(200, 400),Random.Range(-100, -300)));
    }
}