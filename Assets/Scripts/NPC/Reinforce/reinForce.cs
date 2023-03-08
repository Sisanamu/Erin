using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reinForce : MonoBehaviour
{
    [SerializeField] protected Image Wait;
    float time = 0f;
    float F_time = 1f;

    [SerializeField] private GameObject ReinforceWindow;
    [SerializeField] private Text Goldtext;
    [SerializeField] private Text Percentagetext;
    int Gold;
    int Percentage;
    Button reinForceButton;
    [SerializeField] private GameObject lowGold;
    [SerializeField] private GameObject WaitRein;
    [SerializeField] protected Slot beforeSlot;
    [SerializeField] protected Slot afterSlot;
    [SerializeField] protected Text SuccessOrFailure;
    [SerializeField] protected GameObject SuccessOrFailureWindow;

    private void Start()
    {
        reinForceButton = GetComponent<Button>();
        reinForceButton.onClick.AddListener(reinForceEquipment);
    }

    void Update()
    {
        if (beforeSlot.item != null)
        {
            reinForceButton.enabled = true;
            if (afterSlot.item != null)
            {
                Inventory.instance.AcquireItem(afterSlot.item, afterSlot.itemCount, afterSlot.Reinforce);
                afterSlot.ClearSlot();
            }
            else
            {
                Percentage = (100 - (3 * beforeSlot.Reinforce));
                Goldtext.enabled = true;
                Percentagetext.enabled = true;
                Goldtext.text = $"소모 골드 : {reinForceGold()}";
                Percentagetext.text = $"{Percentage}%";
            }
        }
        else
        {
            Goldtext.enabled = false;
            Percentagetext.enabled = false;
            reinForceButton.enabled = false;
        }
    }

    void reinForceEquipment()
    {
        if (reinForceGold() < GameManager.Instance.Gold)
        {
            StartCoroutine(FadeIn());
            GameManager.Instance.Gold -= reinForceGold();
            int Rnd = Random.Range(0, 99);
            if (Rnd < Percentage)
            {
                beforeSlot.Reinforce += 1;
                afterSlot.AddItem(beforeSlot.item, 1, beforeSlot.Reinforce);
                beforeSlot.ClearSlot();
                SuccessOrFailure.text = "강화에 성공하였습니다.";
            }
            else
            {
                SuccessOrFailure.text = "강화에 실패하였습니다.";
            }
        }
        else
        {
            StartCoroutine(lowgoldWindow());
        }
    }


    int reinForceGold()
    {
        Gold = 10 * (int)Mathf.Pow(2, beforeSlot.Reinforce);
        return Gold;
    }
    IEnumerator lowgoldWindow()
    {
        lowGold.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        lowGold.SetActive(false);
    }
    IEnumerator successorFailureWindows()
    {
        SuccessOrFailureWindow.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        SuccessOrFailureWindow.SetActive(false);
    }
    IEnumerator FadeIn()
    {
        Color color = Wait.color;
        time = 0f;
        color.a = 1f;
        while (color.a > 0f)
        {
            time += Time.deltaTime / F_time;
            color.a = Mathf.Lerp(1, 0, time);
            Wait.color = color;
            yield return null;
        }
        StartCoroutine(successorFailureWindows());
    }
    public void exitReinForce()
    {
        if (beforeSlot.item != null)
        {
            Inventory.instance.AcquireItem(beforeSlot.item, beforeSlot.itemCount, beforeSlot.Reinforce);
            beforeSlot.ClearSlot();
        }
        if (afterSlot.item != null)
        {
            Inventory.instance.AcquireItem(afterSlot.item, afterSlot.itemCount, afterSlot.Reinforce);
            afterSlot.ClearSlot();
        }

        ReinforceWindow.SetActive(false);
        lowGold.SetActive(false);
    }
}