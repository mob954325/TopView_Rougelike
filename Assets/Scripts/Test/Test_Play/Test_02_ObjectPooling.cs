using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_02_ObjectPooling : TestBase
{
    Transform t;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        t = transform.GetChild(0);
        Factory.Instance.GetTestItem(t.position);
    }
}