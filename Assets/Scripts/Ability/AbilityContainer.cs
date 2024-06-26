using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    /// <summary>
    /// 활성화된 능력 배열
    /// </summary>
    public Ability[] abilities;

    private const int maxAbliityCount = 2;

    private int index = 0;

    void Awake()
    {
        abilities = new Ability[maxAbliityCount];
    }

    public void AddAbility(AbilityCode code) 
    {
        // 능력 오브젝트 생성
        GameObject obj = new GameObject($"Ability_{code}");
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;

        Ability ability = obj.AddComponent<Ability>(); // 능력 스크립트 추가
        ability.Initialize(DataManager.Instance.abilityDatas[(int)code]); // 초기화
        abilities[index] = ability;
        index++;
    }

    /// <summary>
    /// 능력 업그레이드 함수
    /// </summary>
    /// <param name="index">업그레이드할 능력 인덱스</param>
    public void UpGradeAbiliy(int index)
    {
        abilities[index].AddProjectile();
    }
}