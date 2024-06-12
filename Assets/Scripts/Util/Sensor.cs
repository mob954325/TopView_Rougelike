using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 콜라이더로 무언가를 감지하는 오브젝트에 상속하는 클래스
/// </summary>
[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
    protected GameObject owner;

    void OnTriggerEnter(Collider other)
    {
        DetectObject(other);
    }

    /// <summary>
    /// 오브젝트가 트리거 될 때 실행되는 함수
    /// </summary>
    /// <param name="other"></param>
    public virtual void DetectObject(Collider other)
    {
        // 트리거될 때 실행될 내용 작성
    }
}