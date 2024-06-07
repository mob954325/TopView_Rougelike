#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_01_Battle : TestBase, IHealth ,IBattler
{
    public float maxHp = 100000;
    public float hp;

    public float AttackPower { get; set; }
    public float DefencePower { get; set; }
    public float CurrentHealth { get; set; }

    public float MaxHealth => maxHp;

    public Action onDie { get; set; }

    protected override void OnEnable()
    {
        hp = maxHp;
    }

    public void Attack(IBattler target)
    {
        Debug.Log("공격");
    }

    public void Hit(float hitDamage)
    {
        Debug.Log($"{gameObject.name} : 피격 받음, 남은 체력 : {hp}");
        hp -= hitDamage;
    }
}
#endif