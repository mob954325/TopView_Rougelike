using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileBase : PoolObject
{
    Rigidbody rigid;
    Collider coll;

    /// <summary>
    /// 날라갈 지점
    /// </summary>
    Vector3 targetVec = Vector3.zero;

    /// <summary>
    /// 맞고 난 뒤 생성될 이펙트 오브젝트
    /// </summary>
    public GameObject EffecObject;

    /// <summary>
    /// 어딘가에 부딪혔을 때 실행되는 이펙트
    /// </summary>
    ParticleSystem HitEffect;    // Visual Effect 로 변환 고려 ( 성능 이슈 )

    /// <summary>
    /// 이 투사체의 공격력
    /// </summary>
    float damage;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void OnEnable()
    {
        DisableObject(5f);
    }

    void Start()
    {
        SetDestination(targetVec);        
    }

    void OnCollisionEnter(Collision collision)
    {
        // 이펙트 오브젝트 생성
        GameObject obj = Instantiate(EffecObject);
        obj.transform.position = transform.position;
        ParticleSystem particle = obj.GetComponent<ParticleSystem>();
        particle.Play();

        IBattler battleTarget = collision.gameObject.GetComponent<IBattler>();  // 충돌된 오브젝트가 IBattler를 가진 오브젝트면
        if (battleTarget != null)
        {
            battleTarget.Hit(damage);   // 공격
        }

        Destroy(obj, particle.time + 1f);    // 이펙트 오브젝트 제거 예약
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 투사체 목표 지점 설정 함수
    /// </summary>
    /// <param name="destinationVector">날라갈 위치값</param>
    public void SetDestination(Vector3 destinationVector, float damage = 1f)
    {
        targetVec = destinationVector;
        Vector3 dirVec = destinationVector - transform.position;
        rigid.AddForce(dirVec, ForceMode.Impulse);
        transform.LookAt(targetVec);

        this.damage = damage;
    }

    //float lifeTime = 2f;
    //
    //IEnumerator LifeTime()
    //{
    //    yield return new WaitForSeconds(lifeTime);
    //    gameObject.SetActive(false);
    //}
}