using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Battle : MonoBehaviour
{
    Animator animator;

    bool isAttacking = false;

    /// <summary>
    /// 공격 시작 확인 파라미터 ( true : 공격, false : 공격 안함 ) 
    /// </summary>
    int HashToAttack = Animator.StringToHash("Attack");

    /// <summary>
    /// 공격을 하는 중인지 확인하는 프로퍼티 ( true : 공격중, false : 공격안하고있음 )
    /// </summary>
    int HashToIsAttacking = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAttack()
    {
        animator.SetBool(HashToAttack, true);   // 공격 시작
    }

    // 애니메이션 함수 =============================================================

    /// <summary>
    /// 공격이 종료되면 호출되는 애니메이션 이벤트 함수
    /// </summary>
    public void EndAttackAnimation()
    {
        isAttacking = false;

        animator.SetBool(HashToIsAttacking, isAttacking);
    }

    public void BeginAttack()
    {
        isAttacking = true; // 공격 진행중 true로 설정

        animator.SetBool(HashToAttack, false);
        animator.SetBool(HashToIsAttacking, isAttacking);
    }
}