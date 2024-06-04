using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player_Movement : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    /// <summary>
    /// 플레이어 현재 속도
    /// </summary>
    public float speed = 5.0f;

    /// <summary>
    /// 플레이어 걷는 속도 ( 게임 시작전 speed )
    /// </summary>
    private float walkSpeed = 5f;

    /// <summary>
    /// 달리기 속도
    /// </summary>
    public float sprintSpeed = 8f;

    /// <summary>
    /// 움직임 방향 벡터
    /// </summary>
    public Vector2 moveVector = Vector2.zero;

    /// <summary>
    /// 바라보는 방향 벡터
    /// </summary>
    public Vector3 lookVector = Vector3.zero;

    /// <summary>
    /// 애니메이터 Speed 파라미터
    /// </summary>
    int hashToSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        walkSpeed = speed;
    }

    private void FixedUpdate()
    {
        Move();                       
        transform.LookAt(lookVector);   // 플레이어가 바라보는 방향
    }

    /// <summary>
    /// 플레이어 이동 입력을 받는 함수
    /// </summary>
    /// <param name="inputValue">입력 값</param>
    public void OnMove(Vector2 inputValue)
    {
        moveVector = inputValue;
    }

    /// <summary>
    /// 플레이어 이동 실행 함수
    /// </summary>
    void Move()
    {
        // 월드 기준 상하좌우 움직임
        Vector3 verticalValue = moveVector.y * speed * Vector3.forward;         // 수직 벡터값
        Vector3 horizontalValue = moveVector.x * speed * Vector3.right;         // 수평 벡터값 

        Vector3 moveDir = verticalValue + horizontalValue;                      // 움직일 방향 값

        rigid.MovePosition(transform.position + Time.fixedDeltaTime * moveDir);

        animator.SetFloat(hashToSpeed, moveVector.magnitude * speed / sprintSpeed);
    }

    public void OnLook(Vector2 vector)
    {
        Vector2 screentCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // 스크린 중앙을 (0, 0)으로 맞추기
        Vector2 mousePosition = vector - screentCenter;                                 // 스크린 중앙와 마우스의 방향 벡터값

        lookVector = new Vector3(mousePosition.x, 0, mousePosition.y);
    }

    public void OnSprint(bool isPressed)
    {
        if (isPressed)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}