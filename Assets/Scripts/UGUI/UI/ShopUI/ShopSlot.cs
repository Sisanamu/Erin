using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    public Text itemText;
    public Text imtePrice;
    private ItemEffectDatabase IED;
    [SerializeField] protected GameObject lowGold;
    [SerializeField] protected string[] soundName;
    private void Start()
    {
        IED = FindObjectOfType<ItemEffectDatabase>();
    }
    private void Update()
    {
        ListItem(item);
    }
    public void ListItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.ItemImage;
        itemText.text = item.ItemName.ToString();
        imtePrice.text = item.priceItem.ToString();
    }
    public void buyItem()
    {
        if(GameManager.Instance.Gold >=item.priceItem)
        {
            Inventory.instance.AcquireItem(item);
            GameManager.Instance.Gold -= item.priceItem;
            SoundManager.instance.PlayEffects(soundName[0]);
        }
        else if(GameManager.Instance.Gold<item.priceItem)
        {
            StartCoroutine(lowgoldWindow());
            SoundManager.instance.PlayEffects(soundName[1]);
        }
    }
    IEnumerator lowgoldWindow()
    {
        lowGold.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        lowGold.SetActive(false);
    }
}