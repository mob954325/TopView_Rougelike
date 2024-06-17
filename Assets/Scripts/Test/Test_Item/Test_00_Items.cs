#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_00_Items : TestBase
{
    public ItemCodes code;

    Transform target;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        target = transform.GetChild(0);
        Factory.Instance.GetItem(code, target.position);
    }
}
#endif