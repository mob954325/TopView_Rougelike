#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_18_DeadScene : TestBase
{
    public Player player;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.CurrentHealth = 0;
    }
}
#endif