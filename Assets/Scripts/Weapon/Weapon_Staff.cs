using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Staff : WeaponBase
{
    /// <summary>
    /// 파이어볼 프리팹 함수
    /// </summary>
    public GameObject fireBall;

    /// <summary>
    /// 이 스크립트를 가지고 있는 최상위 오브젝트 (root)
    /// </summary>
    GameObject rootObj;

    /// <summary>
    /// 투사체 높이 ( 고정값 : 8f )
    /// </summary>
    const float projectileHeight = 8f;

    protected override void Awake()
    {
        Owner = GetComponentInParent<IBattler>(); // 재사용 중, 수정해야함
        rootObj = transform.root.gameObject;
    }

    public override void ActiveWeapon()
    {
        // 사용 안함
    }

    public override void InactiveWeapon()
    {
        // 사용 안함
    }

    /// <summary>
    /// 지팡이 클래스가 주문을 사용할 때 호출되는 함수
    /// </summary>
    /// <param name="targetPosition">공격할 위치</param>
    public void CastingSpell(Vector3 targetPosition)
    {
        Vector3 spawnPosition = rootObj.transform.position + transform.up * projectileHeight;
        GameObject obj = Factory.Instance.SpawnEnemyMage_Projectile(spawnPosition, Quaternion.identity);    // 투사체 소환

        // Projectile 스크립트에 접근 후 공격 시작
        ProjectileBase projectile = obj.GetComponent<ProjectileBase>();
        if (projectile == null)
        {
            // 없으면 더미 오브젝트 소환
            projectile = obj.AddComponent<ProjectileBase>();
            obj.name = $"Dummy Projectile Object ( Created )";
        }

        float totalDamage = Owner.AttackPower;  // 투사체 공격력

        projectile.SetDestination(rootObj, targetPosition, totalDamage); // 투사체 목적지 설정
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // 사용 안함
    }
}