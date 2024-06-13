using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Break : TestBase
{
    public Break _break;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        _break.OnBreak();
    }
}