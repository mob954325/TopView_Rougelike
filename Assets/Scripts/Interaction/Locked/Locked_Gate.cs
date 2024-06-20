using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Gate : LockedObject
{
    Collider coll;

    int HashToClose = Animator.StringToHash("Close");

    /// <summary>
    /// 방을 통과 할 때 호출되는 델리게이트
    /// </summary>
    public Action onPassDoor;

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPassDoor?.Invoke();
            SetColliderEnable(false);
        }
    }

    protected override void BeforeUse()
    {
        //
    }

    protected override void AfterOpen()
    {
        // 사용 안함
    }

    /// <summary>
    /// 문을 강제로 여는 함수 
    /// </summary>
    public void ForcedOpen()
    {
        SetIsOpen(true);
        animator.SetBool(HashToClose, false);
        animator.SetTrigger(HashToOpen);
    }

    /// <summary>
    /// 문을 닫는 함수
    /// </summary>
    public void ForcedClose()
    {
        animator.SetBool(HashToClose, true);
    }

    /// <summary>
    /// 문의 트리거 콜라이더 활성화, 비활성화 하는 함수
    /// </summary>
    /// <param name="value">true면 활셩화 false면 비활성화</param>
    public void SetColliderEnable(bool value)
    {
        coll.enabled = value;
    }
}