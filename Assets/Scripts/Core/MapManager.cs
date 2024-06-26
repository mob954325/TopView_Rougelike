using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    // 생성한 맵의 정보 관리
    // 맵의 각 셀들 관리
    // 각 셀별 클리어 여부

    /// <summary>
    /// 맵 생성기
    /// </summary>
    public MapGenerator generator;

    [Header("맵 정보")]
    /// <summary>
    /// 맵의 셀 오브젝트들
    /// </summary>
    [SerializeField]RoomObject[] cellObject;

    Enemy_Boss_Warrior bossObj;

    protected override void PreInitialize()
    {
        generator = GetComponentInChildren<MapGenerator>();    
    }

    public void SetBossClear(Enemy_Boss_Warrior obj)
    {
        bossObj = obj;
        bossObj.onDie += () => { GameManager.Instance.EndGame(GameManager.Instance.player.GetPlayerScore(), true); }
    }
}