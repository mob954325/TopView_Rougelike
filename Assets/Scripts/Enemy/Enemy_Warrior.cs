using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_Warrior : EnemyBase
{
    Rigidbody rigid;

    /// <summary>
    /// 추적 방향 벡터
    /// </summary>
    Vector3 dirVec;

    /// <summary>
    /// 공격하는지 확인하는 임시 변수
    /// </summary>
    bool isAttack = false;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }

    protected override void OnReady()
    {
        base.OnReady();
        traget = FindAnyObjectByType<Player>().gameObject;   // 플레이어로 타겟지정
    }

    protected override void OnTracing()
    {
        dirVec = traget.transform.position - transform.position;    // 추적할 방향

        rigid.MovePosition(transform.position + Time.fixedDeltaTime * dirVec * tracingSpeed);
        transform.LookAt(traget.transform.position);

        if(dirVec.sqrMagnitude < range * range)
        {
            CurrentState = EnemyState.Attack; // 공격 상태로 변환
        }
    }

    protected override void OnAttack()
    {
        if (dirVec.sqrMagnitude > range * range) // 공격 범위에 벗어나면
        {
            CurrentState = EnemyState.Tracing; // 추격 상태로 변환
        }

        if (!isAttack)  // 공격을 하지 않는 상태면
        {
            // 공격 시작
            animator.SetTrigger("Attack");
            isAttack = true;
        }
    }

    // 애니메이션 함수 ======================================================================================

    /// <summary>
    /// 공격이 끝날 때 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void OnAttackEnd()
    {
        isAttack = false;   // 공격 종료
        CurrentState = EnemyState.Ready; // 공격 후 대기 상태로 변환
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = CurrentState == EnemyState.Attack ? Color.red : Color.yellow;
        Handles.DrawWireDisc(this.transform.position, transform.up, range);
    }
#endif
}