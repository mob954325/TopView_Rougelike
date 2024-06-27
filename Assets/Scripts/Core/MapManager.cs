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
    [SerializeField] RoomObject[] cellObject;

    // UI 클래스 ==========================================================

    /// <summary>
    /// 업그레이드 UI 클래스
    /// </summary>
    UpgradeUI upgradeUI;

    /// <summary>
    /// 업그레이드 UI 접근 프로퍼티
    /// </summary>
    public UpgradeUI UpGradeUI => upgradeUI;

    /// <summary>
    /// 보스 체력 UI 클래스
    /// </summary>
    BossHealthUI bossHealthUI;

    /// <summary>
    /// 보스 체력 UI 접근용 프로퍼티
    /// </summary>
    public BossHealthUI BossHealthUI => bossHealthUI;   

    // 함수 ==========================================================

    protected override void PreInitialize()
    {
        generator = GetComponentInChildren<MapGenerator>();
        upgradeUI = FindAnyObjectByType<UpgradeUI>();
        bossHealthUI = FindAnyObjectByType<BossHealthUI>();
    }

    /// <summary>
    /// 방의 정보를 얻는 함수
    /// </summary>
    public void SetCellObjects()
    {
        cellObject = new RoomObject[generator.MapRooms.Length];
        cellObject = generator.MapRooms;
    }

    // 플레이어가 들어가있는 방 찾기
    // 해당 방의 Wall_Down 머터리얼 찾아서 Alpha 낮추기
}