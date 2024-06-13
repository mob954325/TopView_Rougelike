using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IUseable
{
    Animator animator;

    int HashToOpen = Animator.StringToHash("Open");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 상자랑 상호작용 할 때 호출되는 함수
    /// </summary>
    public void OnUse()
    {
        animator.SetTrigger(HashToOpen);
    }
}