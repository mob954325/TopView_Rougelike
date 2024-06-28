#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Break : TestBase
{
    public Wall_Breakable _break;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        //Debug.Log("누름");
        _break.OnBreak(transform.GetChild(0));
    }
}
#endif