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
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        //transform.rotation = Quaternion.identity;
    }
    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(50, 350, 0);
        StartCoroutine(textActiveFalse());
    }
    IEnumerator textActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}