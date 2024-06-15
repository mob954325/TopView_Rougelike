using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 방향 값 (비트 플래그)
/// </summary>
[Flags]
public enum Direction : byte
{
   NONE = 0, UP = 1, DOWN = 2, LEFT = 4, RIGHT = 8
}

/// <summary>
/// 방타입 
/// </summary>
public enum RoomType
{
    Normal = 0, Chest, Boss
}

public class MapObject : MonoBehaviour
{
    /// <summary>
    /// 해당 방 타입
    /// </summary>
    public RoomType type = RoomType.Normal;
    
    /// <summary>
    /// 뚫려있는 길 방향
    /// </summary>
    public Direction direction = Direction.NONE;

    /// <summary>
    /// 입구 오브젝트들 (상하좌우)
    /// </summary>
    public GameObject[] entrance; 

    /// <summary>
    /// 해당 방 적 개수
    /// </summary>
    public uint enemyCount = 0;

    /// <summary>
    /// 위치 값 (월드)
    /// </summary>
    public Vector3 position; // 왼쪽 밑이 pivot

    /// <summary>
    /// 해당 스테이지 클리어 여부
    /// </summary>
    public bool isClear = false;

    private void Awake()
    {
        Transform child;
        entrance = new GameObject[4];

        for(int i = 0; i < 4; i++)
        {
            child = transform.GetChild(i).GetChild(2);
            entrance[i] = child.gameObject;
        }
    }

    /// <summary>
    /// 셀의 길을 뚫는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    public void MakePath(Direction dir)
    {
        int mask = 1; // 비트 확인용 값
        for(int i = 0; i < 4; i++)
        {
            int result = (int)dir & mask;
            if(result == mask)
            {
                entrance[i].SetActive(false);
            }
            else
            {
                entrance[i].SetActive(true);
            }

            mask <<= 1; // 왼쪽으로 한칸 옮김
        }
    }
}