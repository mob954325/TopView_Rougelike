using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Direction : byte
{
   NONE = 0, UP = 1, DOWN = 2, LEFT = 4, RIGHT = 8
}

public enum RoomType
{
    Normal = 0, Chest, Boss
}

public class Cell : MonoBehaviour
{
    // 타입
    public RoomType type = RoomType.Normal;
    // 뚫려있는 방향
    public Direction direction = Direction.NONE;
    // 입구 오브젝트
    public GameObject[] entrance; 
    // 적 개수
    public uint enemyCount = 0;
    // 해당 셀 위치 값
    public Vector2Int grid;
    // 셀 한 면의 크기
    const float cellWidth = 15f;
    // 해당 스테이지 클리어 여부
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
    /// 셀 생성자 
    /// </summary>
    /// <param name="dir">뚫려있는 방향(비트 플래그)</param>
    /// <param name="type">방 타입</param>
    /// <param name="grid">그리드값 위치</param>
    public Cell(Direction dir, RoomType type, Vector2Int grid)
    {
        MakePath(dir);
        transform.position = new Vector3(grid.x, 0f, grid.y);
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
            Debug.Log($"{result}");
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