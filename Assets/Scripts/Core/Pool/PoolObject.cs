using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool에서 사용할 오브젝트의 클래스
/// </summary>
public class PoolObject : MonoBehaviour
{
    /// <summary>
    /// 비활성화 될 때 호출되는 델리게이트
    /// </summary>
    public Action onDisable;

    protected virtual void OnDisable()
    {
        transform.position = Vector3.zero;  // 위치 초기화
        onDisable?.Invoke();                // 비활성화
    }

    /// <summary>
    /// 오브젝트 비활성화 코루틴
    /// </summary>
    /// <param name="time">비활성화 딜레이 타임</param>
    protected IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}