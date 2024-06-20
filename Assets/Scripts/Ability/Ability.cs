using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] AbilityData data;

    public float damage;

    public float rotateSpeed;

    public float coolTime;

    public GameObject[] projectile;

    public int projectileCount = 0;

    /// <summary>
    /// 능력 초기화 함수
    /// </summary>
    /// <param name="data">저장할 능력 데이터</param>
    public void Initialize(AbilityData data)
    {
        this.data = data;

        this.damage = data.damage;
        this.rotateSpeed = data.speed;
        this.coolTime = data.coolTime;
        projectile = new GameObject[data.maxCount];

        CreateProjectileByType(data.code);
    }

    void CreateProjectileByType(AbilityCode code)
    {
        AbilityObject abilityObj = Instantiate(data.projecttile, this.transform).GetComponent<AbilityObject>(); // 투사체 추가
        if(code == AbilityCode.Rotating)
        {
            abilityObj.Initialize(rotateSpeed, damage);             // 오브젝트 생성
            abilityObj.transform.localPosition = new Vector3(2f, 0f, 0f);
            projectile[projectileCount] = abilityObj.gameObject;    // 배열 저장

            projectileCount++;                                      // 인덱스 증가
        }
    }
}