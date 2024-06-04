using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warrior : EnemyBase
{
    protected override void OnEnable()
    {
        OnReady(2f);
    }

    protected override void OnReady(float delayTime)
    {
        base.OnReady(delayTime);
    }
}