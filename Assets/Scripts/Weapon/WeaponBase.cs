using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// 해당 무기를 사용하는 오브젝트의 IBattler
    /// </summary>
    protected IBattler Owner;

    protected Collider coll;

    protected virtual void Awake()
    {
        Owner = GetComponentInParent<IBattler>();

        coll = GetComponent<Collider>();  
        coll.enabled = false;   // 처음엔 비활성화
    }

    /// <summary>
    /// 공격을 시작할 때 호출되는 함수
    /// </summary>
    public virtual void ActiveWeapon()
    {
        coll.enabled = true;
    }

    /// <summary>
    /// 공격을 끝낼 때 호출되는 함수
    /// </summary>
    public virtual void InactiveWeapon()
    {
        coll.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // 임시 내용

        if (other.gameObject.CompareTag("Enemy")) // 공격 대상이 존재한다
        {
            IBattler target = other.GetComponent<IBattler>();
            Debug.Log($"{gameObject.transform.parent.name}이 공격한 오브젝트 : {other.gameObject.name}");
            Owner.Attack(target);
        }

        if (other.gameObject.CompareTag("Player")) // 공격 대상이 존재한다
        {
            IBattler target = other.GetComponent<IBattler>();
            Debug.Log($"{gameObject.transform.parent.name}이 공격한 오브젝트 : {other.gameObject.name}");
            Owner.Attack(target);
        }
    }
}
