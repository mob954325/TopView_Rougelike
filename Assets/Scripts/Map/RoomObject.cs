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

/// <summary>
/// 맵 오브젝트 클래스
/// </summary>
public class RoomObject : MonoBehaviour
{
    /// <summary>
    /// 불투명화 할 벽 클래스들
    /// </summary>
    TranslucentWall[] translucentWalls;

    /// <summary>
    /// 해당 방 인덱스
    /// </summary>
    int roomIndex;

    /// <summary>
    /// 방 인덱스 접근 프로퍼티
    /// </summary>
    public int RoomIndex => roomIndex;

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
    public Direction Direction => direction;

    /// <summary>
    /// 연결된 방향의 벽 오브젝트들 (상하좌우)
    /// </summary>
    public GameObject[] entranceWalls;

    /// <summary>
    /// 연결된 방향의 게이트 오브젝트들 (상하좌우)
    /// </summary>
    public Locked_Gate[] entranceGates;

    /// <summary>
    /// 방의 오브젝트들 저장 배열
    /// </summary>
    public GameObject[] roomObjects;

    /// <summary>
    /// 해당 방 적 개수
    /// </summary>
    [SerializeField]int enemyCount = 0;

    public int EnemyCount
    {
        get => enemyCount;
        set
        {
            enemyCount = value;
            if(enemyCount < 1 && isRoomEnter && !isClear)
            {
                onRoomClear?.Invoke();
                isClear = true;
            }
        }
    }

    /// <summary>
    /// 적 스폰 범위
    /// </summary>
    float maxSpawnRange = 3f;

    /// <summary>
    /// 방에 들어왔는지 확인하는 변수
    /// </summary>
    bool isRoomEnter = false;

    /// <summary>
    /// 해당 스테이지 클리어 여부
    /// </summary>
    bool isClear = false;

    /// <summary>
    /// 클리어 여부 접근 프로퍼티
    /// </summary>
    public bool IsClear => isClear;

    /// <summary>
    /// 방의 모든 적이 처리되면 호출되는 델리게이트 ( 조건 : 적 개수 0이면 호출 )
    /// </summary>
    public Action onRoomClear;

    // 생명 함수 ===========================================================================================

    private void Awake()
    {
        Transform child;
        entranceWalls = new GameObject[4];
        entranceGates = new Locked_Gate[4];

        translucentWalls = GetComponentsInChildren<TranslucentWall>();

        foreach(var c in translucentWalls)
        {
            c.Initialize(); // 벽면 클래스 초기화
        }


        for (int i = 0; i < 4; i++)
        {
            child = transform.GetChild(i).GetChild(2);
            entranceGates[i] = child.GetChild(0).GetComponent<Locked_Gate>();
            entranceGates[i].gameObject.SetActive(false);

            entranceWalls[i] = child.GetChild(1).gameObject;
        }
    }

    // 오브젝트 설정 함수 ===========================================================================================

    /// <summary>
    /// 맵 오브젝트 초기화 함수
    /// </summary>
    /// <param name="roomType">방 타입</param>
    /// <param name="enemyCount">스폰할 적 숫자</param>
    /// <param name="indexNum">방 인덱스 번호</param>
    public void Initialize(RoomType roomType, int enemyCount = 0, int indexNum = 9999)
    {
        type = roomType;
        this.EnemyCount = enemyCount;
        this.roomIndex = indexNum;

        if (roomType == RoomType.Normal || roomType == RoomType.Boss)
        {
            roomObjects = new GameObject[enemyCount];   // 적 오브젝트들 저장
        }
        else
        {
            roomObjects = new GameObject[1];            // 상자, 플레이어 등등 저장
        }
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

    // 스폰 관련 함수 ===========================================================================================

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
        for (int i = 0; i < enemyCount; i++)
        {
            int enemyCode = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EnemyNormalType)).Length);

            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-maxSpawnRange, maxSpawnRange),
                                                0,
                                                UnityEngine.Random.Range(-maxSpawnRange, maxSpawnRange));

            GameObject obj = Factory.Instance.SpawnEnemyByCode((EnemyNormalType)enemyCode, cellPos + spawnPosition, Quaternion.identity); // 적 생성
            roomObjects[i] = obj;
            roomObjects[i].GetComponent<PoolObject>().onDisable += () => { EnemyCount--; };
        }    
    }

    // 문 관련 함수 ===========================================================================================

    /// <summary>
    /// 특정 방향의 문을 여는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    public void OpenDoor(Direction dir)
    {
        if (!IsVaildDirection(dir))
            return;

        int index = -1;
        switch (dir)
        {
            case Direction.UP:
                index = 0;
                entranceGates[index].ForcedOpen();
                break;
            case Direction.DOWN:
                index = 1;
                entranceGates[index].ForcedOpen();
                break;
            case Direction.LEFT:
                index = 2;
                entranceGates[index].ForcedOpen();
                break;
            case Direction.RIGHT:
                index = 3;
                entranceGates[index].ForcedOpen();
                break;
        }

        if (!IsClear)
        {
            entranceGates[index].SetColliderEnable(true);
        }
        else
        {
            entranceGates[index].SetColliderEnable(false);
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
        
        int index = -1;
        switch (dir)
        {
            case Direction.UP:
                index = 0;
                entranceGates[index].ForcedClose();
                break;
            case Direction.DOWN:
                index = 1;
                entranceGates[index].ForcedClose();
                break;
            case Direction.LEFT:
                index = 2;
                entranceGates[index].ForcedClose();
                break;
            case Direction.RIGHT:
                index = 3;
                entranceGates[index].ForcedClose();
                break;
        }

        entranceGates[index].SetColliderEnable(false);
    }

    // 기능 함수 ===========================================================================================

    /// <summary>
    /// 존재하는 방향인지 확인하는 함수
    /// </summary>
    /// <returns>존재하면 true 아니면 false</returns>
    public bool IsVaildDirection(Direction dir)
    {
        bool result = false;

        int mask = (int)Direction; // 모든 방향값 , 1111
        int maskedNum = mask & (int)dir;    // 비교 : dir값의 자리가 1이면 존재 0이면 존재하지않음

        if(maskedNum == (int)dir) // 값이 같다 == 해당 방향이 존재한다
        {
            result = true;
        }

        return result;
    }

    /// <summary>
    /// isRoomEnter 값 변경 함수 
    /// </summary>
    public bool SetIsEnter(bool value)
    {
        return isRoomEnter = value;
    }

#if UNITY_EDITOR
    public void Test_SetPath(Direction dir)
    {
        MakePath(dir);
    }

    public void Test_DisableAllObjects()
    {
        foreach(var item in roomObjects)
        {
            item.SetActive(false);
        }
    }
#endif
}