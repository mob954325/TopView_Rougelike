using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_Bomb", menuName = "ScriptableObjects/Item-Bomb", order = 4)]
public class ItemData_Bomb : ItemData, IGetable, IUseable
{
    /// <summary>
    /// 골드량 증가
    /// </summary>
    /// <param name="owner">값을 증가 시킬 오브젝트</param>
    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        player.GetBomb(count);
    }

    public void OnUse()
    {
        // 폭탄 터지는 내용
    }
}