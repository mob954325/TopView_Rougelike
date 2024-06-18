using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Gate : LockedObject
{
    int HashToClose = Animator.StringToHash("Close");

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
        animator.SetTrigger(HashToOpen);
    }

    /// <summary>
    /// 문을 닫는 함수
    /// </summary>
    public void ForcedClose()
    {
        animator.SetTrigger(HashToClose);
    }
}