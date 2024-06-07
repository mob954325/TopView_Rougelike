using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 원거리 공격 기본 클래스
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RangeAttack_Base : MonoBehaviour
{
    Rigidbody rigid;
    Collider coll;

    /// <summary>
    /// 날라갈 방향
    /// </summary>
    public Vector3 dirVec = Vector3.zero;

    /// <summary>
    /// 어딘가에 부딪혔을 때 실행되는 이펙트
    /// </summary>
    public ParticleSystem HitEffect;    // Visual Effect 로 변환 고려 ( 성능 이슈 )

    /// <summary>
    /// 초기화 함수
    /// </summary>
    /// <param name="dir"></param>
    public void Initialize(Vector3 dir)
    {

    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // 날라갈 방향 기즈모 표시
    }
#endif
}