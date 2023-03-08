using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaPonManager : MonoBehaviour
{
    [SerializeField]public MeshFilter RMfilter;
    [SerializeField]public MeshFilter LMfilter;
    public GameObject[] SwordreinforceEffect;
    public GameObject[] ShieldreinforceEffect;

    public static WeaPonManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void changesword(Item sword, int Reinforce)
    {
        RMfilter.mesh = sword.weaponobject;
        GameManager.Instance.WeaponDamage = sword.WeaponDamage * (1 + (0.2f * Reinforce));
    }
    public void unEquipSword(Item sword, int Reinforce)
    {
        RMfilter.mesh = null;
        Reinforce = 0;
        GameManager.Instance.WeaponDamage = 0;
    }
    public void changeshield(Item shield, int Reinfocre)
    {
        LMfilter.mesh = shield.weaponobject;
        GameManager.Instance.def = shield.def * (1 + ((int)0.2f * Reinfocre));
    }
    public void unEquipShield(Item shield, int Reinforce)
    {
        LMfilter.mesh = null;
        Reinforce = 0;
        GameManager.Instance.def = 0;
    }

    public void SwordreinForceEffectOn(int Reinforce)
    {
        if(Reinforce <= 0)
        {
            SwordreinforceEffect[0].SetActive(false);
            SwordreinforceEffect[1].SetActive(false);
            SwordreinforceEffect[2].SetActive(false);
        }

        if (Reinforce > 0)
        {
            SwordreinforceEffect[0].SetActive(true);
        }
        if (Reinforce > 5)
        {
            SwordreinforceEffect[1].SetActive(true);
        }
        if (Reinforce > 9)
        {
            SwordreinforceEffect[2].SetActive(true);
        }
    }
    
    public void ShieldreinForceEffectOn(int Reinforce)
    {
        if(Reinforce <= 0)
        {
            ShieldreinforceEffect[0].SetActive(false);
            ShieldreinforceEffect[1].SetActive(false);
            ShieldreinforceEffect[2].SetActive(false);
        }

        if (Reinforce > 0)
        {
            ShieldreinforceEffect[0].SetActive(true);
        }
        if (Reinforce > 5)
        {
            ShieldreinforceEffect[1].SetActive(true);
        }
        if (Reinforce > 9)
        {
            ShieldreinforceEffect[2].SetActive(true);
        }
    }
}