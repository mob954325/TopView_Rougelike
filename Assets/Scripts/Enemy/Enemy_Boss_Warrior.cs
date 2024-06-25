using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Boss_Warrior : Enemy_Normal
{
    // 행동순서 
    // 플레이어에게 돌진한다 -> 내려 찍기 ( 광역 공격 ) -> 대기 -> 공격 ( 랜덤 )
    // ready - idle - tracing - attack - die

    GameManager manager;

    Enemy_AttackArea attackArea;

    Vector3 NextAttackPosition;

    public float rotateSpeed = 5f;

    int HashToAttack = Animator.StringToHash("Attack");
    int HashToTaunt = Animator.StringToHash("Taunt");

    // 생명 함수 ==============================================================================================

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        attackArea = GetComponentInChildren<Enemy_AttackArea>();

        manager = GameManager.Instance;

        currentHealth = maxHp;
    }

    protected override void OnEnable()
    {
        base.OnEnable();    // OnReady 실행
    }

    protected override void OnDisable()
    {
        base.OnDisable();   // 
    }

    // 행동 함수 ==============================================================================================

    protected override void OnReady()
    {
        base.OnReady(); // 활성화 후 1초 대기
    }

    protected override void OnIdle()
    {
        base.OnIdle();

        // 다음 공격 준비
        if (manager.player != null)
        {
            target = manager.player.gameObject;             // 타겟 지정             
        }

        NextAttackPosition = target.transform.position - transform.position;
        transform.LookAt(NextAttackPosition);                                        // 바라보는 방향
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);             // 수평 회전 외 전부 막기

        // 플레이어에게 돌아보기
        // 일정 각도가 되면 돌진 후 공격 시작

        CurrentState = EnemyState.Tracing;  // 상태전환
    }

    protected override void OnTracing()
    {
        dirVec = NextAttackPosition - transform.position;

        if (dirVec.sqrMagnitude < range * range) // 범위 안에 플레이어가 있으면 
        {
            CurrentState = EnemyState.Attack;   // 공격
        }
        else
        {
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * dirVec.normalized * tracingSpeed);
        }
    }

    protected override void OnAttack()
    {
        // 공격 종류
        // 내려 찍기 ( jump chop )
        // 잡몹 소환 ( Taunt )
        if (dirVec.sqrMagnitude > range * range) // 범위 안에 플레이어가 없으면
        {
            CurrentState = EnemyState.Tracing;   // 추적
        }

        if(!isAttack)
        {
            float rand = Random.value;
            if(rand > 0.2f)
            {
                animator.SetTrigger(HashToAttack);
                isAttack = true;
            }
            else
            {
                animator.SetTrigger(HashToTaunt);
                isAttack = true;
            }
        }
    }

    protected override void OnDead()
    {
        base.OnDead(); // 사망 트리거
    }

    // 애니메이션 함수 =================================================================

    public override void OnAttackStart()
    {
        attackArea.ActiveCollider();
    }

    public override void OnAttackEnd()
    {
        attackArea.DeactiveCollider();

        isAttack = false;
        CurrentState = EnemyState.Idle;
    }

    public void OnTauntStart()
    {
        for(int i = 0; i < 3; i++)
        {
            Vector3 spawnPoint = new ((Random.insideUnitCircle * range).x, 0, (Random.insideUnitCircle * range).y);
            Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Warrior, spawnPoint, Quaternion.identity);
        }
    }

    public void OnTauntEnd()
    {
        isAttack = false;
    }
}
