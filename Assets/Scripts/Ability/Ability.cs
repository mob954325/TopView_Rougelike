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
            SpawnProjectile(data.code);
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
        for(int i = 0; i < activeCount; i++)
        {
            projectile[i].GetComponent<AbilityObjectBase>().Initialize(rotateSpeed, damage, activeCount + 1);

            // 위치 초기화
            this.projectile[activeCount].transform.localPosition =
                new Vector3(this.projectile[activeCount].transform.localPosition.x, 0, this.projectile[activeCount].transform.localPosition.z);
        }
        activeCount++;
    }

    /// <summary>
    /// 능력 투사체 생성 함수
    /// </summary>
    /// <param name="code">능력 코드</param>
    /// <param name="count">생성할 회전 각도</param>
    /// <returns></returns>
    void SpawnProjectile(AbilityCode code, int count = 1)
    {
        //for(int i = 0; i < data.maxCount; i++)
        //{
        //    AbilityObjectBase projectile = Instantiate(data.projectilePrefab, this.transform).GetComponent<AbilityObjectBase>(); // 투사체 추가
        //    if(code == AbilityCode.Rotating)
        //    {
        //        projectile.Initialize(rotateSpeed, damage, count);      // 오브젝트 생성
        //        this.projectile[i] = projectile.gameObject;    // 배열 저장 -> y좌표 0으로 초기화해야함
        //
        //        this.projectile[i].transform.localPosition =
        //            new Vector3(this.projectile[i].transform.localPosition.x, 0 , this.projectile[i].transform.localPosition.z); // 투사체 y값 0으로 설정
        //    }
        //}
    }
}