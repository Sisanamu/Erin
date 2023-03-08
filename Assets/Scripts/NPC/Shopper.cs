using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : npcController
{
    public GameObject Shop;
    public GameObject reinForce;
    public GameObject Inventory;

    protected override void Update()
    {
        base.Update();
        if(!dialogueOn)
        {
            Shop.SetActive(false);
            reinForce.SetActive(false);
        }
    }

    public void ClickShop()
    {
        SoundManager.instance.PlayEffects("Confirm");
        Shop.SetActive(!Shop.activeSelf);
        nameUI.SetActive(!Shop.activeSelf);
    }
    public void clickReinForce()
    {
        SoundManager.instance.PlayEffects("Confirm");
        reinForce.SetActive(!reinForce.activeSelf);
        Inventory.SetActive(reinForce.activeSelf);
        nameUI.SetActive(!reinForce.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerController>().NPC = gameObject.GetComponent<npcController>();
            Player = other.GetComponent<playerController>();
            nameUI.SetActive(true);
            isWalk = false;
            nameUI.SetActive(true);
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.Instance.NPC = null;
            Player = null;
            nameUI.SetActive(false);
            Shop.SetActive(false);
            reinForce.SetActive(false);
            isWalk = true;
            StartCoroutine(NextPatrol());
        }
    }
}