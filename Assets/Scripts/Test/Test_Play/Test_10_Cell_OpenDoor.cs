#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_10_Cell_OpenDoor : TestBase
{
    [Header("스테이지 정보")]
    public MapGenerator mapGenerator;

    [Range(2, 3)]
    public int width;
    [Range(2, 3)]
    public int height;

    [Header("테스트 정보")]
    [Tooltip("열 방향")]
    public Direction dir;

    [Range(0,3)]
    public int x;
    [Range(0,3)]
    public int y;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        mapGenerator.Initialize(width, height);        
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        if (x >= width || y >= height)
        {
            //Debug.LogWarning($"존재하지 않는 그리드 값입니다. [{x},{y}]");
            return;
        }

        if (dir == (Direction.UP | Direction.DOWN | Direction.LEFT | Direction.RIGHT))
        {
            //Debug.LogWarning($"잘못된 dir, 한 반향만 설정해주세요");           
            return;
        }

        mapGenerator.OpenOnePath(new Vector2Int(x, y), dir);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        if (x >= width || y >= height)
        {
            //Debug.LogWarning($"존재하지 않는 그리드 값입니다. [{x},{y}]");
            return;
        }

        if (dir == (Direction.UP | Direction.DOWN | Direction.LEFT | Direction.RIGHT))
        {
            //Debug.LogWarning($"잘못된 dir, 한 반향만 설정해주세요");
            return;
        }

        mapGenerator.CloseOnePath(new Vector2Int(x, y), dir);
    }
}
#endif