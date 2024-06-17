using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_09_SpawnObjects : Test_08_GenerateMap
{
    Transform target;

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        target = transform.GetChild(0);
        Factory.Instance.SpawnChest(target.position, Quaternion.identity);
        Factory.Instance.SpawnItem(ItemCodes.Key, target.position, Quaternion.identity);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        generator.SpawnObjets();
    }
}