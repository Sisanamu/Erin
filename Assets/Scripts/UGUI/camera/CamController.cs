using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamController : MonoBehaviour
{
    public Transform Player;
    public LayerMask Structure;
    private RaycastHit hit;
    public bool isStructer;
    public Vector3 PlayerPos;
    public Vector3 PlayerCam;
    public float dir;
    public Vector3 _delta = new Vector3(0, 4, -15);
    Vector3 playerCenter = new Vector3(0, 1, 0);

    private void Update()
    {
        PlayerPos = Player.position + playerCenter;
        PlayerCam = transform.position - PlayerPos;
        PlayerCam.Normalize();

        if(Physics.Raycast(PlayerPos, PlayerCam, out hit, 15.5f, Structure))
        {
            Debug.DrawLine(PlayerPos, PlayerCam, Color.red, 15.5f);
            Debug.Log(hit.point);
            float dist = (hit.point - Player.position).magnitude * 0.8f;
            transform.localPosition = _delta.normalized * dist;
        }
        else
        {
            Debug.DrawLine(PlayerPos, PlayerCam, Color.green, 15.5f);
            transform.localPosition = _delta;
        }
    }
}