using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적 상태 enum
/// </summary>
public enum EnemyState
{
    Ready = 0,
    Idle,
    Tracing,
    Attack,
    Dead
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class EnemyBase : PoolObject
{
    [Header("Enemy Base Settings")]
    /// <summary>
    /// 적 현재 상태 ( 준비, 추격, 공격, 사망 )
    /// </summary>
    [SerializeField] EnemyState currentState = EnemyState.Ready;

    /// <summary>
    /// 상태 접근 및 수정 프로퍼티
    /// </summary>
    protected EnemyState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;

            switch (currentState)
            {
                case EnemyState.Ready:
                    // 시작 초기화
                    onAction = OnReady;
                    Speed = 0f;
                    break;
                case EnemyState.Idle:
                    onAction = OnIdle;
                    Speed = 0f;
                    break;
                case EnemyState.Tracing:
                    Speed = tracingSpeed;
                    onAction = OnTracing;
                    break;
                case EnemyState.Attack:
                    Speed = 0f;
                    onAction = OnAttack;
                    break;
                case EnemyState.Dead:
                    // 사망처리때 초기화
                    OnDead();
                    Speed = 0f;
                    break;
            }
            animator.SetFloat(HashToSpeed, Speed);
        }
    }

    protected Animator animator;
    protected Rigidbody rigid;
    protected Collider coll;

    /// <summary>
    /// 공격할 목표 오브젝트
    /// </summary>
    protected GameObject target;

    /// <summary>
    /// 대기 시간
    /// </summary>
    float delayTime = 1f;

    /// <summary>
    /// 현재 이동 속도
    /// </summary>
    float Speed;

    /// <summary>
    /// 추적 속도
    /// </summary>
    public float tracingSpeed = 3f;

    /// <summary>
    /// 공격 최대 사거리
    /// </summary>
    public float range = 3f;

    // 기타 ================================================================

    /// <summary>
    /// 상태 머신이 실행할 델리게이트
    /// </summary>
    Action onAction;

    /// <summary>
    /// 플레이어를 찾으면 호출되는 델리게이트
    /// </summary>
    public Action<GameObject> onFindPlayer;

    /// <summary>
    /// 애니메이터 Speed 파라미터
    /// </summary>
    int HashToSpeed = Animator.StringToHash("Speed");

    /// <summary>
    /// 애니메이터 Death 파라미터 ( 사망 bool )
    /// </summary>
    int HashToDeath = Animator.StringToHash("Death");

    // 기본함수 ==============================================================

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    protected virtual void OnEnable()
    {
        OnReady();
    }

    private void Update()
    {
        onAction?.Invoke();
    }

    // -> 밑 함수를 추상화로 뺄 수 있나?

    /// <summary>
    /// 배치 후 공격하기 전 대기하는 코루틴
    /// </summary>
    /// <param name="time"> 대기 시간</param>
    protected virtual IEnumerator DeployDelay(float time)
    {
        yield return new WaitForSeconds(time);

        if(target != null)
        {
            CurrentState = EnemyState.Tracing;
        }
        else
        {
            CurrentState = EnemyState.Idle;
        }
    }

    /// <summary>
    /// 스폰하고 실행되는 함수 (한번만 호출)
    /// </summary>
    protected virtual void OnReady()    
    {        
        StartCoroutine(DeployDelay(delayTime));
    }

    /// <summary>
    /// 대기 상태일 때 호출되는 함수 (업데이트)  
    /// </summary>
    protected virtual void OnIdle()
    {
        // target이 null이 아닐 때까지 계속 기다림
        if(target != null)
        {
            CurrentState = EnemyState.Tracing;
        }
    }

    /// <summary>
    /// 추적할 때 호출하는 함수 (업데이트)
    /// </summary>
    protected virtual void OnTracing()
    {
    }

    /// <summary>
    /// 공격할 때 호출하는 함수 (업데이트)
    /// </summary>
    protected virtual void OnAttack()
    {
    }

    /// <summary>
    /// 사망했을 때 호출하는 함수 (한번만 호출)
    /// </summary>
    protected virtual void OnDead()
    {
        animator.SetTrigger(HashToDeath);   // 사망 애니메이션 활성화
    }
}