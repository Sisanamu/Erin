using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShieldSlot : Slot
{
    public GameObject slotImage;

    public static ShieldSlot instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (item != null)
        {
            if (WeaPonManager.instance.LMfilter != null)
            {
                slotImage.SetActive(false);
            }
        }
        else
        {
            slotImage.SetActive(true);
        }

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (WeaPonManager.instance.LMfilter != null)
            {
                Inventory.instance.AcquireItem(item, 1, Reinforce);
                ClearSlot();
            }
        }
    }
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}