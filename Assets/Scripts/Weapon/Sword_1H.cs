using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Sword_1H : MonoBehaviour
{
    Collider coll;

    /// <summary>
    /// 감지한 공격 대상
    /// </summary>
    IBattler target; // 범위 공격 고려안함

    void Awake()
    {
        coll = GetComponent<Collider>();    
        coll.enabled = false;   // 처음엔 비활성화
    }

    /// <summary>
    /// 칼 콜라이더 활성화 함수
    /// </summary>
    public void ActiveCollider()
    {
        coll.enabled = true;
    }

    /// <summary>
    /// 칼 콜라이더 비활성화 함수
    /// </summary>
    public void InactiveCollider()
    {
        coll.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        IBattler target = other as IBattler;
        if (target != null) // 공격 대상이 존재한다
        {
            this.target = target;
            Debug.Log($"플레이어 칼에 닿은 오브젝트 : {other.gameObject.name}");
        }
    }

    /// <summary>
    /// 공격 대상을 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public IBattler GetTarget()
    {
        return target;
    }
}
