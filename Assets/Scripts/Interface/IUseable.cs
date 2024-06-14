using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용 가능한 오브젝트에 상속되는 인터페이스 
/// </summary>
public interface IUseable
{
    /// <summary>
    /// 오브젝트를 사용할 때 호출되는 함수
    /// </summary>
    public void OnUse(GameObject owner);
}