using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 잠긴 오브젝트가 가지는 클래스
/// </summary>
public class LockedObject : PoolObject, IUseable
{
    Animator animator;

    /// <summary>
    /// 열었는지 확인하는 변수
    /// </summary>
    bool isOpen = false;

    int HashToOpen = Animator.StringToHash("Open");

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 상자랑 상호작용 할 때 호출되는 함수
    /// </summary>
    public void OnUse(GameObject owner)
    {
        BeforeUse();

        if (isOpen)
            return;

        Player player = owner.GetComponent<Player>();

        if(player != null && player.UseKey()) // 작동 대상이 플레이어이다.
        {
            animator.SetTrigger(HashToOpen); // 상자 열기
            isOpen = true;

            // 아이템 받기 또는 아이템 소환
            AfterOpen();
        }
    }

    /// <summary>
    /// 상호작용 전 실행할 내용
    /// </summary>
    protected virtual void BeforeUse()
    {
    }

    /// <summary>
    /// 상호작용 이후 실행할 내용
    /// </summary>
    protected virtual void AfterOpen()
    {

    }
}