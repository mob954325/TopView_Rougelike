using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Breakable : MonoBehaviour, IBreakable
{
    Rigidbody[] reactionObjs;

    /// <summary>
    /// 해당 벽이 파괴되었는지 확인하는 변수
    /// </summary>
    bool isBreak = false;

    void Awake()
    {
        Transform child = transform.GetChild(1);
        reactionObjs = child.GetComponentsInChildren<Rigidbody>();
    }

    /// <summary>
    /// 파괴할 때 호출되는 함수
    /// </summary>
    /// <param name="startPoint"></param>
    public void OnBreak(Transform startPoint)
    {
        if (isBreak)
            return;

        foreach (var obj in reactionObjs)
        {
            // 각 벽돌 날리기
            obj.constraints = RigidbodyConstraints.None;
            obj.AddForce((obj.transform.position - startPoint.position).normalized * 3f, ForceMode.Impulse);
            obj.gameObject.GetComponent<Brick>().ActiveDisable();
        }
        isBreak = true;
    }
}