using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileBase : PoolObject
{
    GameObject owner;

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
        //SetDestination(targetVec);        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == owner)
            return;

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
    /// <param name="owner">시전자</param>
    /// <param name="destinationVector">날라갈 위치값</param>
    /// <param name="damage">투사체 대미지</param>
    public void SetDestination(GameObject owner, Vector3 destinationVector, float damage = 1f)
    {
        this.owner = owner;
        targetVec = destinationVector;
        Vector3 dirVec = destinationVector - transform.localPosition;
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