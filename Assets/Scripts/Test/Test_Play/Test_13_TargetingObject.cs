#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_13_TargetingObject : TestBase
{
    public AbilityObject_Targeting obj;
    public Transform target;
    public AbilityContainer abilityContainer;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        obj.Initialize(3f, 1f);
        obj.Attack(target);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyWarrior(target.transform.position, Quaternion.identity);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        abilityContainer.AddAbility(AbilityCode.Targeting);
    }
}
#endif