using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 투사체를 가지고 있는 클래스
/// </summary>
public class Ability : MonoBehaviour
{
    [SerializeField] AbilityData data;

    /// <summary>
    /// 능력 데이터 접근 프로퍼티
    /// </summary>
    public AbilityData Data => data;

    /// <summary>
    /// 대미지
    /// </summary>
    public float damage;

    /// <summary>
    /// 회전 속도
    /// </summary>
    public float rotateSpeed;

    /// <summary>
    /// 쿨타임
    /// </summary>
    public float coolTime;

    /// <summary>
    /// 투사체 배열
    /// </summary>
    public GameObject[] projectile;

    /// <summary>
    /// 활성화된 개수
    /// </summary>
    public int activeCount = 0;

    /// <summary>
    /// 능력 초기화 함수
    /// </summary>
    /// <param name="data">저장할 능력 데이터</param>
    public void Initialize(AbilityData data)
    {
        this.transform.localPosition = Vector3.zero; // 위지 초기화

        this.data = data;
        this.damage = data.damage;
        this.rotateSpeed = data.speed;
        this.coolTime = data.coolTime;
        projectile = new GameObject[data.maxCount];

        // 최대 개수 만큼 다 만들고 
        for(int i = 0; i < data.maxCount; i++)
        {
            SpawnProjectile(data.code, i);

            if(i > data.minCount - 1)   // min개수만 활성화
            {   
                projectile[i].SetActive(false); // 나머지 비활성화
            }
        }
    }

    /// <summary>
    /// 능력 투사체를 추가하는 함수 ( 1개 )
    /// </summary>
    /// <returns></returns>
    public void AddProjectile()
    {
        if (activeCount >= data.maxCount) // 투사체 최대 개수면 무시
            return;

        projectile[activeCount].SetActive(true);    // 투사체 활성화
        activeCount++;                              // 활성화 개수 추가

        RefreshAllProjectile();
    }

    /// <summary>
    /// 능력 투사체를 최대개수만큼 생성하고 하는 함수
    /// </summary>
    /// <param name="code">능력 코드</param>
    /// <param name="index">배열 인덱스 값</param>
    void SpawnProjectile(AbilityCode code, int index)
    {
        AbilityObjectBase projectile = Instantiate(data.projectilePrefab, this.transform).GetComponent<AbilityObjectBase>(); // 투사체 추가

        projectile.Initialize(rotateSpeed, damage);         // 오브젝트 생성
        this.projectile[index] = projectile.gameObject;     // 배열 저장

        activeCount = data.minCount;    // 활성화된 투사체 개수 초기화

        projectile.spawnVector = SetLocalPositionByAngle(index);
    }

    /// <summary>
    /// 모든 투사체 위치 초기화 함수
    /// </summary>
    void RefreshAllProjectile()
    {
        for(int i = 0; i < projectile.Length; i++)
        {
            SetLocalPositionByAngle(i);
        }
    }

    /// <summary>
    /// 각도에 따라 투사체 위치를 설정하는 함수
    /// </summary>
    /// <param name="index">투사체 인덱스</param>
    Vector3 SetLocalPositionByAngle(int index)
    {
        // 투사체 위치
        float angle = 360f / activeCount;

        Quaternion betweenAngle = Quaternion.Euler(0f, angle * index, 0f);            // 투사체 별 각도

        Vector3 spawnPoint = betweenAngle * Vector3.forward;                          // 위치 계산
        spawnPoint = new Vector3(spawnPoint.x, 0, spawnPoint.z);
        this.projectile[index].transform.localPosition = spawnPoint;                  // 위치 조정

        return spawnPoint;
    }

    /// <summary>
    /// 능력 투사체별 공격 실행 함수
    /// </summary>
    /// <param name="target">공격할 타겟 트랜스폼</param>
    public void Attack(Transform target)
    {
        foreach(var item in projectile)
        {
            item.SetActive(true);   // 활성화  

            AbilityObjectBase ablityObj = item.GetComponent<AbilityObjectBase>();
            item.transform.localPosition = ablityObj.spawnVector;   // 위치 초기화
            ablityObj.Attack(target);                               // 공격 실행
        }
    }
}