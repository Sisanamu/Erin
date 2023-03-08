using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템의 이름
    public string[] part;
    public int[] num; //회복 수치
}
[System.Serializable]
public class WeaponEquip
{
    public string WeaponName;
    public string[] parts;
}
[System.Serializable]
public class ETCITem
{
    public string ETCName;
    public string[] parts;
}

public class ItemEffectDatabase : MonoBehaviour
{

    [SerializeField]
    private WeaponEquip[] weaponEquips;
    [SerializeField]
    private ItemEffect[] itemEffect;
    [SerializeField]
    private ETCITem[] ETCITems;
    [SerializeField]
    private toolTip theTooltip;

    #region  SingleTon
    private ItemEffectDatabase instance = null;
    public ItemEffectDatabase Instance
    {
        get
        {
            if (instance == null)
                instance = null;
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    private const string HP = "HP", SP = "SP", Sword = "Sword", Shield = "Shield", Reinforce = "ReinForce";
    
    public void UseItem(Item _item)
    {
        if (_item.itemtype == ItemType.Used)
        {
            for (int i = 0; i < itemEffect.Length; i++)
            {
                if (itemEffect[i].itemName == _item.ItemName)
                {
                    for (int j = 0; j < itemEffect[i].part.Length; j++)
                    {
                        switch (itemEffect[i].part[j])
                        {
                            case HP:
                                GameManager.Instance.IncreaseHP(itemEffect[i].num[j]);
                                break;
                            case SP:
                                GameManager.Instance.IncreaseSP(itemEffect[i].num[j]);
                                break;
                        }
                    }
                    return;
                }
            }
        }
        else if(_item.itemtype == ItemType.ETC)
        {
            for(int i=0; i<ETCITems.Length; i++)
            {
                if(ETCITems[i].ETCName == _item.ItemName)
                {
                    for(int j=0; j<ETCITems[i].parts.Length; j++)
                    {
                        switch(ETCITems[i].parts[j])
                        {
                            case Reinforce:
                                
                            break;
                        }
                    }
                }
            }
        }
    }
    public void activeToolTip(Item _item, Vector3 _Pos, int _Reinfocre)
    {
        theTooltip.activeToolTip(_item, _Pos, _Reinfocre);
    }
    public void deactiveTooltip()
    {
        theTooltip.deactiveTooltip();
    }
}