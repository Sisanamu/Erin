using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class nomaldialogue : dialogueController
{
    [SerializeField] protected GameObject enterButton;

    protected override void Update()
    {
        base.Update();
        if(dialogueList.Count -1 != dialogueIndex)
        {
            enterButton.SetActive(false);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        dialogueIndex++;
        if (dialogueList.Count <= dialogueIndex)
        {
            playerController.Instance.dialogueOn = false;
            NPC.dialogueOn = false;
            dialogueIndex = 0;
            enterButton.SetActive(false);
        }
        if (dialogueList.Count - 1 == dialogueIndex)
            enterButton.SetActive(true);
    }
}