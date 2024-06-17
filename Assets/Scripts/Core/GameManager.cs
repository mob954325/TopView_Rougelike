using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 플레이어
    /// </summary>
    public Player player;

    public GameObject playerPrefab;

    public bool isPlayerSpanwed = false;

    protected override void PreInitialize()
    {
        //player = FindAnyObjectByType<Player>();
        //player = playerPrefab.GetComponent<Player>();
    }

    public bool SpawnPlayer(Vector3 position)
    {
        bool result = false;

        if(!isPlayerSpanwed)
        {
            GameObject playerObj = Instantiate(playerPrefab);
            playerObj.transform.position = position;
            playerObj.name = $"PLAYER";

            player = playerObj.GetComponent<Player>();

            isPlayerSpanwed = true; // 스폰 확인 
            result = true;
        }

        return result;  
    }
}