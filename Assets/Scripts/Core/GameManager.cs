using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 시작여부를 확인하는 bool값
    /// </summary>
    public bool isGameStart;

    // 플레이어 =================================================================

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
    /// 플래이어 카메라 스크립트
    /// </summary>
    public Player_Camera playerCam;

    /// <summary>
    /// 플레이 타임
    /// </summary>
    public float playTime = 0f;

    // 델리게이트 =================================================================

    /// <summary>
    /// 게임 시작시 호출되는 델리게이트
    /// </summary>
    public Action onGameStart;

    /// <summary>
    /// 게임 종료시 호출되는 델리게이트 (T1 : 점수값, T2 : 플레이어가 사망하면 false, 클리어하면 true)
    /// </summary>
    public Action<int, bool> onGameEnd;

    // 생명 함수 =================================================================

    protected override void PreInitialize()
    {
        playerCam = GetComponentInChildren<Player_Camera>();
    }

    void FixedUpdate()
    {
        if(isGameStart)
        {
            playTime += Time.fixedDeltaTime;
        }
    }

    // 기능 함수 =================================================================

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

    /// <summary>
    /// 게임을 실행하는 함수
    /// </summary>
    public void StartGame()
    {
        // 플레이어 생성
        SpawnPlayer(Vector3.zero);
        // 맵 생성
        MapManager.Instance.generator.OnGenerateStart();
        // UI 생성
        onGameStart?.Invoke();

        // 스폰 후 설정
        playerCam.Initialize(player); // 카메라 세팅

        // 게임 설정 초기화
        isGameStart = true;
        playTime = 0f;
    }

    /// <summary>
    /// 게임이 끝날 때 실행하는 함수
    /// </summary>
    public void EndGame(int score, bool isClear)
    {
        onGameEnd?.Invoke(score, isClear);
    }
}