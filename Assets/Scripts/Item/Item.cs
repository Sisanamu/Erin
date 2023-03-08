using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public ItemType itemtype;
    public Sprite ItemImage;
    public int priceItem;
    public float itemCoolTime;
    public Mesh weaponobject;
    public float WeaponDamage;
    public int def;
    public string part;
    public int recovery;
}