using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    public GameObject cam;
    public GameObject Player;
    public Animator anim;
    public bool isDrag;

    Image Blank = null;
    RectTransform rectTransforn;
    public Vector3 StartPos = Vector3.zero;
    Vector3 direction = Vector3.zero;

    public GameObject JoyBG;
    public Image Joy;

    private void Start()
    {
        isDrag = false;
        rectTransforn = JoyBG.GetComponent<RectTransform>();
        Blank = GetComponent<Image>();
    }

    public void OnpointerDown(BaseEventData baseEventData)
    {
        JoyBG.gameObject.SetActive(true);
        JoyBG.GetComponent<RectTransform>().position = Input.mousePosition;
        Joy.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnPointerUp(BaseEventData baseEventData)
    {
        isDrag = false;
        JoyBG.gameObject.SetActive(false);
        StartPos = Vector3.zero;
        Joy.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(BaseEventData baseEventData)
    {
        isDrag = true;
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (Joy.rectTransform, pointerEventData.position, pointerEventData.pressEventCamera, out Vector2 localPoint))
        {
            StartPos.x = (localPoint.x / Joy.rectTransform.sizeDelta.x);
            StartPos.y = (localPoint.y / Joy.rectTransform.sizeDelta.y);
            StartPos.z = 0f;

            StartPos = (StartPos.magnitude > 1.0f) ? StartPos.normalized : StartPos;
            direction.x = StartPos.x * (Joy.rectTransform.sizeDelta.x / 3.5f);
            direction.y = StartPos.y * (Joy.rectTransform.sizeDelta.y / 3.5f);

            Joy.rectTransform.anchoredPosition = direction;

        }
    }
    private void FixedUpdate()
    {
        if (isDrag)
        {
            var Pos = cam.transform.forward;
            Pos.y = 0;
            Player.transform.LookAt(Player.transform.position + Pos);
            Player.GetComponent<playerController>().isWalk = true;

            Vector3 movement = new Vector3(direction.x / 50, 0, direction.y / 50);
            Player.transform.Translate(movement * GameManager.Instance.P_speed * Time.deltaTime);
            anim.SetBool("IsMove", true);
        }
        else
        {
            Player.GetComponent<playerController>().isWalk = false;
            anim.SetBool("IsMove", false);
        }
        anim.SetFloat("PosX", direction.x);
        anim.SetFloat("PosZ", direction.y);
    }
}