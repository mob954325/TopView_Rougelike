using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
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
    }

    void OnDisable()
    {
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
}