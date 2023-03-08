using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    public static QuickSlot instance;
    public int slotCount;
    [SerializeField]
    private GameObject Quickslot_Parent;

    public Slot_QuickSlot[] quickslots;
    public Slot_QuickSlot[] GetQuick() { return quickslots; }
    [SerializeField] private Item[] items;
    private void Awake()
    {
        instance = this;
    }

    public void LoadtoQuick(int _arrayNum, string _itemName, int _itemCount)
    {
        for(int i=0; i<items.Length; i++)
        {
            if(items[i].ItemName == _itemName)
                quickslots[_arrayNum].AddItem(items[i], _itemCount);
        }
    }

    void Start()
    {
        quickslots = GetComponentsInChildren<Slot_QuickSlot>();
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < quickslots.Length; i++)
        {
            if (_item != null)
            {
                if (ItemType.Used == _item.itemtype)
                {
                    if (quickslots[i].item == null)
                    {

                        quickslots[i].AddItem(_item, _count);
                        return;
                    }
                    else if (quickslots[i].item != null)
                    {
                        if (quickslots[i].item.ItemName == _item.ItemName)
                        {
                            quickslots[i].SetSlotCount(_count);
                            return;
                        }
                    }
                }

            }

        }
    }
}