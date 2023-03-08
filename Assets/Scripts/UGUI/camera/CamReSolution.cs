using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamReSolution : MonoBehaviour
{
    private void Awake()
    {
        SetResolution(1080, 1920);
    }
    void SetCanvasScaler(int _Width, int _Height)
    {
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(_Width, _Height);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    void SetResolution(int width = 1920, int height = 1080)
    {
        SetCanvasScaler(width, height);

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(width, (int)(((float)deviceHeight / deviceWidth) * width), true);

        // if ((float)width / height < (float)deviceWidth / deviceHeight)
        // {
        //     float newWidth = ((float)width / height) / ((float)deviceWidth / deviceHeight);
        //     Camera.main.rect = new Rect(((1f - newWidth) / 2f), 0f, newWidth, 1f);
        // }
        // else
        // {
        //     float newHight = ((float)deviceWidth / deviceHeight) / ((float)width / height);
        //     Camera.main.rect = new Rect(0f, ((1f - newHight) / 2f), 1f, newHight);
        // }
    }
}