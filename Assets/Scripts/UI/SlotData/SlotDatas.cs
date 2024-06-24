using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본 업그레이드 슬롯 데이터 클래스
/// </summary>
[CreateAssetMenu(fileName = "SlotData_999_", menuName = "ScriptableObjects/UI/SlotData", order = 1)]
public class SlotDatas : ScriptableObject
{
    [Header("기본 정보")]

    /// <summary>
    /// 해당 데이터의 슬롯 타입
    /// </summary>
    public UpgradeSlotType slotType;

    /// <summary>
    /// 슬롯 이름
    /// </summary>
    public string slotName;

    /// <summary>
    /// 슬롯 설명
    /// </summary>
    [TextArea]
    public string slotDesc;

    /// <summary>
    /// 업그래이드 내용을 실행하는 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    public virtual void Upgrade(Player player)
    {
        // 업그레이드 내용
    }
}