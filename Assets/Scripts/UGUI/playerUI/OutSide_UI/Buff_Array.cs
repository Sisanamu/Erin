using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Array : MonoBehaviour
{
    public static Buff_Array instance;
    public GameObject BuffPrefabs;
    private void Awake()
    {
        instance = this;
    }

    public void CreateBuff(Status_Effect type, float du, Sprite Icon)
    {
        GameObject go = Instantiate(BuffPrefabs, transform);
        go.GetComponent<BaseBuff>().Init(type, du);
        go.GetComponent<UnityEngine.UI.Image>().sprite = Icon;
    }
}