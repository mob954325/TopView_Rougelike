using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력 오브젝트 베이스 클래스
/// </summary>
public abstract class AbilityObjectBase : MonoBehaviour
{
    /// <summary>
    /// 이 오브젝트의 루트 오브젝트
    /// </summary>
    protected Transform root;

    public Vector3 spawnVector;

    /// <summary>
    /// 회전속도
    /// </summary>
    protected float rotateSpeed;

    /// <summary>
    /// 이 투사체의 대미지
    /// </summary>
    protected float damage;

    /// <summary>
    /// 오브젝트 초기화
    /// </summary>
    /// <param name="damage">대미지</param>
    /// <param name="speed">회전 및 이동 속도</param>
    public abstract void Initialize(float speed, float damage);

    /// <summary>
    /// 능력별 구현한 공격 함수
    /// </summary>
    /// <param name="target">공격 목표</param>
    public abstract void Attack(Transform target);
}