using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_Coin", menuName = "ScriptableObjects/Item", order = 2)]
public class ItemData_Coin : ItemData, IGetable
{
    /// <summary>
    /// 골드량 증가
    /// </summary>
    /// <param name="owner">값을 증가 시킬 오브젝트</param>
    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        player.GetGold(count);
    }
}