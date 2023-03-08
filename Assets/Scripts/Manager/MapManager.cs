using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] protected string soundName;
    [SerializeField] protected string SceneName;
    [SerializeField] protected int NextLevel;
    [SerializeField] protected bool canWarp;
    [SerializeField] protected GameObject Warp;

    void Start()
    {
        SoundManager.instance.PlayBGM(soundName);
        Warp.GetComponent<warpController>().SceneName = SceneName;
        Warp.SetActive(false);
        
    }
    void Update()
    {
        if (FindObjectOfType<GameManager>() != null)
        {
            if (GameManager.Instance.Level >= NextLevel)
            {
                Warp.SetActive(true);
            }
        }
    }
}