using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Hp : MonoBehaviour
{
    public enemyController Enemy;
    Image Hp;
    float ratio;

    void Start()
    {
        Hp = GetComponent<Image>();
    }

    void Update()
    {
        ratio = Enemy.GetComponent<enemyController>().currentHP / Enemy.GetComponent<enemyController>().MaxHp;
        Hp.fillAmount = Mathf.Lerp(Hp.fillAmount, ratio, 0.5f);
    }
}