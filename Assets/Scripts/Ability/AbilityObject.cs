using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 베이스 클래스 ( 임시 )
/// </summary>
public class AbilityObject : MonoBehaviour
{
    Transform root;

    float rotateSpeed = 0f;

    float damage = 0f;

    /// <summary>
    /// 능력 오브젝트 초기화 함수
    /// </summary>
    /// <param name="speed">회전 속도</param>
    /// <param name="damage">물체 대미지</param>
    public void Initialize(float speed, float damage)
    {
        rotateSpeed = speed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        // parent 기준 회전 
        //parent = transform.parent;

        transform.RotateAround(transform.parent.position, Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IBattler battler = other.gameObject.GetComponent<IBattler>();
        Debug.Log($"{other.gameObject.name}");

        if(battler != null)
        {
            battler.Hit(damage); // 공격
        }
    }
}