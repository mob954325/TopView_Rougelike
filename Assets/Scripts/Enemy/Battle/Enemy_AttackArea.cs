using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// 적  Attack_Area에서 실행되는 클래스 
/// </summary>
public class Enemy_AttackArea : MonoBehaviour
{
    /// <summary>
    /// 현재 오브젝트를 가지고 있는 오브젝트의 IBattler
    /// </summary>
    IBattler Owner;

    Collider coll;

    void Awake()
    {
        Owner = GetComponentInParent<IBattler>();
        coll = GetComponent<Collider>();

        DeactiveCollider(); // 활성화 방지용
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IBattler target))
        {
            //Debug.Log($"{other.gameObject.name}");
            Owner.Attack(target);
        }
    }

    /// <summary>
    /// 콜라이더 활성화 함수
    /// </summary>
    public void ActiveCollider()
    {
        coll.enabled = true;
    }

    /// <summary>
    /// 콜라이더 비활성화 함수
    /// </summary>
    public void DeactiveCollider()
    {
        coll.enabled = false;
    }
}