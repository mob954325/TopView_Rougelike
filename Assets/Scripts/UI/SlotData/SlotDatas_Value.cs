using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 단순한 수치만 증가하는 슬롯 데이터 클래스
/// </summary>
[CreateAssetMenu(fileName = "SlotData_999_", menuName = "ScriptableObjects/UI/SlotData_Value", order = 3)]

public class SlotDatas_Value : SlotDatas
{
    [Header("증가량 정보")]
    public float value;

    public override void Upgrade(Player player)
    {
        switch(slotType)
        {
            case UpgradeSlotType.Health:
                player.InCreaseMaxHealth(Mathf.Floor(value));
                break;
            case UpgradeSlotType.Attack:
                player.IncreaseAttackPower(value);
                break;
            case UpgradeSlotType.Defence:
                player.IncreaseDefencePower(value);
                break;
            case UpgradeSlotType.Speed:
                player.InCreaseSpeed(value);
                break;
        }
    }
}