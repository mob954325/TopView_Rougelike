#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_08_GenerateMap : TestBase
{
    public MapGenerator generator;

    public int x;
    public int y;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        generator.Initialize(x, y);
    }
}
#endif