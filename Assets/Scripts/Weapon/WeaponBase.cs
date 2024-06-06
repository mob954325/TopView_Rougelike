using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// 해당 무기를 사용하는 오브젝트의 IBattler
    /// </summary>
    IBattler Owner;

    Collider coll;

    void Awake()
    {
        Owner = GetComponentInParent<IBattler>();

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

    //OnCollisionEnter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) // 공격 대상이 존재한다
        {
            IBattler target = other.GetComponent<IBattler>();
            Debug.Log($"{gameObject.transform.parent.name}이 공격한 오브젝트 : {other.gameObject.name}");
            Owner.Attack(target);
        }
    }
}
