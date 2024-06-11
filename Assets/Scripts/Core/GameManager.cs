using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 플레이어
    /// </summary>
    [HideInInspector] public Player player;

    protected override void PreInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }
}