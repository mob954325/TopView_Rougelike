#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_17_Boss : TestBase
{
    public Enemy_Boss_Warrior boss;
    public BossHealthUI ui;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Warrior, Vector3.zero, Quaternion.identity, true);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyByCode(EnemyNormalType.Warrior, Vector3.zero, Quaternion.identity, false);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        boss = FindAnyObjectByType<Enemy_Boss_Warrior>();
        ui.Initialize(boss);
    }
}
#endif