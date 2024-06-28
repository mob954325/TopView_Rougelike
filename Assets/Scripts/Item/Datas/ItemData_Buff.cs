using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_Buff", menuName = "ScriptableObjects/Item/Item-Buff", order = 4)]
public class ItemData_Buff : ItemData, IGetable
{
    public BuffType buffType;

    [Header("능력치 증가량")]
    [Tooltip("공격력 증가량")]
    public float attackPowerIncrease;

    [Tooltip("방어력 증가량")]
    public float defenceIncrease;

    [Tooltip("속도 증가량")]
    public float speedIncrease;

    [Tooltip("체력 증가량")]
    public float healthIncrease;

    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        if(player != null)
        {
            for(int i = 0; i < typeof(BuffType).GetEnumValues().Length; i++)    
            {
                int result = (int)buffType & (1 << i); // 무슨 버프인지 확인

                if (result != 1 << i)
                    continue;

                GetBuff(player, result);
            }
        }
    }

    /// <summary>
    /// 버프를 선택해서 증가시키는 함수
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="result">비교한 비트 결과값</param>
    /// <returns></returns>
    private bool GetBuff(Player player, int bitResult)
    {
        bool result = true;
        IBattler battler = player as IBattler;

        switch (bitResult)
        {            
            case 1:
                battler.AttackPower += attackPowerIncrease;
                Factory.Instance.SpawnText(player.BillBoardPosition, Color.black, $"{itemName} +{attackPowerIncrease}");
                break;
            case 2:
                battler.DefencePower += defenceIncrease;
                Factory.Instance.SpawnText(player.BillBoardPosition, Color.black, $"{itemName} +{defenceIncrease}");
                break;
            case 4:
                player.InCreaseSpeed(speedIncrease);
                Factory.Instance.SpawnText(player.BillBoardPosition, Color.black, $"{itemName} +{speedIncrease}");
                break;
            case 8:
                player.InCreaseMaxHealth(healthIncrease);
                Factory.Instance.SpawnText(player.BillBoardPosition, Color.black, $"{itemName} +{healthIncrease}");
                break;
            default:
            result = false;
                break;
        }

        return result;
    }
}