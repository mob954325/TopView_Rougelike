using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_20_GenerateText : TestBase
{
    Transform target;

    void Start()
    {
        target = transform.GetChild(0);
        GameManager.Instance.SpawnPlayer(Vector3.zero);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnText(target.position, Color.blue, "[TEST]", 20f);
    }
}