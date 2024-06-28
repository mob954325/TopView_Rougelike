#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_19_Enemy_Rouge : TestBase
{
    Transform target;

    void Start()
    {
        GameManager.Instance.SpawnPlayer(Vector3.zero);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
    }
}
#endif