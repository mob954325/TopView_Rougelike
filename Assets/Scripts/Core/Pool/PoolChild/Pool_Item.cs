using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Item : Pool<ItemObject>
{
    /// <summary>
    /// 아이템 오브젝트 하나를 꺼내는 함수
    /// </summary>
    public GameObject GetItemObject(ItemCodes code, Vector3? position = null, Quaternion? rotation = null)
    {
        ItemObject obj = GetObject(position, rotation);
        obj.Initialize(ItemDataManager.Instance.itemDatas[(int)code]);  // 아이템 오브젝트 초기화

        return obj.gameObject;
    }
}