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

    /// <summary>
    /// 감지된 오브젝트 리스트
    /// </summary>
    public List<GameObject> detectedObjects;

    void OnTriggerEnter(Collider other)
    {
        OnDetectObject(other);
    }

    void OnTriggerStay(Collider other)
    {
        OnObjectStay(other);
    }

    void OnTriggerExit(Collider other)
    {
        detectedObjects.Remove(other.gameObject);
    }

    /// <summary>
    /// 오브젝트가 감지될 때 실행되는 함수
    /// </summary>
    /// <param name="other">트리거된 콜라이더</param>
    public virtual void OnDetectObject(Collider other)
    {
        // 트리거될 때 실행될 내용 작성
        detectedObjects.Add(other.gameObject);
    }

    /// <summary>
    /// 센서 안에 있을 때 실행할 내용이 있는 함수
    /// </summary>
    /// <param name="other">트리거된 콜라이더</param>
    public virtual void OnObjectStay(Collider other)
    {

    }
}