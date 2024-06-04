using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 상태 enum
/// </summary>
public enum EnemyState
{
    Ready = 0,
    Tracing,
    Attack,
    Dead
}

[RequireComponent(typeof(Animator))]
public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// 적 현재 상태 ( 준비, 추격, 공격, 사망 )
    /// </summary>
    [SerializeField] EnemyState state = EnemyState.Ready;

    /// <summary>
    /// 상태 접근 및 수정 프로퍼티
    /// </summary>
    EnemyState State
    {
        get => state;
        set
        {
            state = value;

            switch (state)
            {
                case EnemyState.Ready:
                    Speed = 0f;
                    break;
                case EnemyState.Tracing:
                    Speed = tracingSpeed;
                    OnTracing();
                    break;
                case EnemyState.Attack:
                    OnAttack();
                    Speed = 0f;
                    break;
                case EnemyState.Dead:
                    OnDead();
                    Speed = 0f;
                    break;
            }
            animator.SetFloat(HashToSpeed, Speed);
        }
    }

    protected Animator animator;

    /// <summary>
    /// 배치후 대기 시간
    /// </summary>
    float deployDelayTime = 1f;

    /// <summary>
    /// 현재 이동 속도
    /// </summary>
    float Speed;

    /// <summary>
    /// 추적 속도
    /// </summary>
    public float tracingSpeed = 3f;

    /// <summary>
    /// 애니메이터 Speed 파라미터
    /// </summary>
    int HashToSpeed = Animator.StringToHash("Speed");

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        OnReady(deployDelayTime);
    }

    /// <summary>
    /// 배치 후 공격하기 전 대기하는 코루틴
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DeployDelay(float time)
    {
        yield return new WaitForSeconds(time);
        State = EnemyState.Tracing;
    }

    /// <summary>
    /// 스폰하고 실행되는 함수 (OnEnable)
    /// </summary>
    protected virtual void OnReady(float delayTime)
    {
        StartCoroutine(DeployDelay(delayTime));
    }

    /// <summary>
    /// 추적할 때 호출하는 함수
    /// </summary>
    protected virtual void OnTracing()
    {
        
    }

    /// <summary>
    /// 공격할 때 호출하는 함수
    /// </summary>
    protected virtual void OnAttack()
    {

    }

    /// <summary>
    /// 사망했을 때 호출하는 함수
    /// </summary>
    protected virtual void OnDead()
    {

    }
}