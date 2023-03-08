using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public static ItemPickUp instance;
    public Item item;

    public Image ItemImage;
    public Text ItemName;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (item != null)
        {
            ItemImage.sprite = item.ItemImage;
            ItemName.text = item.ItemName.ToString();
        }
        else if (item == null)
        {
            PickUP.instance.GetComponent<PickUP>().gameObject.SetActive(false);
        }
    }
}