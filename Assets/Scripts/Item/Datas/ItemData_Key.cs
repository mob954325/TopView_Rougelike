using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_Key", menuName = "ScriptableObjects/Item/Item-Key", order = 2)]
public class ItemData_Key : ItemData, IGetable, IUseable
{
    /// <summary>
    /// 골드량 증가
    /// </summary>
    /// <param name="owner">값을 증가 시킬 오브젝트</param>
    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        player.GetKey(count);
        Factory.Instance.SpawnText(player.BillBoardPosition, Color.black, $"{itemName} +{count}");
    }

    public void OnUse(GameObject owner)
    {
        // 열쇠 사용
    }
}