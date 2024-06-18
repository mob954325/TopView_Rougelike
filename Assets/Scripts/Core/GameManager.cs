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

    /// <summary>
    /// 플레이어 프리팹
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// 플레이어가 스폰되었는지 확인하는 변수 
    /// </summary>
    public bool isPlayerSpanwed = false;

    /// <summary>
    /// 게임 시작시 호출되는 델리게이트
    /// </summary>
    public Action onGameStart;

    /// <summary>
    /// 게임 종료시 호출되는 델리게이트
    /// </summary>
    public Action onGameEnd;

    /// <summary>
    /// 플레이어 스폰 함수
    /// </summary>
    /// <param name="position">스폰 위치 값</param>
    /// <returns>스폰 성공하면 true 아니면 false</returns>
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