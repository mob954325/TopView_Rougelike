using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player_InputSettings))]

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IHealth, IBattler
{
    public Player_InputSettings playerInput;

    Rigidbody rigid;
    Animator animator;
    // 가지고 있는 아이템 정보=============================================  

    /// <summary>
    /// 열쇠 개수
    /// </summary>
    [SerializeField] uint keyCount = 0;

    /// <summary>
    /// 골드 개수
    /// </summary>
    [SerializeField] uint coinAmount = 0;

    /// <summary>
    /// 폭탄 개수
    /// </summary>
    [SerializeField] uint bombCount = 0;

    // IHealth =========================================================
    /// <summary>
    /// 시작 체력
    /// </summary>
    float startHealth = 10f;

    /// <summary>
    /// 현재 체력
    /// </summary>
    [SerializeField]float currnetHealth;
    public float CurrentHealth 
    { 
        get => currnetHealth;
        set
        {
            currnetHealth = Mathf.Clamp(value, 0f, MaxHealth);

            if(currnetHealth <= 0f) // 체력이 없으면 사망
            {
                onDie?.Invoke();
            }
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    float maxHealth;
    public float MaxHealth => maxHealth;

    public Action onDie { get; set; }

    // IBattler ========================================================

    /// <summary>
    /// 캐릭터 현재 공격력
    /// </summary>
    public float attackPower = 2f;
    public float AttackPower
    {
        get => attackPower;
        set => attackPower = value;
    }

    /// <summary>
    /// 캐릭터 현재 방어력 ( 가진 방어력만큼 데미지가 덜 들어감)
    /// </summary>
    public float defencePower = 1f;
    public float DefencePower 
    {
        get => defencePower;
        set => defencePower = value;
    }


    // Movement =========================================================
    /// <summary>
    /// 플레이어 현재 속도
    /// </summary>
    public float speed = 5.0f;

    /// <summary>
    /// 플레이어 걷는 속도 ( 기본속도 )
    /// </summary>
    private float walkSpeed = 5f;

    /// <summary>
    /// 달리기 속도
    /// </summary>
    public float sprintSpeed = 8f;

    // Hashes ===========================================================

    /// <summary>
    /// 이동 값 파라미터
    /// </summary>
    int hashToSpeed = Animator.StringToHash("Speed");

    /// <summary>
    /// 공격 시작 확인 파라미터 ( true : 공격, false : 공격 안함 ) 
    /// </summary>
    int HashToAttack = Animator.StringToHash("Attack");

    /// <summary>
    /// 공격을 하는 중인지 확인하는 프로퍼티 ( true : 공격중, false : 공격안하고있음 )
    /// </summary>
    int HashToIsAttacking = Animator.StringToHash("IsAttacking");

    /// <summary>
    /// 애니메이션 피격 파라미터 ( trigger )
    /// </summary>
    int HashToHit = Animator.StringToHash("Hit");

    /// <summary>
    /// 애니메이션 Death 파라미터 ( bool )
    /// </summary>
    int HashToDeath = Animator.StringToHash("Death");

    void Awake()
    {
        playerInput = GetComponent<Player_InputSettings>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        CharacterInintialize();
    }

    /// <summary>
    /// 캐릭터를 초기화 할 때 실행하는 함수 
    /// </summary>
    private void CharacterInintialize()
    {
        maxHealth = startHealth;
        CurrentHealth = MaxHealth;

        onDie += OnDie;
    }

    // Movement =============================================================

    /// <summary>
    /// 플레이어 이동함수
    /// </summary>
    /// <param name="moveVector">움직일 방향 값</param>
    /// <param name="isSprint">움직일 방향 값</param>
    public void Move(Vector3 moveVector, bool isSprint)
    {
        speed = isSprint ? sprintSpeed : walkSpeed; // 달리는지 걷는지 확인하고 값 변경

        rigid.MovePosition(transform.position + Time.fixedDeltaTime * moveVector * speed);
        animator.SetFloat(hashToSpeed, moveVector.magnitude * speed / sprintSpeed);
    }

    /// <summary>
    /// 플레이어 회전 함수
    /// </summary>
    /// <param name="lookVector">캐릭터가 바라보는 벡터값</param>
    public void Look(Vector3 lookVector)
    {
        transform.LookAt(lookVector);   // 플레이어가 바라보는 방향
    }

    // Battle   =============================================================

    /// <summary>
    /// 플레이어 공격 시 실행하는 함수
    /// </summary>
    public void Attack(IBattler target)
    {
        if(target != null)
        {
            target.Hit(attackPower);
        }
    }

    public void OnAttack()
    {
        animator.SetBool(HashToAttack, true);   // 공격 애니메이션 시작
    }

    /// <summary>
    /// 사망시 실행되는 함수
    /// </summary>
    public void OnDie()
    {
        animator.SetTrigger(HashToDeath);
        // 조작 막기 
    }

    // 아이템 관련 상호작용 함수 ================================================

    /// <summary>
    /// 열쇠 개수 증가 함수
    /// </summary>
    /// <param name="count">획득량</param>
    public void GetKey(uint count)
    {
        keyCount += count;
    }

    /// <summary>
    /// 열쇠를 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 열쇠 개수</param>
    public void UseKey(uint count = 1)
    {
        keyCount -= count;
    }

    /// <summary>
    /// 골드 증가 함수
    /// </summary>
    /// <param name="amount">획득량</param>
    public void GetCoin(uint amount)
    {
        coinAmount += amount;
    }

    /// <summary>
    /// 코인를 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 코인 개수</param>
    public void UseCoin(uint count)
    {
        coinAmount -= count;
    }

    /// <summary>
    /// 폭탄 개수 증가 함수 (획득)
    /// </summary>
    /// <param name="count">획득량</param>
    public void GetBomb(uint count)
    {
        bombCount += count;
    }

    /// <summary>
    /// 폭탄 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 폭탄 개수</param>
    public void UseBomb(uint count = 1)
    {
        bombCount -= count;
    }


    // 애니메이션 이벤트 함수 ==================================================

    /// <summary>
    /// 공격 시작 시 호출되는 함수 ( 애니메이션 이벤트 함수 )
    /// </summary>
    /// <param name="isAttacking">공격 중이면 true 아니면 false</param>
    public void BeginAttackAnim(bool isAttacking)
    {
        animator.SetBool(HashToAttack, false);
        animator.SetBool(HashToIsAttacking, isAttacking);
    }

    /// <summary>
    /// 공격이 끝나면 호출되는 함수 ( 애니메이션 이벤트 함수 )
    /// </summary>
    /// <param name="isAttacking">공격 중이면 true 아니면 false</param>
    public void EndAttackAnim(bool isAttacking)
    {
        animator.SetBool(HashToAttack, false);
        animator.SetBool(HashToIsAttacking, isAttacking);
    }

    /// <summary>
    /// 피격시 호출되는 함수
    /// </summary>
    /// <param name="hitDamage"></param>
    public void Hit(float hitDamage)
    {
        CurrentHealth -= hitDamage - DefencePower;
        animator.SetTrigger(HashToHit);
    }
}