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
    Normal = 0, Chest, Start ,Boss
}

public class MapObject : MonoBehaviour
{
    /// <summary>
    /// 해당 방 타입
    /// </summary>
    [SerializeField]RoomType type = RoomType.Normal;

    /// <summary>
    /// 방 타입 확인용 프로퍼티 
    /// </summary>
    public RoomType Type => type;
    
    /// <summary>
    /// 뚫려있는 길 방향
    /// </summary>
    [SerializeField]Direction direction = Direction.NONE;

    /// <summary>
    /// 입구 오브젝트들 (상하좌우)
    /// </summary>
    public GameObject[] entrance;

    /// <summary>
    /// 해당 방 적 개수
    /// </summary>
    [SerializeField]int enemyCount = 0;

    /// <summary>
    /// 적 스폰 범위
    /// </summary>
    float maxSpawnRange = 3f;

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
    /// 맵 오브젝트 초기화 함수
    /// </summary>
    /// <param name="roomType">방 타입</param>
    /// <param name="pathDir">뚫린 방향</param>
    /// <param name="enemyCount">스폰할 적 숫자</param>
    public void Initialize(RoomType roomType, int enemyCount = 0)
    {
        type = roomType;
        this.enemyCount = enemyCount;

        // 개수 만큼 적 생성
    }

    /// <summary>
    /// 셀의 길을 뚫는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    public void MakePath(Direction dir)
    {
        direction = dir;

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
    
    /// <summary>
    /// 파괴 가능한 벽 생성
    /// </summary>
    public void PlaceBreakableWall()
    {

    }

    /// <summary>
    /// 방에서 적 스폰
    /// </summary>
    /// <param name="cellPos">스폰 할 위치(셀 위치)</param>
    public void SpawnEnemy(Vector3 cellPos)
    {
        for(int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(1f, 3f),
                                                0,
                                                UnityEngine.Random.Range(1f, 3f));
            Factory.Instance.SpawnEnemyMage(cellPos + spawnPosition, Quaternion.identity);
        }    
    }

    /// <summary>
    /// 클리어 설정
    /// </summary>
    public void SetClear()
    {
        isClear = true;
    }

#if UNITY_EDITOR
    public void Test_SetPath(Direction dir)
    {
        MakePath(dir);
    }
#endif
}