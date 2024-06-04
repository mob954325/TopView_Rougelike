using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_Warrior : EnemyBase
{
    Rigidbody rigid;

    /// <summary>
    /// 추적 방향 벡터
    /// </summary>
    Vector3 dirVec;
    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnEnable()
    {
        OnReady(2f);
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + Time.fixedDeltaTime * dirVec * tracingSpeed);
    }

    protected override void OnReady(float delayTime)
    {
        base.OnReady(delayTime);

        traget = FindAnyObjectByType<Player>().gameObject;   // 플레이어로 타겟지정
    }

    protected override void OnTracing()
    {
        dirVec = traget.transform.position - transform.position;
    }

    protected override void OnAttack()
    {
        animator.SetTrigger("Attack");
    }
}