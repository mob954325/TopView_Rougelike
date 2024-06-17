#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Chest : TestBase
{
    public LockedObject chest;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        chest.OnUse(GameManager.Instance.player.gameObject);
    }
}
#endif