using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public string SlotName;
    public Item item; // 획득한 아이탬
    public int itemCount; // 획득한 아이템의 개수
    public int Reinforce;

    public ItemEffectDatabase IED;
    private toolTip tool;
    public Image itemImage;
    public GameObject noHaveSpace;

    [SerializeField]
    private Text Text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField] Text reinForceText;
   
    public void Start()
    {
        IED = FindAnyObjectByType<ItemEffectDatabase>();
    }
    protected void OnEnable()
    {
        if (item != null)
        {
            itemImage.sprite = item.ItemImage;
            SetColor(1);
        }
    }
    protected void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // 아이템 획득
    public void AddItem(Item _item, int _Count = 1, int _Reinforce = 1)
    {
        item = _item;
        itemCount = _Count;
        itemImage.sprite = item.ItemImage;
        Reinforce = _Reinforce;

        if (item.itemtype != ItemType.Equip)
        {
            Text_Count.text = itemCount.ToString();
            Reinforce = 0;
            go_CountImage.SetActive(true);
        }
        else
        {
            itemCount = _Count;
            Text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);

    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        Text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        Reinforce = 0;
        SetColor(0);

        Text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (item.itemtype == ItemType.Used)
            {
                QuickSlot.instance.slotCount++;
                for (int i = 0; i < QuickSlot.instance.quickslots.Length; i++)
                {
                    if (QuickSlot.instance.quickslots[i].item == null)
                    {
                        QuickSlot.instance.AcquireItem(item, itemCount);
                        SetSlotCount(-itemCount);
                    }
                    if (QuickSlot.instance.slotCount > QuickSlot.instance.quickslots.Length)
                    {
                        for (int j = 0; j < QuickSlot.instance.quickslots.Length; j++)
                        {
                            if (item != null)
                            {
                                if (QuickSlot.instance.quickslots[j].item.ItemName == item.ItemName)
                                {
                                    QuickSlot.instance.slotCount = QuickSlot.instance.quickslots.Length;
                                    QuickSlot.instance.AcquireItem(item, itemCount);
                                    SetSlotCount(-itemCount);
                                }
                            }
                        }
                    }
                }
            }
            else if (item.itemtype == ItemType.Equip)
            {
                if (item.WeaponDamage != 0)
                {
                    if (SwordSlot.instance.item == null)
                    {
                        SwordSlot.instance.AddItem(item, 1, Reinforce);
                        ClearSlot();
                    }
                    else
                    {
                        Item tempItem = SwordSlot.instance.item;
                        int tempReinforce = SwordSlot.instance.Reinforce;
                        SwordSlot.instance.AddItem(item, 1, Reinforce);
                        Inventory.instance.AcquireItem(tempItem, 1, tempReinforce);
                        ClearSlot();
                    }
                }
                else if (item.def != 0)
                {
                    if (ShieldSlot.instance.item == null)
                    {
                        ShieldSlot.instance.AddItem(item, 1, Reinforce);
                        ClearSlot();
                    }
                    else
                    {
                        Item tempItem = ShieldSlot.instance.item;
                        int tempReinforce = ShieldSlot.instance.Reinforce;
                        ShieldSlot.instance.AddItem(item, 1, Reinforce);
                        Inventory.instance.AcquireItem(tempItem, 1, tempReinforce);
                        ClearSlot();
                    }
                }
            }
        }
    }

    IEnumerator noHaveSpaceUI()
    {
        noHaveSpace.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        noHaveSpace.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (item.itemtype == ItemType.Equip)
            {
                IED.activeToolTip(item, transform.position, Reinforce);
                reinForceText.enabled = true;
                reinForceText.text = $"[+ {Reinforce}]";
            }
            else
            {
                IED.activeToolTip(item, transform.position, Reinforce);
                reinForceText.enabled = false;
            }
            dragSlot.instance.DragSlot = this;
            dragSlot.instance.DragSetImage(itemImage, Reinforce);
            dragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            dragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragSlot.instance.DragSlot = null;
        dragSlot.instance.SetColor(0);
        dragSlot.instance.Reinforce = 0;
        IED.deactiveTooltip();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (dragSlot.instance.DragSlot != null && SlotName != "After")
            ChangeSlot();
    }
    public void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        int tempReinforce = Reinforce;

        AddItem(dragSlot.instance.DragSlot.item,
        dragSlot.instance.DragSlot.itemCount, dragSlot.instance.DragSlot.Reinforce);

        if (tempItem != null)
        {
            dragSlot.instance.DragSlot.AddItem(tempItem, tempItemCount, tempReinforce);
        }
        else
            dragSlot.instance.DragSlot.ClearSlot();
    }
}