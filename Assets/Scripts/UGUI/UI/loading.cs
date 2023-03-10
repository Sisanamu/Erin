using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class loading : MonoBehaviour
{
    Image LoadingBar;
    public TextMeshProUGUI loadingText;

    private void Start() {
        LoadingBar = GetComponent<Image>();
    }
    void Update()
    {
        loadingText.text = $"{(LoadingBar.fillAmount * 100).ToString("F1")} %";
    }
}
