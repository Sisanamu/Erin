using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class beforReinforceSlot : Slot
{
    [SerializeField] private Text ItemInfo;

    private void Update()
    {
        if (item != null)
        {
            ItemInfo.enabled = true;
            ItemInfo.text = $"{item.ItemName}[+{Reinforce}]";
        }
        else
        {
            ItemInfo.enabled = false;
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Inventory.instance.AcquireItem(item, itemCount, Reinforce);
        ClearSlot();
    }
}
