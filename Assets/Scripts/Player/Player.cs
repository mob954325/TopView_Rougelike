using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_InputSettings))]
[RequireComponent(typeof(Player_Movement))]
[RequireComponent(typeof(Player_Battle))]

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    Player_InputSettings playerInput;
    Player_Movement movement;
    Player_Battle battle;

    void Awake()
    {
        playerInput = GetComponent<Player_InputSettings>();
        movement = GetComponent<Player_Movement>();
        battle = GetComponent<Player_Battle>();

        playerInput.onMove += movement.OnMove;
        playerInput.onLook += movement.OnLook;
        playerInput.onSprint += movement.OnSprint;
        playerInput.onAttack += battle.OnAttack;
    }
}