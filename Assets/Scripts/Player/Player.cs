using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_InputSettings))]
[RequireComponent(typeof(Player_Movement))]
public class Player : MonoBehaviour
{
    Player_InputSettings playerInput;
    Player_Movement movement;

    void Awake()
    {
        playerInput = GetComponent<Player_InputSettings>();
        movement = GetComponent<Player_Movement>();

        playerInput.onMove += movement.OnMove;
        playerInput.onLook += movement.OnLook;
        playerInput.onSprint += movement.OnSprint;
    }
}