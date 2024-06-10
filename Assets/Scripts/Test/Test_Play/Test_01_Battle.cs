#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Battle : TestBase
{
    public Player player;
    public float value;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.CurrentHealth = value;
    }
}
#endif