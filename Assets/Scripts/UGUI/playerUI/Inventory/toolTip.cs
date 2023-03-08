using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toolTip : MonoBehaviour
{
    [SerializeField] GameObject go_Base;
    [SerializeField] Text itemName;
    [SerializeField] Text itemCategory;
    [SerializeField] Text itemTooltip;

    public void activeToolTip(Item _item, Vector3 _Pos, int _Reinfocre)
    {
        go_Base.SetActive(true);
        _Pos += new Vector3( -go_Base.GetComponent<RectTransform>().rect.width*0.6f
        ,-go_Base.GetComponent<RectTransform>().rect.height*0.5f, 0f);
        go_Base.transform.position = _Pos;
        itemName.text = _item.ItemName;
        
        if(_item.itemtype == ItemType.Equip)
        {
            itemCategory.text = "[장비]";
            itemCategory.color = Color.red;
            if(_item.def == 0)
            {
                itemTooltip.text = $"{_item.part}을(를) {(_item.WeaponDamage*(1 + (0.2f * _Reinfocre)))}만큼 상승";
            }
            else if(_item.WeaponDamage == 0)
            {
                itemTooltip.text = $"{_item.part}을(를) {_item.def * (1 + (0.2f * _Reinfocre))}만큼 상승";
            }
        }   
        else if(_item.itemtype == ItemType.Used)
        {
            itemCategory.text = "[포션]";
            itemCategory.color = Color.green;
            itemTooltip.text = $"{_item.part}을(를) {_item.recovery}만큼 회복";
        }
        else if(_item.itemtype == ItemType.ETC)
        {
            itemCategory.text = "잡동사니";
            itemCategory.color = Color.black;
        }
    }
    public void deactiveTooltip()
    {
        go_Base.SetActive(false);
    }
}