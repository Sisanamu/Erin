using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcController : MonoBehaviour
{
    [SerializeField] protected string npcName;
    [SerializeField] Text nameTag;
    [SerializeField] protected Animator anim;
    [SerializeField] protected bool isWalk;
    [SerializeField] public bool dialogueOn;
    [SerializeField] public GameObject dialogue;
    [SerializeField] protected GameObject Canvas;
    [SerializeField] protected Vector3 waitPoint;
    [SerializeField] protected Transform SpawnPoint;
    [SerializeField] protected float N_Speed;
    [SerializeField] protected float StartWaitTime;
    [SerializeField] protected float StopTime;
    [SerializeField] protected float WaitTime;
    [SerializeField] protected float Waitpos;
    [SerializeField] public GameObject nameUI;
    public playerController Player;
    public string[] soundName;

    private void Start()
    {
        isWalk = true;
        nameTag.text = npcName;
        anim = GetComponent<Animator>();
        nameUI.GetComponent<Text>().text = npcName;
    }
    protected virtual void Update()
    {
        UIaim();
        PatrolOn();
        if (dialogueOn)
        {
            dialogue.SetActive(true);
        }
        else
        {
            dialogue.GetComponentInChildren<nomaldialogue>().dialogueIndex = 0;
            dialogue.SetActive(false);
        }
        if(Player != null)
        {
            Rotate(Player.gameObject);
        }
    }
    void PatrolOn()
    {
        if (WaitTime > 0 && isWalk)
        {
            anim.SetBool("IsWalk", true);
            transform.Translate(Vector3.forward * N_Speed * Time.deltaTime);
            WaitTime -= Time.deltaTime;
        }
        else if (WaitTime <= 0)
        {
            StartCoroutine(NextPatrol());
        }
    }
    public void Rotate(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.localRotation = Quaternion.Slerp(transform.localRotation,
            Quaternion.LookRotation(dir), 30f * Time.deltaTime);
    }

    protected IEnumerator NextPatrol()
    {
        WaitTime = StartWaitTime;
        isWalk = false;
        anim.SetBool("IsWalk", false);
        waitPoint =
        new Vector3(SpawnPoint.position.x + Random.Range(-Waitpos, Waitpos),
        transform.position.y, SpawnPoint.position.z + Random.Range(-Waitpos, Waitpos));
        yield return new WaitForSeconds(StopTime);
        isWalk = true;
        transform.LookAt(waitPoint);
    }
    void UIaim()
    {
        nameUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
    }
}