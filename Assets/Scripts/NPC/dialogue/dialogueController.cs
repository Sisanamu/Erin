using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dialogueController : MonoBehaviour, IPointerClickHandler
{
    public static dialogueController instance;
    public GameObject dialogues;
    protected Text dialogue;
    public List<string> dialogueList = new List<string>();
    public int dialogueIndex = 0;
    [SerializeField] protected npcController NPC;
    private void Awake()
    {
        instance = this;
    }

    protected void Start()
    {
        dialogue = GetComponent<Text>();
    }

    protected virtual void Update()
    {
        dialogue.text = dialogueList[dialogueIndex].ToString();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }
}