using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Gate : LockedObject
{
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
}