using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    Pool_Enemy enemyPool;
    Pool_Bomb bombPool;

    protected override void Initialized()
    {
        base.Initialized();

        enemyPool = GetComponentInChildren<Pool_Enemy>();
        enemyPool.Initialize();
        bombPool = GetComponentInChildren<Pool_Bomb>();
        bombPool.Initialize();
    }

    public GameObject GetEnemy(Vector3? position = null, Quaternion? rotation = null)
    {
        return enemyPool.GetObject(position, rotation).gameObject;
    }

    public GameObject GetBomb(Vector3? position = null, Quaternion? rotation = null)
    {
        return bombPool.GetObject(position, rotation).gameObject;
    }
}