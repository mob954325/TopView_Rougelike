using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Player))]
public class Player_Battle : MonoBehaviour
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 소지하고 있는 무기
    /// </summary>
    WeaponBase sword;

    /// <summary>
    /// 능력 모음 오브젝트
    /// </summary>
    AbilityContainer abilityContainer;

    float coolTime = 0.0f;

    bool isAttacking = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        sword = GetComponentInChildren<WeaponBase>();
        abilityContainer = GetComponentInChildren<AbilityContainer>();
    }

    private void Start()
    {
        player.playerInput.onAttack += OnAttack;
        player.playerInput.onHeavyAttack += OnHeavyAttack;
    }

    private void FixedUpdate()
    {
        coolTime += Time.fixedDeltaTime;

        if (coolTime > 1f)
        {
            coolTime = 0f;
            AbilityAttack();
        }
    }

    /// <summary>
    /// 공격키 입력시 호출되는 함수 ( 왼쪽 마우스 버튼 )
    /// </summary>
    private void OnAttack()
    {
        // 공격 시 실행 내용
        player.OnAttack();
    }

    /// <summary>
    /// 강공격키 입력시 호출되는 함수 ( 오른쪽 마우스 버튼 )
    /// </summary>
    private void OnHeavyAttack()
    {
        player.OnHeavyAttack();
    }

    public void AbilityAttack()
    {
        GameObject[] objs = player.GetDetectedObjects();
        GameObject target = null;
        float minLength = float.MaxValue;

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].GetComponent<EnemyBase>() != null) // 적이면 타겟으로 지정 ( 첫번째로 지정된 타겟 )
            {
                if(target == null)  // target이 없으면 그 대상을 target으로 지정
                {
                    target = objs[i];
                    minLength = (target.transform.position - transform.position).sqrMagnitude;
                }
                else
                {
                    float checkLength = Mathf.Min((objs[i].transform.position - transform.position).sqrMagnitude, (target.transform.position - transform.position).sqrMagnitude);
                    if(checkLength < minLength) // 체크한 값이 더 낮다 == 체크한 오브젝트가 더 가깝다 -> target 업데이트
                    {
                        target = objs[i];
                    }
                }
            }
        }

        foreach(var ability in abilityContainer.abilities)
        {
            if (ability != null && target != null)
            {
                ability.Attack(target.transform);
            }
        }
    }
    // 능력 관련 함수 =============================================================

    public void GetAbliity(AbilityCode code)
    {
        abilityContainer.AddAbility(code);
    }

    public void UpgradeAbility(AbilityCode code)
    {
        // index번째 능력과 code가 같으면 해당 인덱스번 업그래이드
        int index = 0;
        foreach(var item in abilityContainer.abilities)
        {
            if(item.Data.code == code)
            {
                break;
            }
            index++;
        }

        if (index > abilityContainer.abilities.Length - 1)   // 해당 능력은 존재하지않는다.
            return;

        abilityContainer.UpGradeAbiliy(index);
    }

    /// <summary>
    /// 능력을 가지고 있는지 확인하는 변수
    /// </summary>
    /// <param name="code">확인할 능력 코드</param>
    /// <returns>있으면 true 없으면 false</returns>
    public bool CheckHasAbility(AbilityCode code)
    {
        bool result = false;

        foreach(var item in abilityContainer.abilities)
        {
            if (item == null) break;        // 데이터가 없으면 false

            if (item.Data.code == code)     // 데이터가 있는데 같은 능력 코드면 true
            {
                result = true;
                break;
            }
        }


        return result;
    }

    // 애니메이션 함수 =============================================================
    public void BeginAttack()
    {
        isAttacking = true; // 공격 진행중 true로 설정

        player.BeginAttackAnim(isAttacking);
        sword.ActiveWeapon();
    }

    /// <summary>
    /// 공격이 종료되면 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void EndAttack()
    {
        isAttacking = false;

        player.EndAttackAnim(isAttacking);
        sword.InactiveWeapon();
    }
}