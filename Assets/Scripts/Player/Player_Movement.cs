using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class Player_Movement : MonoBehaviour
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 달리기 체크 변수
    /// </summary>
    bool isSprint;

    /// <summary>
    /// 움직임 방향 벡터
    /// </summary>
    public Vector2 moveInputVector = Vector2.zero;

    /// <summary>
    /// 마우스 위치 값
    /// </summary>
    public Vector2 mouseInputVector = Vector2.zero;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        player.playerInput.onMove += OnMove;
        player.playerInput.onLook += OnLook;
        player.playerInput.onSprint += OnSprint;
    }

    private void FixedUpdate()
    {
        player.Move(GetMoveDirection(), isSprint);
        player.Look(GetLookVector());
    }

    /// <summary>
    /// 플레이어 이동 입력을 받는 함수
    /// </summary>
    /// <param name="inputValue">입력 값</param>
    public void OnMove(Vector2 inputValue)
    {
        moveInputVector = inputValue;
    }

    /// <summary>
    /// 플레이어가 이동할 방향을 반환하는 함수
    /// </summary>
    /// <returns>목표 방향 벡터</returns>
    Vector3 GetMoveDirection()
    {
        Vector3 moveDir;

        // 월드 기준 상하좌우 움직임
        Vector3 verticalValue = moveInputVector.y * Vector3.forward;         // 수직 벡터값
        Vector3 horizontalValue = moveInputVector.x * Vector3.right;         // 수평 벡터값 

        moveDir = verticalValue + horizontalValue;                      // 움직일 방향 값

        return moveDir;
    }

    /// <summary>
    /// 플레이어의 회전 입력을 받는 함수
    /// </summary>
    /// <param name="inputValue">입력값</param>
    public void OnLook(Vector2 inputValue)
    {
        mouseInputVector = inputValue;
    }

    /// <summary>
    /// 플레이어 캐릭터 회전 함수 ( 마우스 위치 바라보기 )
    /// </summary>
    /// <param name="vector"></param>
    public Vector2 GetLookVector()
    {
        Vector2 lookVector;

        Vector2 screentCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // 스크린 중앙을 (0, 0)으로 맞추기
        Vector2 mousePosition = mouseInputVector - screentCenter;                                 // 스크린 중앙와 마우스의 방향 벡터값

        lookVector = new Vector3(mousePosition.x, 0, mousePosition.y);

        return lookVector;
    }

    /// <summary>
    /// 플레이어 달리기 함수
    /// </summary>
    /// <param name="isPressed">버튼을 눌렀으면 Ture 아니면 false</param>
    public void OnSprint(bool isPressed)
    {
        isSprint = isPressed;
    }
}