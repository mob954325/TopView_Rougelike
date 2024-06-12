#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03_Death : TestBase
{
    public GameObject[] target;
    IHealth[] targetHealths;

    void Start()
    {
        targetHealths = new IHealth[target.Length];
    }
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        for(int i = 0; i < target.Length; i++)
        {
            targetHealths[i] = target[i].GetComponent<IHealth>();
            targetHealths[i].CurrentHealth = 0f;
        }
    }
}
#endif