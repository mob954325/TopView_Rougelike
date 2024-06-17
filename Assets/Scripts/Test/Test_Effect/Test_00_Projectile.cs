#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_00_Projectile : TestBase
{
    public ProjectileBase projectile;

    public Transform startTransform;
    public Transform goalTransform;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        // 오브젝트 생성 및 초기화
        GameObject obj = Instantiate(projectile).gameObject;
        obj.name = "Test";
        obj.GetComponent<ProjectileBase>().SetDestination(goalTransform.position);
        obj.transform.position = startTransform.position;
    }
}
#endif