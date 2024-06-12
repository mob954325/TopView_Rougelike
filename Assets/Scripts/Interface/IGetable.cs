using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 획득 가능한 오브젝트에 상속되는 인터페이스
/// </summary>
public interface IGetable
{
    /// <summary>
    /// 획득할 때 호출되는 실행되는 함수
    /// </summary>
    /// <param name="owner">획득하는 오브젝트</param>
    public void OnGet(GameObject owner);
}