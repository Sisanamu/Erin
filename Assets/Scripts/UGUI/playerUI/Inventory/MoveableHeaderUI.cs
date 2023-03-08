using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform _target;

    private Vector2 _beginPoint;
    private Vector2 _movePoint;

    private void Awake()
    {
        if (_target == null)
            _target = transform.parent;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _target.position = _beginPoint + (eventData.position - _movePoint);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _beginPoint = _target.position;
        _movePoint = eventData.position;
    }
}