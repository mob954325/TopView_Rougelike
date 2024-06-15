using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_07_Cell : TestBase
{
    public MapObject cell;

    [Header("Cell 정보")]
    public Direction direction;
    public RoomType roomType;

    public Vector2Int grid = Vector2Int.zero;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        cell.MakePath(direction);
    }
}