#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_12_PlayerAttacks : TestBase
{
    Transform target;

    public AbilityContainer abilityContainer;

    public AbilityCode code;

    private void Start()
    {
        target = transform.GetChild(0);
        //GameManager.Instance.SpawnPlayer(Vector3.zero);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Factory.Instance.SpawnEnemyMage(target.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        abilityContainer.AddAbility(code);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        abilityContainer.UpGradeAbiliy(0);
    }
}
#endif