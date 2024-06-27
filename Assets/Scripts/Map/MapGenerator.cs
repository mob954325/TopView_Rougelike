using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 생성 클래스 
/// </summary>
public class MapGenerator : MonoBehaviour
{
    // 스테이지 세팅 ============================================================================

    /// <summary>
    /// 맵 사이즈
    /// </summary>
    [Range(3,5)]
    public int mapSize = 3;

    /// <summary>
    /// 넓이 
    /// </summary>
    int width;

    /// <summary>
    /// 높이
    /// </summary>
    int height;

    /// <summary>
    /// 셀 오브젝트
    /// </summary>
    public GameObject cellObject;

    /// <summary>
    /// 맵 셀 오브젝트
    /// </summary>
    RoomObject[] mapRooms;

    /// <summary>
    /// 맵 셀 접근용 프로퍼티
    /// </summary>
    public RoomObject[] MapRooms => mapRooms;

    /// <summary>
    /// mapObjs 한 면의 길이
    /// </summary>
    [SerializeField]int mapObjLength = 15;

    /// <summary>
    /// 생성되었는지 확인하는 변수
    /// </summary>
    bool isGenerated = false;

    // 방 개수 ============================================================================

    /// <summary>
    /// 현재 상자방 개수 (생성용)
    /// </summary>
    int currentChestRoomCount = 0;

    /// <summary>
    /// 최대 상자방 생성 개수 (생성용)
    /// </summary>
    [Range(1, 5)]
    public int maxChestRoomCount = 0;

    /// <summary>
    /// 현재 기본 방 개수 (생성용)
    /// </summary>
    int currnetNormalRoomCount = 0;

    /// <summary>
    /// 현재 시작방 개수 (생성용)
    /// </summary>
    int currnetStartRoomCount = 0;

    /// <summary>
    /// 현재 보스방 개수 (생성용)
    /// </summary>
    int currnetBossRoomCount = 0;

    // 맵 타입 오브젝트 ========================================================================

    /// <summary>
    /// 시작 방
    /// </summary>
    RoomObject startRoom;

    /// <summary>
    /// 시작 방 접근 프로퍼티
    /// </summary>
    RoomObject StartRoom => startRoom;

    /// <summary>
    /// 보스방 후보 리스트
    /// </summary>
    List<RoomObject> bossRoomCandidates;

    /// <summary>
    /// 상자방 후보 리스트
    /// </summary>
    List<RoomObject> chestRoomCandidates;

    // 델리게이트 =============================================================================

    /// <summary>
    /// 생성이 완료되면 실행되는 델리게이트
    /// </summary>
    public Action onGenerateComplete;

    // 생명 주기 함수  ========================================================================

    /// <summary>
    /// 맵 생성을 시작할 때 호출되는 함수
    /// </summary>
    public void OnGenerateStart()
    {
       width = mapSize;
       height = mapSize;
       
       Initialize(width, height);
       
       // 플레이어 설정
       SpawnObjets(StartRoom.Type, StartRoom.RoomIndex);   // 플레이어 스폰
       OpenAroundDoor(StartRoom.RoomIndex, true);                // 모든 플레이어 방 개방
    }

    // 맵 생성 함수 ============================================================================

    /// <summary>
    /// 스테이지 구성을 시작하는 함수
    /// </summary>
    /// <param name="width">넓이</param>
    /// <param name="height">길이</param>
    public void Initialize(int width, int height)
    {
        this.width = width;
        this.height = height;

        mapRooms = new RoomObject[width * height];

        GenerateMap();
        onGenerateComplete?.Invoke();
    }

    /// <summary>
    /// 맵 생성 시 호출되는 함수 
    /// </summary>
    void GenerateMap()
    {
        // 방 개수 변수 초기화
        currnetNormalRoomCount = width * height;
        currnetStartRoomCount = 0;
        currnetBossRoomCount = 0;
        currentChestRoomCount = 0;

        // 맵 삭제 (기존에 생성했던 맵 제거 용)
        DeleteMap();

        Ellers eller = new Ellers(width, height); // 맵 알고리즘 실행

        // 랜덤 방 설정용 리스트들 설정
        int bossRoomCandidatesCount = (width * height) - ((width - 1) * (height - 1));          // 보스방 후보 개수
        int chestRoomCandidatesCount = width * height - 2;                                      // 상자방 후보 개수 ( 모든 방 - 시작 방 - 보스방)

        bossRoomCandidates = new List<RoomObject>(bossRoomCandidatesCount);
        chestRoomCandidates = new List<RoomObject>(chestRoomCandidatesCount);

        // 맵 생성 시작
        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                int index = y * width + x; // 현재 방 인덱스 번호

                // 방 오브젝트 생성
                GameObject obj = Instantiate(cellObject);
                mapRooms[index] = obj.GetComponent<RoomObject>();
                obj.transform.parent = this.gameObject.transform;
                obj.name = $"Cell_{index}";

                int randomEnemyNum = (int)UnityEngine.Random.Range(1, 4);   // 적 개수 랜덤 생성

                Vector2Int grid = new Vector2Int(x, y);                     // 그리드 값 (방 위치 체크용)

                chestRoomCandidates.Add(mapRooms[index]);                   // 상자방 후보 지정 

                // 방 타입 정하기
                // 시작 위치 : 맵 중앙 ( 버림 )
                if ((grid == WorldToGrid(new Vector3(mapObjLength, 0, mapObjLength))) && currnetStartRoomCount < 1)
                {
                    mapRooms[index].Initialize(RoomType.Start, 0, index);
                    startRoom = mapRooms[index];

                    currnetStartRoomCount++;

                    chestRoomCandidates.Remove(mapRooms[index]);    // 상자방 후보 제거
                }
                else // 노말방 생성
                {
                    mapRooms[index].Initialize(RoomType.Normal, randomEnemyNum, index);


                    // 보스 방 위치 맵 끝 방 중 하나 (path가 하나라도 열려있어야함) -> 노말 방 중 하나 변경
                    if ((y == 0 || y == height - 1 || x == 0 || x == width - 1))
                    {
                        bossRoomCandidates.Add(mapRooms[index]);        // 보스방 후보 추가
                        currnetBossRoomCount++;
                    }
                }

                // 방 오브젝트 초기화
                mapRooms[index].transform.localPosition = GridToWorld(eller.cells[index].grid);
                mapRooms[index].MakePath(eller.cells[index].pathDir);

                // 각 방 별로 함수 추가
                for(int i = 0; i < MapRooms[index].entranceGates.Length; i++)
                {
                    if (MapRooms[index].Type == RoomType.Start) // 시작 방 제외
                        continue;

                    // 방 입장 설정
                    MapRooms[index].entranceGates[i].onPassDoor = () =>
                    {
                        if(!MapRooms[index].IsClear)
                        {
                            SpawnObjets(MapRooms[index].Type, index);   // 타입별 오브젝트 스폰
                            CloseAroundDoor(index);                     // 문 닫기
                            MapRooms[index].SetIsEnter(true);           // 입장 확인

                            if (mapRooms[index].Type == RoomType.Chest) // 해당 방이 상자 방이면 즉시 클리어 
                            {
                                mapRooms[index].EnemyCount = 0;         // 즉시 클리어하게 적 개수 0으로 설정
                            }

                            // 밑의 벽 반투명 설정
                            TranslucenWall(index);
                        }
                    };

                    // 방 클리어 설정
                    mapRooms[index].onRoomClear = () =>
                    {
                        OpenAroundDoor(index, true); // 모든 문 개방

                        if (mapRooms[index].Type == RoomType.Normal) // 기본 방일 때만 스폰
                        {
                            SpawnRandomItems(mapRooms[index].transform.localPosition + new Vector3(mapObjLength * 0.5f, 1.2f, mapObjLength * 0.5f)); // 아이템 스폰 ( 위치 : 맵 중앙 )
                            MapManager.Instance.UpGradeUI.OpenPanel();
                        }

                        if (mapRooms[index].Type == RoomType.Boss)      // 보스 방일 때
                        {
                            MapManager.Instance.BossHealthUI.ShowUI();  // 보스 체력 UI 활성화
                        }

                        // 반투명 설정 해제
                        OpaqueWall(index);
                    };

                    // 모든 방문 초기화
                    CloseAroundDoor(index);
                }
            }
        }

        SetRandomRoomType(RoomType.Chest, ref chestRoomCandidates, maxChestRoomCount);  // 상자방 생성
        SetRandomRoomType(RoomType.Boss, ref bossRoomCandidates, 1);  // 보스방 생성


        ShowRoomTypeCount();
        isGenerated = true; // 미로 생성 여부 체크
    }

    /// <summary>
    /// list에서 랜덤으로 setCount만큼 방 타입을 변경하는 함수
    /// </summary>
    /// <param name="type">설정할 방 타입</param>
    /// <param name="list">후보 리스트</param>
    /// <param name="setCount">설정할 방 개수</param>
    void SetRandomRoomType(RoomType type, ref List<RoomObject> list, int setCount)
    {
        int roomCount = list.Count; // 방 개수
        RoomObject[] temp = new RoomObject[roomCount];  // 셔플용 배열

        int index = 0; // 배열용 인덱스 값
        foreach(var room in list)
        {
            temp[index] = room; // 배열요소에 데이터 추가
            index++;
        }

        // 배열 셔플
        Util<RoomObject> util = new Util<RoomObject>();
        temp = util.Shuffle(temp); 

        for(int i = 0; i < setCount; i++)
        {
            index = WorldToIndex(temp[i].transform.localPosition);  // 셔플한 배열의 인덱스값

            // 방 타입별 mapRooms 초기화
            switch(type)
            {
                case RoomType.Chest:
                    mapRooms[index].Initialize(type, 0, index);
                    currentChestRoomCount++;
                    break;
                case RoomType.Boss:
                    mapRooms[index].Initialize(type, 1, index);
                    currnetStartRoomCount++;
                    break;
            }
        }
    }

    /// <summary>
    /// 맵 제거 (재 생성용)
    /// </summary>
    void DeleteMap()
    {
        if (!isGenerated)
            return;

        // 배열 제거
        for(int i = 0; i < mapRooms.Length; i++)
        {
            if(mapRooms[i] != null)
            {
                mapRooms[i] = null;
            }
        }

        // 오브젝트 제거
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    // 오브젝트 생성 함수 ====================================================================

    /// <summary>
    /// 맵에 오브젝트 스폰 하는 함수
    /// </summary>
    /// <param name="type">방 타입</param>
    /// <param name="type">인덱스 값</param>
    public void SpawnObjets(RoomType type, int index)
    {
        Vector3 pos = mapRooms[index].transform.localPosition + new Vector3(mapObjLength * 0.5f, 0, mapObjLength * 0.5f);
        GameObject obj = null;  // 오브젝트 저장용
        switch (type)
        {
            case RoomType.Start:
                GameManager.Instance.player.transform.position = pos; //
                break;
            case RoomType.Chest:
                obj = Factory.Instance.SpawnChest(pos, Quaternion.identity);
                mapRooms[index].roomObjects[0] = obj;
                break;
            case RoomType.Boss:
                obj = Factory.Instance.SpawnEnemyWarriorBoss(pos, Quaternion.identity);
                obj.GetComponent<Enemy_Normal>().onDie += () => 
                {
                    GameManager.Instance.EndGame(GameManager.Instance.player.GetPlayerScore(), true);
                    MapManager.Instance.BossHealthUI.HideUI();
                };  // 보스 잡으면 클리어 패널 등장

                mapRooms[index].roomObjects[0] = obj;
                break;
            case RoomType.Normal:
                mapRooms[index].SpawnEnemy(pos);
                break;
        }
    }

    /// <summary>
    /// 방 클리어하고 아이템을 랜덤으로 생성하는 함수 ( 폭탄 or 열쇠 )
    /// </summary>
    /// <param name="position">생성할 위치</param>
    void SpawnRandomItems(Vector3 position)
    {
        float randomValue = UnityEngine.Random.value;

        if (randomValue > 0.5f)
        {
            Factory.Instance.SpawnItem(ItemCodes.Bomb, position, Quaternion.identity);
        }
        else
        {
            Factory.Instance.SpawnItem(ItemCodes.Key, position, Quaternion.identity);
        }
    }

    // 기능 함수 ============================================================================

    /// <summary>
    /// 주변 모든 문 닫기 ( 그리드 )
    /// </summary>
    /// <param name="grid">그리드 값</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void CloseAroundDoor(Vector2Int grid, bool isTwoWay = false)
    {
        RoomObject obj = mapRooms[GridToIndex(grid)];    // 시작 방

        for (int i = 0; i < 4; i++)
        {
            Direction dir = (Direction)(1 << i);
            if (obj.IsVaildDirection(dir))  // 각 방향 검사
            {
                CloseOnePath(grid, (Direction)(1 << i), isTwoWay);
            }
        }
    }

    /// <summary>
    /// 주변 모든 문 닫기 ( 인덱스 )
    /// </summary>
    /// <param name="index">인덱스 값</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void CloseAroundDoor(int index, bool isTwoWay = false)
    {
        CloseAroundDoor(IndexToGrid(index), isTwoWay);
    }

    /// <summary>
    /// 주변 모든 문 열기 ( 그리드 )
    /// </summary>
    /// <param name="grid">중심 방 그리드 값</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void OpenAroundDoor(Vector2Int grid, bool isTwoWay = false)
    {
        RoomObject obj = mapRooms[GridToIndex(grid)];    // 시작 방

        for (int i = 0; i < 4; i++)
        {
            Direction dir = (Direction)(1 << i);
            if (obj.IsVaildDirection(dir))  // 각 방향 검사
            {
                OpenOnePath(grid, (Direction)(1 << i), isTwoWay);
            }
        }
    }

    /// <summary>
    /// 주변 모든 문 열기 ( 인덱스 )
    /// </summary>
    /// <param name="index">인덱스 값</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void OpenAroundDoor(int index, bool isTwoWay = false)
    {
        OpenAroundDoor(IndexToGrid(index), isTwoWay);
    }

    /// <summary>
    /// 한 쪽 면을 여는 함수
    /// </summary>
    /// <param name="grid">셀 그리드 값</param>
    /// <param name="dir">열 방향</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void OpenOnePath(Vector2Int grid, Direction dir, bool isTwoWay = false)
    {
        RoomObject obj = mapRooms[GridToIndex(grid)];    // 시작 방
        RoomObject targetObj;                            // 시작 방에서의 dir방향의 방

        if(obj.IsVaildDirection(dir))  // 해당 방향에 길이 있다면
        {
            obj.OpenDoor(dir); // 시작 방문 열기

            if (!isTwoWay)
                return;

            // 4방향 별로 각 그리드 셀 찾기
            switch (dir)
            {
                // targetObj는 시작방의 반대방향 문열기
                case Direction.LEFT:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.left)];
                    targetObj.OpenDoor(Direction.RIGHT);
                    break;
                case Direction.RIGHT:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.right)];
                    targetObj.OpenDoor(Direction.LEFT);
                    break;
                case Direction.DOWN:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.down)];
                    targetObj.OpenDoor(Direction.UP);
                    break;
                case Direction.UP:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.up)];
                    targetObj.OpenDoor(Direction.DOWN);
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"{mapRooms[GridToIndex(grid)].gameObject.name}에 {dir}방향 길이 없습니다.");
        }
    }

    /// <summary>
    /// 한 쪽 면을 닫는 함수
    /// </summary>
    /// <param name="grid">셀 그리드 값</param>
    /// <param name="dir">열 방향</param>
    /// <param name="isTwoWay">양방향 여부</param>
    public void CloseOnePath(Vector2Int grid, Direction dir, bool isTwoWay = false)
    {
        RoomObject obj = mapRooms[GridToIndex(grid)];    // 시작 방
        RoomObject targetObj;                            // 시작 방에서의 dir방향의 방

        if (obj.IsVaildDirection(dir))  // 해당 방향에 길이 있다면
        {
            obj.CloseDoor(dir); // 시작 방문 열기

            if (!isTwoWay)
                return;

            // 4방향 별로 각 그리드 셀 찾기
            switch (dir)
            {
                // targetObj는 시작방의 반대방향 문열기
                case Direction.LEFT:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.left)];
                    targetObj.CloseDoor(Direction.RIGHT);
                    break;
                case Direction.RIGHT:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.right)];
                    targetObj.CloseDoor(Direction.LEFT);
                    break;
                case Direction.DOWN:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.down)];
                    targetObj.CloseDoor(Direction.UP);
                    break;
                case Direction.UP:
                    targetObj = mapRooms[GridToIndex(grid + Vector2Int.up)];
                    targetObj.CloseDoor(Direction.DOWN);
                    break;
            }
        }
        else
        {
            Debug.LogWarning($"{mapRooms[GridToIndex(grid)].gameObject.name}에 {dir}방향 길이 없습니다.");
        }
    }

    /// <summary>
    /// 모든 맵 오브젝트를 파괴하는 함수 (초기화 용)
    /// </summary>
    public void DestoryMap()
    {
        if (MapRooms == null)
            return;

        foreach(var item in MapRooms)
        {
            if(item != null)
            {
                Destroy(item.gameObject);
            }
        }
    }

    /// <summary>
    /// 방의 아랫면을 반투명하게 설정하는 함수
    /// </summary>
    /// <param name="index">방 인덱스</param>
    void TranslucenWall(int index)
    {
        // 해당 방 벽 반투명
        TranslucentWall currentWall = MapRooms[index].transform.GetChild(1).GetComponent<TranslucentWall>();
        currentWall.ActiveTranslucent();

        // 맞은편 벽 반투명
        Vector2Int grid = new Vector2Int(index / width - 1, index % width);
        if(IsVaildGrid(grid)) // 해당 그리드의 방이 존재하면 반투명
        {
            TranslucentWall otherSideWall = MapRooms[GridToIndex(grid)].transform.GetChild(0).GetComponent<TranslucentWall>();
            otherSideWall.ActiveTranslucent();
        }
    }

    /// <summary>
    /// 방의 아랫면을 불투명하게 설정하는 함수
    /// </summary>
    /// <param name="index">방 인덱스</param>
    void OpaqueWall(int index)
    {
        // 해당 방 벽 반투명
        TranslucentWall currentWall = MapRooms[index].transform.GetChild(1).GetComponent<TranslucentWall>();
        currentWall.DeactiveTranslucent();

        // 맞은편 벽 반투명
        Vector2Int grid = new Vector2Int(index / width - 1, index % width);
        if (IsVaildGrid(grid)) // 해당 그리드의 방이 존재하면 반투명
        {
            TranslucentWall otherSideWall = MapRooms[GridToIndex(grid)].transform.GetChild(0).GetComponent<TranslucentWall>();
            otherSideWall.DeactiveTranslucent();
        }
    }

    // 좌표 변환 ============================================================================

    /// <summary>
    /// 그리드 좌표에서 월드 좌표 반환하는 함수
    /// </summary>
    /// <param name="grid">그리드 좌표</param>
    /// <returns>월드 좌표</returns>
    Vector3 GridToWorld(Vector2Int grid)
    {
        return GridToWorld(grid.x, grid.y);
    }

    /// <summary>
    /// 그리드 좌표에서 월드 좌표 반환하는 함수
    /// </summary>
    /// <param name="x">그리드 x 값</param>
    /// <param name="y">그리드 y 값</param>
    /// <returns></returns>
    Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x * mapObjLength, 0, y * mapObjLength);
    }

    /// <summary>
    /// 월드 좌표에서 그리드 좌표구하는 함수
    /// </summary>
    Vector2Int WorldToGrid(Vector3 position)
    {
        int x = (int)position.x / mapObjLength;
        int y = (int)position.z / mapObjLength;

        return new Vector2Int(x, y);
    }

    /// <summary>
    /// 인덱스를 그리드값으로 변환하는 함수
    /// </summary>
    /// <param name="index">인덱스 값</param>
    /// <returns>변환한 그리드 값</returns>
    Vector2Int IndexToGrid(int index)
    {
        int x = index % height;
        int y = index / height;

        return new Vector2Int(x, y);
    }

    /// <summary>
    /// 그리드 좌표에서 인덱스값을 구하는 함수
    /// </summary>
    /// <param name="grid">그리드 값</param>
    /// <returns>인덱스 값</returns>
    int GridToIndex(Vector2Int grid)
    {
        return grid.y * width + grid.x;
    }

    /// <summary>
    /// 월드 좌표에서 인덱스 값을 반환하는 함수
    /// </summary>
    /// <param name="position">월드 좌표값</param>
    /// <returns></returns>
    int WorldToIndex(Vector3 position)
    {
        return GridToIndex(WorldToGrid(position));
    }

    /// <summary>
    /// 해당 위치가 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="grid">그리드 값</param>
    /// <returns>해당 그리드 좌표가 존재하는 좌표면 true 아니면 false</returns>
    bool IsVaildGrid(Vector2Int grid)
    {
        return grid.x > -1 && grid.x < width && grid.y > -1 && grid.y < height;
    }

#if UNITY_EDITOR

    /// <summary>
    /// 방 타입 개수를 보여주는 테스트 함수
    /// </summary>
    public void ShowRoomTypeCount()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (mapRooms[y * width + x].Type == RoomType.Boss)
                {
                    currnetNormalRoomCount--;
                }

                if (mapRooms[y * width + x].Type == RoomType.Start)
                {
                    currnetNormalRoomCount--;
                }

                if(mapRooms[y * width + x].Type == RoomType.Chest)
                {
                    currnetNormalRoomCount--;
                }                
            }
        }

        Debug.Log($"상자 방 : {currentChestRoomCount}\n 기본 방 : {currnetNormalRoomCount}");
    }

    public void Test_CreateCell()
    {
        GameObject obj = Instantiate(cellObject);
        RoomObject mapObject = obj.GetComponent<RoomObject>();

        mapObject.transform.parent = this.gameObject.transform;
        mapObject.name = $"Cell_Test";
        mapObject.transform.localPosition = GridToWorld(new Vector2Int(0,0));
        mapObject.MakePath(Direction.UP | Direction.DOWN | Direction.LEFT | Direction.RIGHT); // 모든 방향 뚫기
    }
#endif
}