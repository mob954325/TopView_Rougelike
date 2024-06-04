using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody rigid;
    Animator animator;

    /// <summary>
    /// 움직임 방향 벡터
    /// </summary>
    public Vector3 moveVector = Vector3.zero;

    /// <summary>
    /// 바라보는 방향 벡터
    /// </summary>
    public Vector3 lookVector = Vector3.zero;

    /// <summary>
    /// 플레이어 스피드
    /// </summary>
    public float speed = 5.0f;

    int HashToSpeed = Animator.StringToHash("Speed");

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        playerInput.onMove += Move;
        playerInput.onLook += Look;
    }

    void FixedUpdate()
    {
        rigid.MovePosition(transform.position + Time.fixedDeltaTime * moveVector.normalized s* speed); // 플레이어 움직임
        transform.LookAt(lookVector);   // 플레이어가 바라보는 방향
        animator.SetFloat(HashToSpeed, moveVector.normalized.magnitude * speed);
    }

    /// <summary>
    /// 플레이어 움직임 입력을 받는 함수
    /// </summary>
    /// <param name="inputValue">입력 값</param>
    private void Move(Vector2 inputValue)
    {
        moveVector = new Vector3(inputValue.x, 0, inputValue.y);
    }

    /// <summary>
    /// 플레이어가 바라보는 방향값을 받는 함수
    /// </summary>
    /// <param name="vector">스크린 포지션의 마우스 위치 값</param>
    private void Look(Vector2 vector)
    {
        Vector2 screentCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // 스크린 중앙을 (0, 0)으로 맞추기
        Vector2 mousePosition = vector - screentCenter;                                 // 스크린 중앙와 마우스의 방향 벡터값

        lookVector = new Vector3(mousePosition.x, 0, mousePosition.y);
    }
}