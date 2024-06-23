using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_14_UI : TestBase
{
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnItem(ItemCodes.Coin, transform.position, Quaternion.identity);
        Factory.Instance.SpawnItem(ItemCodes.Key, transform.position, Quaternion.identity);
        Factory.Instance.SpawnItem(ItemCodes.Bomb, transform.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyWarrior(Vector3.zero, Quaternion.identity);
    }
}