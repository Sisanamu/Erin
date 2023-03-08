using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warpController : MonoBehaviour
{
    public string SceneName;
    public Vector3 warpPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlayEffects("Warp");
            other.transform.position = warpPos;
            loadingSceneManager.LoadScene(SceneName);
            
            gameObject.SetActive(false);
        }
    }
}