#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_15_UpgradeUI : TestBase
{
    public UpgradeUI upgradeUI;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        upgradeUI.OpenPanel();
    }
}
#endif