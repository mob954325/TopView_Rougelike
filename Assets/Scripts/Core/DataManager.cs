using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 데이터 관리 매니저
/// </summary>
public class DataManager : Singleton<DataManager>
{
    /// <summary>
    /// 아이템 정보가 저장되어있는 배열 (아이템 코드 순 정렬)
    /// </summary>
    public ItemData[] itemDatas;

    /// <summary>
    /// 능력 저장 배열 ( 임시 )
    /// </summary>
    public AbilityData[] abilityDatas;

    /// <summary>
    /// 슬롯 데이터 배열
    /// </summary>
    public SlotDatas[] slotDatas;
}