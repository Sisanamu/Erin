using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    Vector3 point;
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected string npcName;
    [SerializeField] protected GameObject nameUI;
    [SerializeField] protected GameObject Save;
    [SerializeField] protected string meetSavePoint;

    void Start()
    {
        point = transform.position;
        nameUI.GetComponent<Text>().text = npcName;
    }

    void Update()
    {
        UIaim();
        transform.Rotate(new Vector3(0, 10, 0) * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.currentHp = GameManager.Instance.MaxHp;
            GameManager.Instance.currentSp = GameManager.Instance.MaxSp;
            SoundManager.instance.PlayEffects(meetSavePoint);
            other.GetComponent<playerController>().Chasetarget = false;
            other.GetComponent<playerController>().revivePoint = new Vector3(point.x + 1f, point.y, point.z + 1f);
            StartCoroutine(saveUI());
        }
    }
    void UIaim()
    {
        nameUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
    }
    IEnumerator saveUI()
    {
        Save.SetActive(true);
        yield return new WaitForSeconds(GameManager.Instance.UIWaitTime);
        Save.SetActive(false);
    }
}