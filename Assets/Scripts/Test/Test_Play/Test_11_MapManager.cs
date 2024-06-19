using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_11_MapManager : TestBase
{
    public MapGenerator mapGenerator;

    public int index;

    void Start()
    {
        mapGenerator = FindAnyObjectByType<MapGenerator>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        mapGenerator.MapRooms[index].Test_DisableAllObjects();
    }
}