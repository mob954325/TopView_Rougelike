using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 오브젝트 데이터
/// </summary>
[CreateAssetMenu(fileName = "Ability_99_Empty", menuName = "ScriptableObjects/Ability/Ability", order = 1)]
public class AbilityData : ScriptableObject
{
    [Header("능력 정보")]
    /// <summary>
    /// 능력 코드
    /// </summary>
    public AbilityCode code;

    /// <summary>
    /// 능력 이름
    /// </summary>
    public string abilityName;

    /// <summary>
    /// 능력 설명
    /// </summary>
    public string abilityDesc;

    [Header("값 정보")]
    /// <summary>
    /// 데미지
    /// </summary>
    public float damage;

    /// <summary>
    /// 투사체 스피드
    /// </summary>
    public float speed;

    /// <summary>
    /// 능력 쿨타임
    /// </summary>
    public float coolTime;

    /// <summary>
    /// 최소 개수
    /// </summary>
    public int minCount;

    /// <summary>
    /// 최대 개수
    /// </summary>
    public int maxCount;

    /// <summary>
    /// 데미지 증가량 ( 활성화 된 투사체 개수가 최대일 때 이만큼 증가 )
    /// </summary>
    public float increaseDamageValue;

    /// <summary>
    /// 투사체 프리팹 오브젝트
    /// </summary>
    public GameObject projectilePrefab;
}