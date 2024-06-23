using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 오브젝트 회전체 클래스
/// </summary>
public class AbilityObject_Rotate : AbilityObjectBase
{
    public override void Initialize(float speed, float damage)
    {
        root = transform.root;
        
        rotateSpeed = speed;
        this.damage = damage;      
    }

    public override void Attack(Transform target)
    {
        // 사용 안함
    }
    
    private void FixedUpdate()
    {
        transform.RotateAround(root.position, transform.up, rotateSpeed * Time.deltaTime);        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == root)
            return;

        IBattler battler = other.gameObject.GetComponent<IBattler>();

        if (battler != null)
        {
            battler.Hit(damage); // 공격
        }
    }
}