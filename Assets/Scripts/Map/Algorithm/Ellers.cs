using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellers
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

        /// <summary>
        /// 셀 고유 숫자(방 생성용)
        /// </summary>
        public int set;
    }

    /// <summary>
    /// 가로
    /// </summary>
    int width;

    /// <summary>
    /// 세로
    /// </summary>
    int height;

    /// <summary>
    /// 전체 
    /// </summary>
    int fullCount;

    /// <summary>
    /// 셀 배열
    /// </summary>
    public Cell[] cells;

    /// <summary>
    /// 셀 고유 번호 (알고리즘용)
    /// </summary>
    int setNum = 1;

    /// <summary>
    /// Backtracking 생성자
    /// </summary>
    /// <param name="width">가로 개수</param>
    /// <param name="height">새로 개수</param>
    public Ellers(int width, int height)
    {
        this.width = width;
        this.height = height;

        fullCount = width * height;

        // cell 초기화
        cells = new Cell[fullCount];
        for(int i = 0; i < fullCount; i++)
        {
            cells[i].index = i;
            cells[i].grid = new Vector2Int(i % height, i / height);
            cells[i].isVisited = false;
            cells[i].pathDir = Direction.NONE;
        }

        EllersAlgorithm();
    }

    /// <summary>
    /// Eller's Algorithm ( 밑에서 위로 )
    /// </summary>
    void EllersAlgorithm()
    {
        // create horizontally conneted;
        int heightCount = 0;
        do
        {
            // 줄 생성
            for(int i = 0; i < width; i++)
            {
                AddCell(ref cells[heightCount * width + i], setNum);
                setNum++;
            }

            // 랜덤으로 합치기
            for (int i = 0; i < width - 1; i++)
            {
                float rand = UnityEngine.Random.value;
                if(rand > 0.5f)
                {
                    if (!IsVaildGrid(cells[heightCount * width + i + 1].grid.x, cells[heightCount * width + i + 1].grid.y))
                        return;

                    MergeRow(ref cells[heightCount * width + i], ref cells[heightCount * width + i + 1]);
                }
            }

            int count = 0; // set 개수용 count

            // 다음 줄의 이동 통로 생성
            for(int i = 0; i < width; i++)
            {
                if (CheckSame(ref cells[heightCount * width + i], ref cells[heightCount * width + i + 1])) // 같은 숫자의 칸인지 확인
                {
                    float rand = UnityEngine.Random.value;
                    if (rand > 0.5f) // 50% 확률 통과되면 무시
                        continue;

                    MergeColumn(ref cells[heightCount * width + i], ref cells[(heightCount + 1) * width + i]); // 다음 줄 뚫기
                }
                else // 다른 숫자가 나왔으면 해당 셋들의 칸에 길이 뚫려있는지 확인
                {
                    bool isVaildPath = false; // 하나라도 뚫려있으면 true 아니면 false

                    // 길이 있는지 확인
                    for(int j = i - count; j < i + count; j++)
                    {
                        if (cells[heightCount * width + j].set == cells[(heightCount + 1) * width + j].set)
                        {
                            isVaildPath = true;
                        }
                    }
                    // 없으면 추가
                    if(!isVaildPath)
                    {
                        MergeColumn(ref cells[heightCount * width + i - count], ref cells[(heightCount + 1) * width + i - count]); // 맨 왼쪽 칸 추가
                    }

                    count = 0;
                }
                count++;
            }
            heightCount++;
        } while (heightCount < height - 2);

        // 마지막 줄 생성
        heightCount++;
        setNum++;
        for (int i = 0; i < width; i++)
        {
            AddCell(ref cells[heightCount * width + i], setNum);
        }

        // 모든 셀 합치기
        for (int i = 0; i < width - 1; i++)
        {
            MergeRow(ref cells[heightCount * width + i], ref cells[heightCount * width + i + 1]);
            MergeColumn(ref cells[(heightCount - 1) * width + i], ref cells[heightCount * width + i]); // 위 아래 연결
        }

        // 마지막 칸 합치기
        MergeColumn(ref cells[fullCount - 1 - width], ref cells[fullCount - 1]); // 위 아래 연결
    }

    /// <summary>
    /// 셀 추가
    /// </summary>
    /// <param name="cell">추가할 셀</param>
    /// <param name="set">셀 고유 숫자</param>
    void AddCell(ref Cell cell, int set)
    {
        cell.set = set;
    }

    /// <summary>
    /// 두 셀이 같은지 확인하는 함수
    /// </summary>
    /// <param name="cell1">확인할 셀 1</param>
    /// <param name="cell2">확인할 셀 2</param>
    /// <returns></returns>
    bool CheckSame(ref Cell cell1, ref Cell cell2)
    {
        return cell1.set == cell2.set;
    }

    /// <summary>
    /// 셀 머지 함수 ( 가로 ) 
    /// </summary>
    /// <param name="sink_cell">시작 셀(왼쪽)</param>
    /// <param name="target_cell">타겟 셀(오른쪽)</param>
    void MergeRow(ref Cell sink_cell, ref Cell target_cell)
    {
        target_cell.set = sink_cell.set;

        // 방 뚫기
        sink_cell.pathDir = sink_cell.pathDir |= Direction.RIGHT; 
        target_cell.pathDir = target_cell.pathDir |= Direction.LEFT; 
    }

    /// <summary>
    /// 셀 머지 함수 ( 세로 ) 
    /// </summary>
    /// <param name="sink_cell">시작 셀(아래)</param>
    /// <param name="target_cell">타겟 셀(위)</param>
    void MergeColumn(ref Cell sink_cell, ref Cell target_cell)
    {
        target_cell.set = sink_cell.set;

        // 방 뚫기
        sink_cell.pathDir = sink_cell.pathDir |= Direction.UP;
        target_cell.pathDir = target_cell.pathDir |= Direction.DOWN;
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
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}