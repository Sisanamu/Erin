using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragSlot : MonoBehaviour
{
    static public dragSlot instance;

    public Slot DragSlot;

    [SerializeField]
    private Image imageItem;
    [SerializeField]
    public int Reinforce;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage, int _Reinforce)
    {
        imageItem.sprite = _itemImage.sprite;
        Reinforce = _Reinforce;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }
}
