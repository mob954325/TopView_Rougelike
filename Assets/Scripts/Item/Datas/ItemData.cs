using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_999_", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemData : ScriptableObject
{

    /// <summary>
    /// 해당 아이템 코드
    /// </summary>
    public ItemCodes code;

    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string itemName;

    /// <summary>
    /// 아이템 설명
    /// </summary>
    public string desc;

    /// <summary>
    /// 최대 개수
    /// </summary>
    public uint maxCount;

    /// <summary>
    /// 아이템의 개수 (0이면 획득만되는 아이템)
    /// </summary>
    public uint count;

    /// <summary>
    /// 아이텝 드랍 프리팹
    /// </summary>
    public GameObject dropPrefab;
}