using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rouge : Enemy_Normal
{
    const float backStabCoolTime = 3f;
    float backStabTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnterState()
    {
        backStabTimer = backStabCoolTime;
    }

    protected override void OnTracing()
    {
        base.OnTracing();

        if(backStabTimer < 0)   // 뒤치기 실행 (쿨타임이 돌면)
        {
            backStabTimer = backStabCoolTime;
            transform.localPosition = target.transform.localPosition + -transform.forward;
        }

        backStabTimer -= Time.deltaTime;
    }
}