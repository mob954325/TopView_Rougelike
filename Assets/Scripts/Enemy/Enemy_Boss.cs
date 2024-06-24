using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : EnemyBase, IHealth, IBattler
{
    // 구현할 기능
    // 1. 스폰 시작
    // 행동순서 
    // 플레이어에게 돌진한다 -> 내려 찍기 ( 광역 공격 ) -> 대기 -> 공격 ( 랜덤 )

    // 공격 종류
    // 내려 찍기 ( jump chop )
    // 잡몹 소환 ( Taunt )

    public float CurrentHealth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public float MaxHealth => throw new NotImplementedException();

    public Action onDie { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float AttackPower { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float DefencePower { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // 생명 함수 ==============================================================================================

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    // 행동 함수 ==============================================================================================

    protected override void OnIdle()
    {
        base.OnIdle();
    }
    protected override void OnReady()
    {
        base.OnReady();
    }

    protected override void OnTracing()
    {
        base.OnTracing();
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    public void Attack(IBattler target)
    {
        throw new NotImplementedException();
    }

    public void Hit(float hitDamage)
    {
        throw new NotImplementedException();
    }
}
