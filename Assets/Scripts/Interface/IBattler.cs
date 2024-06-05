using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattler
{
    /// <summary>
    /// 공격력 접근 및 수정 프로퍼티 
    /// </summary>
    public float AttackPower { get; set; }  

    /// <summary>
    /// 방어력 접근 및 수정 프로퍼티
    /// </summary>
    public float DefencePower { get; set; }

    /// <summary>
    /// 공격 시 호출되는 함수
    /// </summary>
    /// <param name="target">공격 받을 대상</param>
    public void Attack(IBattler target);
    
    /// <summary>
    /// 피격 시 호출되는 함수
    /// </summary>
    /// <param name="hitDamage">피격 데미지</param>
    public void Hit(float hitDamage);
}