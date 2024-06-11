using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Weapon_Staff : WeaponBase
{
    /// <summary>
    /// 파이어볼 프리팹 함수
    /// </summary>
    public GameObject fireBall;

    /// <summary>
    /// 이 스크립트를 가지고 있는 최상위 오브젝트 (root)
    /// </summary>
    GameObject root;

    protected override void Awake()
    {
        Owner = GetComponentInParent<IBattler>(); // 재사용 중, 수정해야함
        root = transform.root.gameObject;
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
        if(fireBall == null)    // 소환할 프리팹이 비어있을 때 
        {
            Debug.LogError($"{root.name}의 소환 할 프리팹이 존재하지 않습니다.");
            return; 
        }

        Vector3 spawnPosition = root.transform.position + transform.up * 1.5f;
        GameObject obj = Instantiate(fireBall);
        obj.transform.position = spawnPosition;

        // Projectile 스크립트에 접근 후 공격 시작
        ProjectileBase projectile = obj.GetComponent<ProjectileBase>();
        if (projectile == null)
        {
            // 없으면 더미 오브젝트 소환
            projectile = obj.AddComponent<ProjectileBase>();
            obj.name = $"Dummy Projectile Object ( Created )";
        }

        float totalDamage = Owner.AttackPower;  // 투사체 공격력

        projectile.SetDestination(targetPosition, totalDamage);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // 사용 안함
    }
}