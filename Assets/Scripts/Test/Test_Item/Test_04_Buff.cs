#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Buff : TestBase
{
    public Transform t;

    private void Start()
    {
        GameManager.Instance.SpawnPlayer(t.position);        
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnItem(ItemCodes.Buff, t.position, Quaternion.identity);
    }
}
#endif