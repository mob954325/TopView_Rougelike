using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 업그래이드를 하는 슬롯 데이터 클래스
/// </summary>
public class SlotDatas_Ability : SlotDatas
{
    [Header("능력 정보")]
    public AbilityCode code;
    public override void Upgrade(Player player)
    {
        player.TryGetComponent(out Player_Battle playerBattle); // 플레이어 전투 스크립트 찾기
        if (playerBattle != null) 
        { 
            if(playerBattle.CheckAbilityContainerIsEmtpy())
            {
                playerBattle.GetAbliity(code);
            }
            else
            {
                playerBattle.UpgradeAbility(code);
            }
        }
    }
    // PlayerBattle -> container -> upgrade
}