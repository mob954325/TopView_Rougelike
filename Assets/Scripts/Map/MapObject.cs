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
    /// 해당 방 인덱스
    /// </summary>
    int roomIndex;

    /// <summary>
    /// 방 인덱스 접근 프로퍼티
    /// </summary>
    int RoomIndex => roomIndex;

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
    /// 뚫려있는 방향 확인용 프로퍼티
    /// </summary>
    public Direction Direction => Direction;

    /// <summary>
    /// 연결된 방향의 벽 오브젝트들 (상하좌우)
    /// </summary>
    public GameObject[] entranceWalls;

    /// <summary>
    /// 연결된 방향의 게이트 오브젝트들 (상하좌우)
    /// </summary>
    public Locked_Gate[] entranceGates;

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
        entranceWalls = new GameObject[4];
        entranceGates = new Locked_Gate[4];

        for (int i = 0; i < 4; i++)
        {
            child = transform.GetChild(i).GetChild(2);
            entranceGates[i] = child.GetChild(0).GetComponent<Locked_Gate>();
            entranceGates[i].gameObject.SetActive(false);

            entranceWalls[i] = child.GetChild(1).gameObject;
        }
    }

    /// <summary>
    /// 맵 오브젝트 초기화 함수
    /// </summary>
    /// <param name="roomType">방 타입</param>
    /// <param name="enemyCount">스폰할 적 숫자</param>
    /// <param name="indexNum">방 인덱스 번호</param>
    public void Initialize(RoomType roomType, int enemyCount = 0, int indexNum = 9999)
    {
        type = roomType;
        this.enemyCount = enemyCount;
        this.roomIndex = indexNum;
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
                entranceGates[i].gameObject.SetActive(true);
                entranceWalls[i].SetActive(false);
            }
            else
            {
                entranceWalls[i].SetActive(true);
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
    /// 특정 방향의 문을 여는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    public void OpenDoor(Direction dir)
    {
        if (!IsVaildDirection(dir))
            return;

        switch (dir)
        {
            case Direction.UP:
                entranceGates[0].ForcedOpen();
                break;
            case Direction.DOWN:
                entranceGates[1].ForcedOpen();
                break;
            case Direction.LEFT:
                entranceGates[2].ForcedOpen();
                break;
            case Direction.RIGHT:
                entranceGates[3].ForcedOpen();
                break;
        }
    }

    /// <summary>
    /// 특정 방향의 문을 닫는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    public void CloseDoor(Direction dir)
    {
        if (!IsVaildDirection(dir))
            return;

        switch (dir)
        {
            case Direction.UP:
                entranceGates[0].ForcedClose();
                break;
            case Direction.DOWN:
                entranceGates[1].ForcedClose();
                break;
            case Direction.LEFT:
                entranceGates[2].ForcedClose();
                break;
            case Direction.RIGHT:
                entranceGates[3].ForcedClose();
                break;
        }
    }



    /// <summary>
    /// 존재하는 방향인지 확인하는 함수
    /// </summary>
    /// <returns>존재하면 true 아니면 false</returns>
    public bool IsVaildDirection(Direction dir)
    {
        bool result = false;

        int mask = (int)(Direction.DOWN | Direction.UP | Direction.RIGHT | Direction.LEFT); // 모든 방향값 , 1111
        int maskedNum = mask & (int)dir;    // 비교 : dir값의 자리가 1이면 존재 0이면 존재하지않음
       
        if(maskedNum == 0) // 0이다 == 해당 방향 비트가 0이다.
        {
            Debug.LogWarning($"{roomIndex}번의 방에 {dir}방향 길이 존재하지 않습니다.");
        }
        else // 존재함
        {
            result = true;
        }

        return result;
    }

#if UNITY_EDITOR
    public void Test_SetPath(Direction dir)
    {
        MakePath(dir);
    }
#endif
}