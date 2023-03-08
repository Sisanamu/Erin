using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamRotate : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public LayerMask notPlayer;
    public Transform CamPivot;
    public Transform Cam;
    public Transform Player;
    public float RotateSpeed = 1f;

    Vector3 beginPos;
    Vector3 draggingPos;
    public float xAngle, yAngle, xAngleTemp, yAngleTemp;

    private void Start()
    {
        xAngle = CamPivot.rotation.eulerAngles.x;
        yAngle = CamPivot.rotation.eulerAngles.y;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        beginPos = eventData.position;

        xAngleTemp = xAngle;
        yAngleTemp = yAngle;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggingPos = eventData.position;

        yAngle = yAngleTemp + (draggingPos.x - beginPos.x) * Screen.height / 1920 * RotateSpeed * Time.deltaTime;
        xAngle = xAngleTemp - (draggingPos.y - beginPos.y) * Screen.width / 1080 * RotateSpeed * Time.deltaTime;

        if (xAngle > 30) xAngle = 30;
        if (xAngle < -10) xAngle = -10;

    }
    private void FixedUpdate()
    { 
        CamPivot.position = Player.position;
        CamPivot.rotation = Quaternion.Euler(xAngle, yAngle, 0);
    }
}
