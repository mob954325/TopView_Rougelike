using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 코드 ( 오름차순 초기화 )
/// </summary>
public enum ItemCodes
{
    Dummy = 0,
    Key,
    Coin,
    Bomb,
    Buff_Attack,
    Buff_Defence,
    Buff_Speed,
    Buff_Health
}

/// <summary>
/// 버프 타입 ( 비트 플래그 )
/// </summary>
[Flags]
public enum BuffType : byte
{
    Damage = 1,
    Defence = 2,
    Speed = 4,
    Health = 8,
}