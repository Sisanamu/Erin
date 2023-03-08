using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : enemyController
{
    protected override void Update()
    {
        base.Update();
        TargettingImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.5f, 0));
    }
}