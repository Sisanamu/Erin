using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status_Button : MonoBehaviour
{
    public GameObject StatusWindow;
    Button StatusButton;
    
    void Start()
    {
        StatusButton = GetComponent<Button>();
        StatusButton.onClick.AddListener(WindowOpen);
    }

    void WindowOpen()
    {
        StatusWindow.SetActive(!StatusWindow.activeSelf);
    }
}