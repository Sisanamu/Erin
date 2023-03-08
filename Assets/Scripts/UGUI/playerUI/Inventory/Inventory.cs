
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField]
    public GameObject slotParent;

    public Slot[] slots;

    public Slot[] Getslots() { return slots; }

    [SerializeField] private Item[] items;

    public void LoadtoInven(int _arrayNum, string _itemName, int _itemCount, int _reinForce)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].ItemName == _itemName)
                slots[_arrayNum].AddItem(items[i], _itemCount, _reinForce);
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        slotParent.SetActive(true);
        slotParent.SetActive(false);
    }
    // private void Update()
    // {
    //     for (int i = 0; i < slots.Length; i++)
    //     {
    //         if (slots[i].SlotName == "Sword")
    //         {
    //             if (WeaPonManager.instance.RMfilter != null)
    //             {
    //                 if (slots[i].item != null)
    //                 {
    //                     WeaPonManager.instance.changesword(slots[i].item, slots[i].Reinforce);
    //                     WeaPonManager.instance.RMfilter.mesh = slots[i].item.weaponobject;
    //                     WeaPonManager.instance.SwordreinForceEffectOn(slots[i].Reinforce);
    //                     GameManager.Instance.WeaponDamage = slots[i].item.WeaponDamage * (1 + (0.2f * slots[i].Reinforce));
    //                 }
    //                 else
    //                 {
    //                     WeaPonManager.instance.unEquipSword(slots[i].item, 0);
    //                     WeaPonManager.instance.SwordreinForceEffectOn(slots[i].Reinforce);
    //                 }
    //             }
    //         }
    //         if (slots[i].SlotName == "Shield")
    //         {
    //             if (WeaPonManager.instance.LMfilter != null)
    //             {
    //                 if (slots[i].item != null)
    //                 {
    //                     WeaPonManager.instance.changeshield(slots[i].item, slots[i].Reinforce);
    //                     WeaPonManager.instance.LMfilter.mesh = slots[i].item.weaponobject;
    //                     WeaPonManager.instance.ShieldreinForceEffectOn(slots[i].Reinforce);
    //                     GameManager.Instance.def = slots[i].item.def * (1 + ((int)0.2f * slots[i].Reinforce));
    //                 }
    //                 else
    //                 {
    //                     WeaPonManager.instance.unEquipShield(slots[i].item, 0);
    //                     WeaPonManager.instance.ShieldreinForceEffectOn(slots[i].Reinforce);
    //                 }

    //             }

    //         }
    //     }
    // }
    public void AcquireItem(Item _item, int _Count = 1, int _Reinforce = 0)
    {
        if (ItemType.Equip != _item.itemtype)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.ItemName == _item.ItemName)
                    {
                        slots[i].SetSlotCount(_Count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (ItemType.Equip != _item.itemtype)
                {
                    slots[i].AddItem(_item, _Count, 0);
                }
                else
                {
                    slots[i].AddItem(_item, _Count, _Reinforce);
                }
                return;
            }
        }
    }
}