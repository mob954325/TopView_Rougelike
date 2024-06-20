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
                CurrentState = EnemyState.Dead; // 사망 상태로 변경
            }
        }
    }

    /// <summary>
    /// 최대체력
    /// </summary>
    public float maxHp = 20f;
    public float MaxHealth => CurrentHealth;


    /// <summary>
    /// 애니메이터 Hit 파라미터 ( 피격 )
    /// </summary>
    int HashToHit = Animator.StringToHash("Hit");

    /// <summary>
    /// 사망 시 호출되는 델리게이트
    /// </summary>
    public System.Action onDie { get; set; }

    protected override void Awake()
    {
        base.Awake();
        weapon = GetComponentInChildren<WeaponBase>();

        currentHealth = maxHp;

        if(weapon == null) // 무기가 없을 때 임시 오브젝트 생성
        {
            // 무기 예외처리
            GameObject obj = new GameObject();
            obj.name = $"new WeaponBase Object( Created )";
            obj.AddComponent<WeaponBase>();
            
            weapon = obj.GetComponent<WeaponBase>();
        }

        onFindPlayer = OnFindTarget;
    }

    protected override void OnReady()
    {
        base.OnReady();
    }

    protected override void OnTracing()
    {
        dirVec = target.transform.position - transform.position;    // 추적할 방향        
        transform.LookAt(target.transform.position);                // 바라보는 방향
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

    /// <summary>
    /// 공격 목표를 설정하는 함수
    /// </summary>
    /// <param name="target">목표 오브젝트</param>
    void OnFindTarget(GameObject targetObj)
    {
        target = targetObj;
    }

    protected override void OnDead()
    {
        base.OnDead();
        StartCoroutine(DisableObject(2f));
    }

    // IBattler 함수 ======================================================================================
    public void Attack(IBattler target)
    {
        target.Hit(AttackPower);
    }

    public void Hit(float hitDamage)
    {
        if (CurrentHealth <= 0) // 체력이 있을 때만 피격 처리
            return;

        CurrentHealth -= hitDamage - DefencePower;
        animator.SetTrigger(HashToHit);
    }
    // 애니메이션 함수 ======================================================================================

    /// <summary>
    /// 공격 시작시 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void OnAttackStart()
    {
        if(type == Type.Mage) // 마법사 공격 임시 추가 ( 적 타입별 공격 함수 )
        {
            Weapon_Staff currnetWeapon = weapon as Weapon_Staff;
            if (currnetWeapon != null)
            {
                currnetWeapon.CastingSpell(target.transform.position);
                //Debug.Log(target.transform.position);
            }
        }
        else
        {
            weapon.ActiveWeapon();
        }
    }

    /// <summary>
    /// 공격이 끝날 때 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void OnAttackEnd()
    {
        weapon.InactiveWeapon();

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