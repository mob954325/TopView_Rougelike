#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_16_SpawnEnemyByType : TestBase
{
    public EnemyNormalType type;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyByCode(type, Vector3.zero, Quaternion.identity);
    }
}
#endif