using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 생성 클래스 
/// </summary>
public class MapGenerator : MonoBehaviour
{
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

    bool isGenerated = false;

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
        DeleteMap();

        BackTracking dfs = new BackTracking(width, height);

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(dfs.cells[y * width + x].isVisited)
                {
                    // 방 오브젝트 생성
                    GameObject obj = Instantiate(cellObj);
                    mapObjs[y * width + x] = obj.GetComponent<MapObject>();
                    obj.transform.parent = this.gameObject.transform;
                    obj.name = $"Cell_{y * width + x}";

                    // 방 오브젝트 위치 잡기
                    mapObjs[y * width + x].transform.position = GridToWorld(dfs.cells[y * width + x].grid);
                    mapObjs[y * width + x].MakePath(dfs.cells[y * width + x].pathDir);
                }
            }
        }

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
}