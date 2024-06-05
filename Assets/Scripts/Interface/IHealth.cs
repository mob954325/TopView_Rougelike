using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력관련 인터페이스
/// </summary>
public interface IHealth
{
    /// <summary>
    /// 체력 접근 및 수정 프로퍼티
    /// </summary>
    public float CurrentHealth { get; set; }

    /// <summary>
    /// 최대 체력 접근 프로퍼티
    /// </summary>
    public float MaxHealth { get; }

    /// <summary>
    /// 사망시 호출되는 델리게이트
    /// </summary>
    public Action onDie { get; set; }
}
