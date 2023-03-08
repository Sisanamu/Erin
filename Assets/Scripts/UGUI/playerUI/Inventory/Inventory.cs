
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