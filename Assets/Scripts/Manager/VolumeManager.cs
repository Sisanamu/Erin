using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider Effectslider;
    [SerializeField] private Slider BGMslider;
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private AudioSource BGM;

    private void Start()
    {
        audios = FindObjectOfType<SoundManager>().audioSourcesEffects;
        BGM = FindObjectOfType<SoundManager>().audioBGM;

        Effectslider.onValueChanged.AddListener(Effectvolume);
        BGMslider.onValueChanged.AddListener(BGMvolume);
    }
    private void Update()
    {
        Effectslider.value = audios[0].volume;
        BGMslider.value = BGM.volume;
    }

    public void Effectvolume(float value)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].volume = value;
        }
    }
    public void BGMvolume(float value)
    {
        BGM.volume = value;
    }
}