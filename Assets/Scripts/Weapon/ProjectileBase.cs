using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    Rigidbody rigid;
    Collider coll;

    /// <summary>
    /// 날라갈 지점
    /// </summary>
    Vector3 destinationVec = Vector3.zero;

    /// <summary>
    /// 어딘가에 부딪혔을 때 실행되는 이펙트
    /// </summary>
    public ParticleSystem HitEffect;    // Visual Effect 로 변환 고려 ( 성능 이슈 )

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void OnEnable()
    {
        Destroy(this.gameObject, 3f);
    }

    void Start()
    {
        SetDestination(destinationVec);        
    }

    /// <summary>
    /// 투사체 초기화 함수
    /// </summary>
    /// <param name="dir">날라갈 방향</param>
    public void SetDestination(Vector3 destinationVector)
    {
        Vector3 dirVec = destinationVector - transform.position;
        rigid.AddForce(dirVec, ForceMode.Impulse);
    }

    float lifeTime = 2f;

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float thiccness = 3f;
        // 날라갈 방향 기즈모 표시
        Handles.color = Color.red;
        Vector3 p1 = transform.position; // 오브젝트 위치
        Vector3 p2 = (destinationVec - transform.position).normalized; // 날라갈 방향
        
        Handles.DrawLine(p1, p2, thiccness);
    }
#endif
}
