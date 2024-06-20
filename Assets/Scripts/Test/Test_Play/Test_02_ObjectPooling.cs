#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_02_ObjectPooling : TestBase
{
    Transform t;

    private void Start()
    {
        t = transform.GetChild(0);        
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnBomb(t.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyMage(t.position, Quaternion.identity);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        GameManager.Instance.SpawnPlayer(t.position);
    }
}
#endif