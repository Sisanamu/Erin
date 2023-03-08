using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDialogue : MonoBehaviour
{
    [SerializeField] protected playerController Player;
    [SerializeField] protected npcController NPC;
    [SerializeField] protected GameObject joystick;
    [SerializeField] protected GameObject Cam;

    private void Update()
    {
        if (Player.NPC != null)
        {
            NPC = Player.NPC.GetComponent<npcController>();
        }
        if(!Player.dialogueOn)
        {
            joystick.SetActive(true);
            Cam.SetActive(true);
        }
    }

    public void dialogueOn()
    {
        Player.dialogueOn = !Player.dialogueOn;
        joystick.SetActive(false);
        Cam.SetActive(false);
        NPC.dialogueOn = Player.dialogueOn;
    }
}
