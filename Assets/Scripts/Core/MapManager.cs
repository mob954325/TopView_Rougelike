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
    MapGenerator generator;

    [Header("맵 정보")]
    /// <summary>
    /// 맵의 셀 오브젝트들
    /// </summary>
    public RoomObject[] cellObject;

    protected override void PreInitialize()
    {
        generator = GetComponentInChildren<MapGenerator>();    
    }

    /// <summary>
    /// 방에 진입했을 때 실행하는 함수
    /// </summary>
    /// <param name="index">인덱스 값</param>
    public void StartRoom(int index)
    {
        generator.CloseAroundDoor(index);
    }

    /// <summary>
    /// 방에 모든 일이 끝나면 실행하는 함수 ( 전투 종료 등등 )
    /// </summary>
    public void EndRoom(int index)
    {
        generator.OpenAroundDoor(index);
    }
}