using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUP : MonoBehaviour
{
    public static PickUP instance;
    public GameObject PickUpItem;

    private void Awake()
    {
        instance = this;
    }
}