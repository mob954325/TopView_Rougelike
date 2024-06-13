using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    /// <summary>
    /// 파괴할 때 호출되는 함수
    /// </summary>
    /// <param name="startPoint">파괴를 하는 오브젝트 위치</param>
    public void OnBreak(Transform startPoint);
}