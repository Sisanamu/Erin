using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status_Effect : MonoBehaviour
{
    public static Enemy_Status_Effect instance;
    public GameObject BuffPrefabs;
    public GameObject go;

    private void Start()
    {
        instance = this;
    }
    public void CreateDeBuff(Status_Effect type, float du, Sprite Icon)
    {
        go = Instantiate(BuffPrefabs, transform);
        go.GetComponent<BaseBuff>().Init(type, du);
        go.GetComponent<UnityEngine.UI.Image>().sprite = Icon;
    }
}