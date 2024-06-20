using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityContainer : MonoBehaviour
{
    IBattler ownerBattler;

    private void Awake()
    {
        ownerBattler = GetComponentInParent<IBattler>();
    }

    // 해당 능력을 얻으면 팩토리에서 생성
    public void AddAbility(AbilityCode code) 
    {
        // 능력 오브젝트 생성
        GameObject obj = new GameObject("Test");
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        Ability ability = obj.AddComponent<Ability>(); // 능력 스크립트 추가

        ability.Initialize(ItemDataManager.Instance.abilityDatas[(int)code]); // 초기화
    }
}