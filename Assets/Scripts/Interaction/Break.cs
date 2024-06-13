using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Break : MonoBehaviour, IBreakable
{
    Rigidbody[] reactionObjs;

    public void OnBreak(Transform startPoint)
    {
        foreach (var obj in reactionObjs)
        {
            obj.constraints = RigidbodyConstraints.None;
            obj.AddForce(obj.transform.position - startPoint.position * 3f, ForceMode.Impulse);
        }
    }

    void Awake()
    {
        Transform child = transform.GetChild(1);
        reactionObjs = child.GetComponentsInChildren<Rigidbody>();
    }
}