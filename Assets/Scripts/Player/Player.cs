using System;
using UnityEngine;

[RequireComponent(typeof(Player_InputSettings))]

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IHealth, IBattler
{
    public Player_InputSettings playerInput;

    /// <summary>
    /// 모델 트랜스폼
    /// </summary>
    Transform modelTransform;

    Rigidbody rigid;
    Animator animator;

    /// <summary>
    /// 플레이어가 가지고 있는 센서
    /// </summary>
    Sensor sensor;

    // 가지고 있는 아이템 정보=============================================  

    [Header("플레이어 아이템 정보")]
    /// <summary>
    /// 열쇠 개수
    /// </summary>
    [SerializeField] uint keyCount = 0;

    /// <summary>
    /// 열쇠 개수 접근 및 수정 프로퍼티
    /// </summary>
    public uint KeyCount
    {
        get => keyCount;
        set
        {
            keyCount = (uint)Mathf.Clamp(value, 0, ItemDataManager.Instance.itemDatas[(int)ItemCodes.Key].maxCount);
            onChangeKey?.Invoke(keyCount);
        }
    }

    /// <summary>
    /// 코인 개수
    /// </summary>
    [SerializeField] uint coinAmount = 0;

    /// <summary>
    /// 코인 개수 접근 및 수정 프로퍼티
    /// </summary>
    public uint CoinAmount
    {
        get => coinAmount;
        set
        {
            coinAmount = (uint)Mathf.Clamp(value, 0, ItemDataManager.Instance.itemDatas[(int)ItemCodes.Coin].maxCount);
            onChangeCoin?.Invoke(coinAmount);
        }
    }

    /// <summary>
    /// 폭탄 개수
    /// </summary>
    [SerializeField] uint bombCount = 0;

    /// <summary>
    /// 폭탄 개수 접근 및 수정 프로퍼티
    /// </summary>
    public uint BombCount
    {
        get => bombCount;
        set
        {
            bombCount = (uint)Mathf.Clamp(value, 0, ItemDataManager.Instance.itemDatas[(int)ItemCodes.Bomb].maxCount);
            onChangeBomb?.Invoke(bombCount);
        }
    }

    // IHealth =========================================================

    /// <summary>
    /// 시작 체력
    /// </summary>
    float startHealth = 10f;

    [Header("플레이어 체력 정보")]
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
            onChangeHealth?.Invoke(currnetHealth);

            if (currnetHealth <= 0f) // 체력이 없으면 사망
            {
                onDie?.Invoke();
            }
        }
    }

    /// <summary>
    /// 최대 체력
    /// </summary>
    [SerializeField]float maxHealth;
    public float MaxHealth => maxHealth;

    public Action onDie { get; set; }

    // IBattler ========================================================

    [Header("플레이어 전투 정보")]

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

    [Header("플레이어 이동 정보")]

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

    /// <summary>
    /// 추가 속도 ( 버프용 )
    /// </summary>
    public float addtionalSpeed = 0f;

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
    /// 강공격 시작 트리거
    /// </summary>
    int HashToHeavyAttack = Animator.StringToHash("HeavyAttack");

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

    // 기타 ==================================================================

    /// <summary>
    /// 폭탄 개수가 변경될 때 실행되는 델리게이트
    /// </summary>
    public Action<uint> onChangeBomb;

    /// <summary>
    /// 열쇠 개수가 변경될 때 실행되는 델리게이트
    /// </summary>
    public Action<uint> onChangeKey;

    /// <summary>
    /// 코인 개수가 변경될 때 실행되는 델리게이트
    /// </summary>
    public Action<uint> onChangeCoin;

    /// <summary>
    /// 체력이 변경될 때 실행되는 델리게이트
    /// </summary>
    public Action<float> onChangeHealth;

    void Awake()
    {
        playerInput = GetComponent<Player_InputSettings>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sensor = GetComponentInChildren<Sensor>();
        modelTransform = transform.GetChild(6);

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
        speed = isSprint ? sprintSpeed + addtionalSpeed : walkSpeed + addtionalSpeed; // 달리는지 걷는지 확인하고 값 변경

        rigid.MovePosition(transform.position + Time.fixedDeltaTime * moveVector * speed);
        animator.SetFloat(hashToSpeed, moveVector.magnitude * ( speed - addtionalSpeed ) / sprintSpeed);
    }

    /// <summary>
    /// 플레이어 회전 함수
    /// </summary>
    /// <param name="lookVector">캐릭터가 바라보는 벡터값</param>
    public void Look(Vector3 lookVector)
    {
        modelTransform.LookAt(lookVector);   // 플레이어가 바라보는 방향
    }

    // Battle   =============================================================

    /// <summary>
    /// IBattler를 가진 오브젝트가 공격 받으면 실행하는 함수
    /// </summary>
    public void Attack(IBattler target)
    {
        if(target != null)
        {
            target.Hit(attackPower);
        }
    }

    /// <summary>
    /// 플레이어 공격시 실행하는 함수
    /// </summary>
    public void OnAttack()
    {
        animator.SetBool(HashToAttack, true);   // 공격 애니메이션 시작
    }

    /// <summary>
    /// 플레이어가 강공격시 실행하는 함수
    /// </summary>
    public void OnHeavyAttack()
    {
        animator.SetTrigger(HashToHeavyAttack);

        GameObject[] objs = sensor.detectedObjects.ToArray();

        for(int i = 0; i < objs.Length; i++)
        {
            IBattler battler = objs[i].GetComponent<IBattler>();
            Rigidbody rigidbody = objs[i].GetComponent<Rigidbody>();

            if(battler != null) // IBattler를 가진 오브젝트만 공격
            {
                // 공격
                battler.Hit(attackPower);

                // 밀치기
                Vector3 dir = objs[i].transform.position - this.transform.position;
                rigidbody.AddForce(dir * 5f, ForceMode.Impulse);
            }
        }
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
        KeyCount += count;
    }

    /// <summary>
    /// 열쇠를 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 열쇠 개수</param>
    /// <returns>사용 성공 여부 (true : 성공적으로 사용함, false : 개수 부족)</returns>
    public bool UseKey(uint count = 1)
    {
        bool result = true;    

        if(KeyCount <= 0)
        {
            result = false;
        }
        else
        {
            KeyCount -= count;
        }

        return result;
    }

    /// <summary>
    /// 골드 증가 함수
    /// </summary>
    /// <param name="amount">획득량</param>
    public void GetCoin(uint amount)
    {
        CoinAmount += amount;
    }

    /// <summary>
    /// 코인를 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 코인 개수</param>
    /// <returns>사용 성공 여부 (true : 성공적으로 사용함, false : 개수 부족)</returns>
    public bool UseCoin(uint count)
    {
        bool result = true;

        if (CoinAmount <= 0)
        {
            result = false;
        }
        else
        {
            CoinAmount -= count;
        }

        return result;
    }

    /// <summary>
    /// 폭탄 개수 증가 함수 (획득)
    /// </summary>
    /// <param name="count">획득량</param>
    public void GetBomb(uint count)
    {
        BombCount += count;
    }

    /// <summary>
    /// 폭탄 사용할 때 개수 감소하는 함수
    /// </summary>
    /// <param name="count">감소시킬 폭탄 개수</param>
    /// <returns>사용 성공 여부 (true : 성공적으로 사용함, false : 개수 부족)</returns>
    public bool UseBomb(uint count = 1)
    {
        bool result = true;

        if (BombCount <= 0)
        {
            result = false;
        }
        else
        {
            BombCount -= count;
        }

        return result;
    }

    /// <summary>
    /// 속도를 증가 시키는 함수
    /// </summary>
    /// <param name="value">증가량</param>
    public void InCreaseSpeed(float value)
    {
        addtionalSpeed += value;
    }

    /// <summary>
    /// 최대 체력 증가 함수
    /// </summary>
    /// <param name="value">증가량</param>
    public void InCreaseMaxHealth(float value)
    {
        maxHealth += value;
    }

    // 기타 함수 =============================================================

    /// <summary>
    /// 감지된 오브젝트를 반환하는 함수 ( 배열 )
    /// </summary>
    public GameObject[] GetDetectedObjects()
    {
        return sensor.detectedObjects.ToArray();
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