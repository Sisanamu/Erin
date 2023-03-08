using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class stillProcessQuest : dialogueController
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        dialogueIndex++;
        if (dialogueList.Count <= dialogueIndex)
        {
            playerController.Instance.dialogueOn = false;
            NPC.dialogueOn = false;
            dialogueIndex = 0;
        }
    }
}