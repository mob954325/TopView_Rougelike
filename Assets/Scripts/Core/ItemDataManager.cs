using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터 관리 매니저
/// </summary>
public class ItemDataManager : Singleton<ItemDataManager>
{
    /// <summary>
    /// 아이템 정보가 저장되어있는 배열 (아이템 코드 순 정렬)
    /// </summary>
    public ItemData[] itemDatas;
}