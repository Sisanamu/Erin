using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFoot : MonoBehaviour
{
    [SerializeField] private string[] SoundName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("grass"))
        {
            SoundManager.instance.PlayEffects(SoundName[0]);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("wood"))
        {
            SoundManager.instance.PlayEffects(SoundName[1]);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("rock"))
        {
            SoundManager.instance.PlayEffects(SoundName[2]);
        }
    }
}