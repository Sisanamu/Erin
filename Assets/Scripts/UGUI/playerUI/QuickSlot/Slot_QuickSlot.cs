using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_QuickSlot : MonoBehaviour
{
    public Item item; // 획득한 아이탬
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;
    public float itemCool;
    private ItemEffectDatabase theitemEffectDatabase;

    // 필요한 컴포넌트
    [SerializeField]
    private Text Text_Count;
    [SerializeField]
    private GameObject go_CountImage;


    public Image coolTimeImage;
    public float coolTime;
    public bool canUseItem;
    public int slotIndex;
    float ratio;

    private void Start()
    {
        theitemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
        coolTimeImage.enabled = false;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        if (coolTime <= 0)
        {
            coolTime = 0;
            coolTimeImage.enabled = false;
            canUseItem = true;
        }
        ratio = coolTime / itemCool;
        coolTimeImage.GetComponent<Image>().fillAmount = ratio;
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // 아이템 획득
    public void AddItem(Item _item, int _Count = 1)
    {
        item = _item;
        itemCount = _Count;
        itemCool = item.itemCoolTime;
        itemImage.sprite = item.ItemImage;

        if (item.itemtype != ItemType.Equip)
        {
            Text_Count.text = itemCount.ToString();
            go_CountImage.SetActive(true);
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
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        Text_Count.text = "0";
        go_CountImage.SetActive(false);

    }
    public void ClickQuickSlot()
    {
        if (item != null && canUseItem)
        {
            GameManager.Instance.slotIndex = slotIndex;

            itemCoolTime();
            if (item.itemtype == ItemType.Used)
            {
                QuickSlot.instance.slotCount--;
                theitemEffectDatabase.UseItem(item);
            }
        }
    }
    void itemCoolTime()
    {
        coolTimeImage.enabled = true;
        canUseItem = false;
        coolTime = itemCool;
    }
}