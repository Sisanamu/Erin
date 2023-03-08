using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // 직렬화
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    #region  SingleTon
    private static SoundManager Instance = null;
    public static SoundManager instance
    {
        get
        {
            if (Instance == null)
                return null;
            return Instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public AudioSource[] audioSourcesEffects;
    public AudioSource audioBGM;
    public string[] playerSoundName;
    public Sound[] effectSounds;
    public Sound[] BGMSound;

    void Start()
    {
        playerSoundName = new string[audioSourcesEffects.Length];
    }

    public void PlayEffects(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourcesEffects.Length; j++)
                {
                    if (!audioSourcesEffects[j].isPlaying)
                    {
                        playerSoundName[j] = effectSounds[i].name;
                        audioSourcesEffects[j].clip = effectSounds[i].clip;
                        audioSourcesEffects[j].Play();
                        return;
                    }
                }
                return;
            }
        }
    }
    public void PlayBGM(string _name)
    {
        for (int i = 0; i < BGMSound.Length; i++)
        {
            if (_name == BGMSound[i].name)
            {
                if (!audioSourcesEffects[i].isPlaying)
                {
                    audioBGM.clip = BGMSound[i].clip;
                    audioBGM.Play();
                    return;
                }
                return;
            }
        }
    }
    public void StopAllSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }
    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            if (playerSoundName[i] == _name)
            {
                audioSourcesEffects[i].Stop();
                return;
            }
        }
    }
}