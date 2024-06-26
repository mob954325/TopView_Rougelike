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

    public float rotateSpeed = 5f;

    int HashToAttack = Animator.StringToHash("Attack");
    int HashToTaunt = Animator.StringToHash("Taunt");

    List<GameObject> spawnedEnemy;

    // 생명 함수 ==============================================================================================

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        attackArea = GetComponentInChildren<Enemy_AttackArea>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();    // OnReady 실행

        manager = GameManager.Instance;
        currentHealth = maxHealth;
        spawnedEnemy = new List<GameObject>();
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
    }

    protected override void OnTracing()
    {
        dirVec = target.transform.position - transform.position;

       if (dirVec.sqrMagnitude < range * range) // 범위 안에 플레이어가 있으면 
       {
           CurrentState = EnemyState.Attack;   // 공격
       }
       else
       {
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * dirVec.normalized * tracingSpeed);

            RotateToTarget();
        }
    }

    protected override void OnAttack()
    {
        RotateToTarget();

        if (dirVec.sqrMagnitude > range * range) // 범위 안에 플레이어가 없으면
        {
            CurrentState = EnemyState.Tracing;   // 추적
        }

        if (!isAttack)
        {
            float rand = Random.value;
            if(spawnedEnemy != null && spawnedEnemy.Count > 5) // 최대 적 개수가 넘으면 일반 공격
            {
                rand = 1f;
            }

            if (rand > 0.2f)
            {
                // 전방 내려 찍기
                animator.SetTrigger(HashToAttack);
                isAttack = true;
            }
            else
            {
                // 잡몹 소환
                animator.SetTrigger(HashToTaunt);
                isAttack = true;
            }
        }
        else // 공격 중 피격 받으면 상태 안변하는 거 방지 -> 3초 지나면 추적상태 전환
        {
            attackTimer += Time.deltaTime;
            //Debug.Log(attackTimer);

            if(attackTimer > 3f)
            {
                attackTimer = 0f;
                isAttack = false;
                CurrentState = EnemyState.Tracing;   // 추적
            }
        }
    }

    protected override void OnDead()
    {
        base.OnDead(); // 사망 트리거
    }
    
    // 기타 함수 ======================================================================

    /// <summary>
    /// Target에게 회전하는 함수 ( Target을 바라본다 )
    /// </summary>
    private void RotateToTarget()
    {
        Quaternion toRotation = Quaternion.LookRotation(dirVec, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
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
            GameObject obj = Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Warrior, spawnPoint, Quaternion.identity);
            spawnedEnemy.Add(obj);
        }
    }

    public void OnTauntEnd()
    {
        isAttack = false;
    }
}
