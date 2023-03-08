using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chase_Button : MonoBehaviour
{
    public playerController Player;
    public enemyController Enemy;

    private void Update()
    {
        if(Player.Enemy != null)
        {
            Enemy = Player.Enemy.GetComponent<enemyController>();
        }
    }

    public void Chase_On()
    {
        Player.Chasetarget
        = !Player.Chasetarget;
        Player.isBattle
        = !Player.isBattle;
        Player.Rotate(Enemy.gameObject);
    }
}