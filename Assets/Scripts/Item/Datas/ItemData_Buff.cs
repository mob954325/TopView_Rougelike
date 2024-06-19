using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_Buff", menuName = "ScriptableObjects/Item-Buff", order = 4)]
public class ItemData_Buff : ItemData, IGetable
{
    public BuffType buffType;

    [Tooltip("능력치 증가량")]
    public float value;

    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        if(player != null)
        {
            //
            IBattler battler = player as IBattler;

            int mask = 1;

            for(int i = 0; i < typeof(BuffType).GetEnumValues().Length; i++)    // 무슨 버프인지 확인
            {
                if (((int)buffType & mask) != 1 << i)
                    continue;
                
                // 임시 증가
                switch(i)
                {
                    case 1:
                        battler.AttackPower += value;
                        break;
                    case 2:
                        battler.DefencePower += value;
                        break;
                    case 3:
                        player.InCreaseSpeed(value);
                        break;
                    default: 
                        break;
                }

            }
        }
    }
}