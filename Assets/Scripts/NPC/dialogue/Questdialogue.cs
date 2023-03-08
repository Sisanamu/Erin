using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Questdialogue : dialogueController
{
    [SerializeField] protected GameObject enterButton;

    protected override void Update()
    {
        base.Update();
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
            NPC.nameUI.SetActive(true);
        }
        if (dialogueList.Count - 1 == dialogueIndex)
        {
            NPC.nameUI.SetActive(false);
            enterButton.SetActive(true);
        }
    }
}