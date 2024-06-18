using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    // 맵 생성,
    // 맵의 각 셀들 관리
    // 각 셀별 클리어 여부

    /// <summary>
    /// 맵 생성기
    /// </summary>
    MapGenerator generator;

    [Header("맵 세팅")]
    /// <summary>
    /// 맵의 셀 오브젝트
    /// </summary>
    public GameObject cellObject;

    [Space(10f)]
    /// <summary>
    /// 맵 가로 길이 (개수)
    /// </summary>
    [Tooltip("가로 오브젝트 개수")]
    public int width;

    /// <summary>
    /// 맵 세로 길이 (개수)
    /// </summary>
    [Tooltip("세로 오브젝트 개수")]
    public int height;

    protected override void PreInitialize()
    {
        GameObject generatorObj = new GameObject();
        generatorObj.name = $"Map";
        generatorObj.transform.parent = transform;

        generator = generatorObj.AddComponent<MapGenerator>();
        generator.cellObject = this.cellObject; // 셀 오브젝트 저장
        generator.Initialize(width, height);    // 스테이지 초기화
    }
}