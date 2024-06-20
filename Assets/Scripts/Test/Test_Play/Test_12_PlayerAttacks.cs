using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_12_PlayerAttacks : TestBase
{
    Transform target;

    private void Start()
    {
        target = transform.GetChild(0);
        GameManager.Instance.SpawnPlayer(Vector3.zero);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyMage(target.position, Quaternion.identity);
    }
}