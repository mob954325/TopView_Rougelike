using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BackTracking;

public class BackTracking
{
    public struct Cell
    {
        /// <summary>
        /// 인덱스값
        /// </summary>
        public int index;

        /// <summary>
        /// 해당 셀 그리드값
        /// </summary>
        public Vector2Int grid;

        /// <summary>
        /// 이전 셀의 그리드 값 (위치가 동일하면 rootCell)
        /// </summary>
        public Direction pathDir;

        /// <summary>
        /// 방문여부 bool (true : 방문함 , false : 방문 안함)
        /// </summary>
        public bool isVisited;
    }

    /// <summary>
    /// 가로 개수
    /// </summary>
    int rowCount;

    /// <summary>
    /// 세로 개수
    /// </summary>
    int columnCount;

    /// <summary>
    /// 전체 개수
    /// </summary>
    int fullCount;

    /// <summary>
    /// 셀 배열
    /// </summary>
    public Cell[] cells;

    Stack<Cell> stack;

    const int dirCount = 4;
    readonly int[] dx = { 0, 0, -1, 1 }; // 오른쪽, 왼쪽
    readonly int[] dy = { 1, -1, 0, 0 }; // 위, 아래

    string str;

    /// <summary>
    /// Backtracking 생성자
    /// </summary>
    /// <param name="rowCount">가로 개수</param>
    /// <param name="columnCount">새로 개수</param>
    public BackTracking(int rowCount, int columnCount)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;

        fullCount = rowCount * columnCount; 

        // cell 초기화
        cells = new Cell[fullCount];
        for(int i = 0; i < fullCount; i++)
        {
            cells[i].index = i;
            cells[i].grid = new Vector2Int(i % columnCount, i / columnCount);
            cells[i].isVisited = false;
            cells[i].pathDir = Direction.NONE;
        }

        stack = new Stack<Cell>();

        StartBackTracking();
        Debug.Log(str);
    }

    /// <summary>
    /// Backtracking을 시작하는 함수
    /// </summary>
    void StartBackTracking()
    {
        int startIndex = (int)UnityEngine.Random.Range(0, fullCount);
        cells[startIndex].isVisited = true;
        stack.Push(cells[startIndex]);

        int count = 0;
        while(stack.Count > 0)
        {
            if(count > 100)
            {
                Debug.Log("무한루프");
                break;
            }
            count++;

            int stackCount = stack.Count;

            Cell currentCell = stack.Peek();
            cells[currentCell.index].isVisited = true;

            //int dirIndex = (int)UnityEngine.Random.Range(0, dirCount);
            for(int i = 0; i < dirCount; i++)
            {
                int nextX = currentCell.grid.x + dx[i];
                int nextY = currentCell.grid.y + dy[i];
                int nextIndex = nextY * rowCount + nextX;

                if (IsVaildGrid(nextX, nextY) && !cells[nextIndex].isVisited)
                {
                    int mask = 1;
                    int curFlag = (int)cells[currentCell.index].pathDir;   
                    cells[currentCell.index].pathDir = (Direction)(curFlag |= (mask << i));    // 방향값 추가

                    int nextFlag = (int)cells[nextIndex].pathDir;
                    cells[nextIndex].pathDir = (Direction)(nextFlag |= GetOppositeDirection((Direction)(mask << i)));

                    stack.Push(cells[nextIndex]);
                }
            }

            if (stackCount == stack.Count) // 인접한 위치에 방문가능한 정점이없다.
            {
                int index = stack.Peek().index;
                stack.Pop();

                str += $"{index} ->";
            }
        }
    }

    /// <summary>
    /// 반대 방향값을 반환하는 함수
    /// </summary>
    /// <param name="dir">방향 값</param>
    /// <returns>반대 방향값</returns>
    int GetOppositeDirection(Direction dir)
    {
        int result = (int)Direction.NONE;

        if (dir == Direction.UP)
        {
            result = (int)Direction.DOWN;
        }
        else if(dir == Direction.DOWN)
        {
            result = (int)Direction.UP;
        }
        else if(dir == Direction.LEFT)
        {
            result = (int)Direction.RIGHT;
        }
        else if(dir == Direction.RIGHT)
        {
            result = (int)Direction.LEFT;
        }

        return result;
    }


    /// <summary>
    /// 존재하는 위치인지 확인하는 함수
    /// </summary>
    /// <param name="x">x 좌표값</param>
    /// <param name="y">y 좌표값</param>
    /// <returns></returns>
    bool IsVaildGrid(int x, int y)
    {
        return x >= 0 && x < rowCount && y >= 0 && y < columnCount;
    }
}