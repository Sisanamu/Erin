using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuff : MonoBehaviour
{
    public float duration;
    public float currentTime;
    public Image Icon;
    public Status_Effect status_Effect;

    private void Awake()
    {
        Icon = GetComponent<Image>();
    }
    public void Init(Status_Effect type, float du)
    {
        this.status_Effect = type;
        duration = du;
        currentTime = duration;
        Icon.fillAmount = 1f;
        Execute();
    }
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    public void Execute()
    {
        StartCoroutine(Activation());
    }
    IEnumerator Activation()
    {
        while (currentTime > 0)
        {
            currentTime -= 0.1f;
            Icon.fillAmount = currentTime / duration;
            yield return seconds;
        }
        Icon.fillAmount = 0;
        currentTime = 0;

        DeActivation();
    }
    public void DeActivation()
    {
        Destroy(gameObject);
    }
}