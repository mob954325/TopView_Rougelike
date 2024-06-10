using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 전투관련 함수를 다루는 베이스 클래스
/// </summary>
public class EnemyBattleBase
{
    /// <summary>
    /// 공격 시작시 호출되는 클래스
    /// </summary>
    public virtual void BeginAttack()
    {
        AttackProcessing();
    }

    /// <summary>
    /// 공격 종료시 호출되는 클래스
    /// </summary>
    public virtual void EndAttack() 
    { 

    }

    /// <summary>
    /// 공격 시작시 진행처리 함수
    /// </summary>
    public virtual void AttackProcessing()
    {

    }
}
