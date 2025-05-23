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

    /// <summary>
    /// 플레이어가 공격할 때 호출되는 델리게이트
    /// </summary>
    public Action onAttack;

    /// <summary>
    /// 플레이어가 특수 공격할 때 호출되는 델리게이트
    /// </summary>
    public Action onHeavyAttack;

    /// <summary>
    /// 플레이어가 폭탄을 사용할 때 호출되는 델리게이트
    /// </summary>
    public Action<uint> onUseBomb;

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
        playerInputAction.Player.Attack.performed += OnAttackInput;
        playerInputAction.Player.HeavyAttack.performed += OnHeavyAttackInput;
        playerInputAction.Player.UseBomb.performed += OnUseBomb;
    }

    void OnDisable()
    {
        playerInputAction.Player.UseBomb.performed -= OnUseBomb;
        playerInputAction.Player.HeavyAttack.performed -= OnHeavyAttackInput;
        playerInputAction.Player.Attack.performed -= OnAttackInput;
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

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        onAttack?.Invoke();
    }

    private void OnHeavyAttackInput(InputAction.CallbackContext context)
    {
        onHeavyAttack?.Invoke();
    }

    private void OnUseBomb(InputAction.CallbackContext context)
    {
        onUseBomb?.Invoke(1);
    }

    /// <summary>
    /// 플레이어 조작을 막는 함수
    /// </summary>
    public void DisablePlayInput()
    {
        playerInputAction.Player.Disable();
    }
}