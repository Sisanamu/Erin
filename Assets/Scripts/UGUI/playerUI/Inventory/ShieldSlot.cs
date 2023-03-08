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
            WeaPonManager.instance.changeshield(item, Reinforce);
            slotImage.SetActive(false);
        }
        else
        {
            slotImage.SetActive(true);
            WeaPonManager.instance.unEquipShield(item, 0);
        }

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Inventory.instance.AcquireItem(item, 1, Reinforce);
            WeaPonManager.instance.unEquipShield(item, 0);
            ClearSlot();
        }
    }
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}