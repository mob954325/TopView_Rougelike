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
        // min개수만 활성화
        for(int i = 0; i < data.maxCount; i++)
        {
            SpawnProjectile(data.code, i);

            if(i < data.minCount)
            {
                projectile[i].SetActive(true);  // 첫번째 오브젝트 활성화
            }
        }
    }

    /// <summary>
    /// 능력 투사체를 추가하는 함수 ( 1개 )
    /// </summary>
    /// <returns></returns>
    public void AddProjectile()
    {
        // 추가된 개수 만큼 위치 조정 및 초기화
        projectile[activeCount].SetActive(true);
        projectile[activeCount].GetComponent<AbilityObjectBase>().Initialize(rotateSpeed, damage);

        // 위치 초기화
        this.projectile[activeCount].transform.localPosition =
            new Vector3(this.projectile[activeCount].transform.localPosition.x, 0, this.projectile[activeCount].transform.localPosition.z);
        activeCount++;

        RefreshProjectile();
    }

    /// <summary>
    /// 능력 투사체 생성 함수
    /// </summary>
    /// <param name="code">능력 코드</param>
    /// <param name="index">배열 인덱스 값</param>
    /// <param name="count">몇번째 투사체인지 index값</param>
    /// <returns></returns>
    void SpawnProjectile(AbilityCode code, int index)
    {
        AbilityObjectBase projectile = Instantiate(data.projectilePrefab, this.transform).GetComponent<AbilityObjectBase>(); // 투사체 추가

        if (code == AbilityCode.Rotating)
        {
            projectile.Initialize(rotateSpeed, damage);      // 오브젝트 생성
            this.projectile[index] = projectile.gameObject;    // 배열 저장 -> y좌표 0으로 초기화해야함

            this.projectile[index].transform.localPosition =
                new Vector3(this.projectile[index].transform.localPosition.x, 0, this.projectile[index].transform.localPosition.z); // 투사체 y값 0으로 설정
            this.projectile[index].SetActive(false);    // 비활성화
        }

        activeCount = data.minCount;    // 활성화된 투사체 개수 초기화

        // 투사체 위치
        float angle = 360f / activeCount;

        Quaternion betweenAngle = Quaternion.Euler(0f, angle * index, 0f);            // 투사체 별 각도

        Vector3 spawnPoint = betweenAngle * Vector3.forward;                                // 위치 계산
        spawnPoint = new Vector3(spawnPoint.x, 0, spawnPoint.z);
        this.projectile[index].transform.localPosition = spawnPoint;                        // 위치 조정
    }

    /// <summary>
    /// 모든 투사체 위치 초기화 함수
    /// </summary>
    void RefreshProjectile()
    {
        for(int i = 0; i < projectile.Length; i++)
        {
            //AbilityObjectBase obj = projectile[i].GetComponent<AbilityObjectBase>();

            // 투사체 위치
            float angle = 360f / activeCount;

            Quaternion betweenAngle = Quaternion.Euler(0f, angle * i, 0f);            // 투사체 별 각도

            Vector3 spawnPoint = betweenAngle * Vector3.forward;                                // 위치 계산
            spawnPoint = new Vector3(spawnPoint.x, 0, spawnPoint.z);
            this.projectile[i].transform.localPosition = spawnPoint;                            // 위치 조정
        }
    }
}