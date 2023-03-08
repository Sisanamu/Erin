using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit_Button : MonoBehaviour
{
    public GameObject Window;
    Button ExitButton;

    void Start()
    {
        ExitButton = GetComponent<Button>();
        ExitButton.onClick.AddListener(ExitWindow);
    }
    void ExitWindow()
    {
        Window.SetActive(false);
    }
}