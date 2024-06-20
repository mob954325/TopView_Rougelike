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

    WeaponBase sword; // 나중에 WeaponBase로 바꿀 예정

    bool isAttacking = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        sword = GetComponentInChildren<WeaponBase>();
    }

    private void Start()
    {
        player.playerInput.onAttack += OnAttack;
        player.playerInput.onHeavyAttack += OnHeavyAttack;
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