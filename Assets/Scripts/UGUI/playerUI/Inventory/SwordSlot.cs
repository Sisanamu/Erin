using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwordSlot : Slot, IPointerClickHandler
{
    public static SwordSlot instance;
    public GameObject slotImage;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (item != null)
        {
            slotImage.SetActive(false);
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
            if (WeaPonManager.instance.RMfilter != null)
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