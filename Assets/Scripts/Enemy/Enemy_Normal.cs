using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 기본적이 가지는 클래스
/// </summary>
public class Enemy_Normal : EnemyBase, IHealth, IBattler
{
    public enum Type
    {
        None = 0, // 정의되지않음
        Warrior,
        Mage
    }

    Rigidbody rigid;
    WeaponBase weapon;

    [Header("Enemy Normal Settings")]
    public Type type;

    /// <summary>
    /// 추적 방향 벡터
    /// </summary>
    Vector3 dirVec;

    /// <summary>
    /// 공격하는지 확인하는 임시 변수
    /// </summary>
    bool isAttack = false;

    [Header("전투")]
    /// <summary>
    /// 현재 공격력
    /// </summary>
    public float attackPower = 5f;

    public float AttackPower 
    { 
        get => attackPower;
        set => attackPower = value; 
    }

    /// <summary>
    /// 방어력
    /// </summary>
    public float defencePower = 1f;
    public float DefencePower 
    {
        get => defencePower; 
        set => defencePower = value; 
    }

    [Header("체력")]
    /// <summary>
    /// 현재 체력
    /// </summary>
    public float currentHealth;
    public float CurrentHealth 
    { 
        get => currentHealth; 
        set
        {
            currentHealth = value;

            if(currentHealth <= 0f)
            {
                onDie?.Invoke();
            }
        }
    }

    /// <summary>
    /// 최대체력
    /// </summary>
    public float maxHp = 20f;
    public float MaxHealth => CurrentHealth;

    public System.Action onDie { get; set; }

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<WeaponBase>();

        currentHealth = maxHp;

        // 예외 처리 ===============================================
        if(weapon == null)
        {
            // 무기 예외처리
            GameObject obj = new GameObject();
            obj.name = $"new WeaponBase Object( Created )";
            obj.AddComponent<WeaponBase>();
            
            weapon = obj.GetComponent<WeaponBase>();
        }
    }

    protected override void OnReady()
    {
        base.OnReady();
        traget = FindAnyObjectByType<Player>().gameObject;   // 플레이어로 타겟지정
    }

    protected override void OnTracing()
    {
        dirVec = traget.transform.position - transform.position;    // 추적할 방향        
        transform.LookAt(traget.transform.position);                // 바라보는 방향
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f); // 수평 회전 외 전부 막기

        if (dirVec.sqrMagnitude < range * range)    // 범위 안에 있으면
        {
            CurrentState = EnemyState.Attack;       // 공격 상태로 변환
        }
        else                                        // 범위 밖에 있으면
        {
            // 플레이어 쪽으로 이동
            rigid.MovePosition(transform.position + Time.fixedDeltaTime * dirVec.normalized * tracingSpeed);
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

    // IBattler 함수 ======================================================================================
    public void Attack(IBattler target)
    {
        target.Hit(AttackPower);
    }

    public void Hit(float hitDamage)
    {
        CurrentHealth -= hitDamage - DefencePower;
    }
    // 애니메이션 함수 ======================================================================================

    /// <summary>
    /// 공격 시작시 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void OnAttackStart()
    {
        weapon.ActiveCollider();
    }

    /// <summary>
    /// 공격이 끝날 때 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void OnAttackEnd()
    {
        weapon.InactiveCollider();

        isAttack = false;   // 공격 종료
        CurrentState = EnemyState.Tracing; // 공격 후 대기 상태로 변환
    }

    // 에디터 ==============================================================================================

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = CurrentState == EnemyState.Attack ? Color.red : Color.yellow;
        Handles.DrawWireDisc(this.transform.position, transform.up, range);
    }
#endif
}