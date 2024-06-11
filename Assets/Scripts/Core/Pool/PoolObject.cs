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

    void OnDisable()
    {
        onDisable?.Invoke();
    }
}