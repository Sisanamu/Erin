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
            WeaPonManager.instance.changesword(item, Reinforce);
            slotImage.SetActive(false);
        }
        else
        {
            slotImage.SetActive(true);
            WeaPonManager.instance.unEquipSword(item, 0);
        }

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Inventory.instance.AcquireItem(item, 1, Reinforce);
            WeaPonManager.instance.unEquipSword(item, 0);
            ClearSlot();
        }
    }
}