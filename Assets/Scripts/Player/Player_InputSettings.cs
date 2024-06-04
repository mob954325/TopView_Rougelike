using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_InputSettings : MonoBehaviour
{
    PlayerInputActions playerInputAction;

    /// <summary>
    /// 움직일 때 호출되는 델리게이트
    /// </summary>
    public Action<Vector2> onMove;

    /// <summary>
    /// 플레이어가 마우스를 움직일 때 호출되는 델리게이트
    /// </summary>
    public Action<Vector2> onLook;

    /// <summary>
    /// 플레이어가 달리기를 시작할 때 호출되는 델리게이트
    /// </summary>
    public Action<bool> onSprint;

    void Awake()
    {
        playerInputAction = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerInputAction.Enable();
        playerInputAction.Player.Move.performed += OnMoveInput;
        playerInputAction.Player.Move.canceled += OnMoveInput;
        playerInputAction.Player.Look.performed += OnLookInput;
        playerInputAction.Player.Sprint.performed += OnSprintInput;
        playerInputAction.Player.Sprint.canceled += OnSprintInput;
    }

    void OnDisable()
    {
        playerInputAction.Player.Sprint.canceled -= OnSprintInput;
        playerInputAction.Player.Sprint.performed -= OnSprintInput;
        playerInputAction.Player.Look.performed -= OnLookInput;
        playerInputAction.Player.Move.canceled -= OnMoveInput;
        playerInputAction.Player.Move.performed -= OnMoveInput;
        playerInputAction.Disable();
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        onMove?.Invoke(context.ReadValue<Vector2>());
    }
    private void OnLookInput(InputAction.CallbackContext context)
    {
        onLook?.Invoke(context.ReadValue<Vector2>());
    }
    private void OnSprintInput(InputAction.CallbackContext context)
    {
        onSprint?.Invoke(context.performed);
    }
}