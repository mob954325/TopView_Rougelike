using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// 스테이지 생성 클래스 
/// </summary>
public class MapGenerator : MonoBehaviour
{
    // 스테이지 세팅 ============================================================================

    /// <summary>
    /// 넓이 
    /// </summary>
    public int width;

    /// <summary>
    /// 높이
    /// </summary>
    public int height;

    /// <summary>
    /// 셀 오브젝트
    /// </summary>
    public GameObject cellObj;

    /// <summary>
    /// 맵 셀 오브젝트
    /// </summary>
    MapObject[] mapObjs;

    /// <summary>
    /// mapObjs 한 면의 길이
    /// </summary>
    const int mapObjLength = 15;

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
    [Range(1, 3)]
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

    // 맵 생성 함수 ============================================================================
    public void Initialize(int width, int height)
    {
        this.width = width;
        this.height = height;

        mapObjs = new MapObject[width * height];
        GenerateMap();
    }

    /// <summary>
    /// 맵 생성 시 호출되는 함수 
    /// </summary>
    public void GenerateMap()
    {
        currnetNormalRoomCount = width * height;
        currnetStartRoomCount = 0;
        currnetBossRoomCount = 0;
        currentChestRoomCount = 0;

        DeleteMap();

        Ellers eller = new Ellers(width, height);

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                // 방 오브젝트 생성
                GameObject obj = Instantiate(cellObj);
                mapObjs[y * width + x] = obj.GetComponent<MapObject>();
                obj.transform.parent = this.gameObject.transform;
                obj.name = $"Cell_{y * width + x}";

                // 적 개수 생성
                int randomEnemyNum = (int)UnityEngine.Random.Range(1, 4);

                // 방 타입 정하기
                Vector2Int grid = new Vector2Int(x, y);

                // 시작 위치 : 맵 중앙 ( 버림 )
                if (grid == new Vector2Int((int)Mathf.Ceil(width), (int)Mathf.Ceil(height))
                    && currnetStartRoomCount < 1)
                {
                    mapObjs[y * width + x].Initialize(RoomType.Start,
                                                      eller.cells[y * width + x].pathDir,
                                                      randomEnemyNum);
                    currnetStartRoomCount++;

                }
                else if ((y == 0 || y == height - 1 || x == 0 || x == width - 1)
                    && currnetBossRoomCount < 1)
                {
                    // 보스 방 위치 맵 끝 방 중 하나 (path가 하나라도 열려있어야함)
                    // y == 0,  y == height - 1, x == 0, x == width - 1
                    mapObjs[y * width + x].Initialize(RoomType.Boss,
                                                      eller.cells[y * width + x].pathDir,
                                                      randomEnemyNum);
                    currnetBossRoomCount++;
                }

                // 방 오브젝트 위치 잡기
                mapObjs[y * width + x].transform.position = GridToWorld(eller.cells[y * width + x].grid);
            }
        }

        // 보물 방 설정
        do
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (mapObjs[y * width + x].Type == RoomType.Normal) // 방이 노말인 방만 변경
                    {
                        if (currentChestRoomCount > maxChestRoomCount - 1) break; // esc do-while when chestroomcount same maxcount

                        // 상자방 랜덤
                        float rand = Random.value;

                        if (rand > 0.5f) // 50% 랜덤 생성
                        {
                            mapObjs[y * width + x].Initialize(RoomType.Chest,
                                                              eller.cells[y * width + x].pathDir,
                                                              0);

                            currentChestRoomCount++;
                        }
                    }
                }
            }
        }while(currentChestRoomCount < maxChestRoomCount + 1);

        ShowRoomTypeCount();
        isGenerated = true;
    }

    /// <summary>
    /// 맵 제거 (재 생성용)
    /// </summary>
    void DeleteMap()
    {
        if (!isGenerated)
            return;

        // 배열 제거
        for(int i = 0; i < mapObjs.Length; i++)
        {
            if(mapObjs[i] != null)
            {
                mapObjs[i] = null;
            }
        }

        // 오브젝트 제거
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
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
        return new Vector3(grid.x * mapObjLength, 0, grid.y * mapObjLength);
    }

    /// <summary>
    /// 월드 좌표에서 그리드 좌표구하는 함수
    /// </summary>
    Vector2Int WorldToGrid(Vector3 position)
    {
        int x = (int)position.x % mapObjLength;
        int y = (int)position.z / mapObjLength;

        return new Vector2Int(x, y);
    }

    /// <summary>
    /// 그리드 좌표에서 인덱스값을 구하는 함수
    /// </summary>
    /// <param name="grid">그리드 값</param>
    /// <returns>인덱스 값</returns>
    int GridToIndex(Vector2Int grid)
    {
        return grid.y * grid.x + grid.x;
    }

    /// <summary>
    /// 해당 위치가 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="grid">그리드 값</param>
    /// <returns>해당 그리드 좌표가 존재하는 좌표면 true 아니면 false</returns>
    bool IsVaild(Vector2Int grid)
    {
        return grid.x > -1 && grid.x < width && grid.y > -1 && grid.y < height;
    }

#if UNITY_EDITOR
    public void ShowRoomTypeCount()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (mapObjs[y * width + x].Type == RoomType.Boss)
                {
                    currnetNormalRoomCount--;
                }

                if (mapObjs[y * width + x].Type == RoomType.Start)
                {
                    currnetNormalRoomCount--;
                }

                if(mapObjs[y * width + x].Type == RoomType.Chest)
                {
                    currnetNormalRoomCount--;
                }                
            }
        }

        Debug.Log($"상자 방 : {currentChestRoomCount}\n 기본 방 : {currnetNormalRoomCount}");
    }
#endif
}