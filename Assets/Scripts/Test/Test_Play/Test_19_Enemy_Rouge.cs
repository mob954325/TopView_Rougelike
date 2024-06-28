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
        target = transform.GetChild(0);
        GameManager.Instance.SpawnPlayer(Vector3.zero);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Rouge, target.transform.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Mage, target.transform.position, Quaternion.identity);        base.OnTest2(context);
    }
}
#endif