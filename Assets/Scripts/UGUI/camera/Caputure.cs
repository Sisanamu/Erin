using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caputure : MonoBehaviour
{
    [SerializeField] Camera UICam;
    [SerializeField] RawImage rawImage;
    [SerializeField] RenderTexture renderTexture;

    private void Update()
    {
        capTure();
    }
    private void capTure()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = UICam.targetTexture;

        UICam.Render();

        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height);
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

        image.Apply();
        rawImage.texture = image;
    }
}
