using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IUseable
{
    Animator animator;

    /// <summary>
    /// 열었는지 확인하는 변수
    /// </summary>
    bool isOpen = false;

    int HashToOpen = Animator.StringToHash("Open");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 상자랑 상호작용 할 때 호출되는 함수
    /// </summary>
    public void OnUse(GameObject owner)
    {
        if (isOpen)
            return;

        Player player = owner.GetComponent<Player>();

        if(player != null && player.UseKey()) // 작동 대상이 플레이어이다.
        {
            animator.SetTrigger(HashToOpen); // 상자 열기
            isOpen = true;

            // 아이템 받기 또는 아이템 소환
        }
    }
}