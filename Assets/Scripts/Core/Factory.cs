using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    Pool_Enemy pool;

    protected override void Initialized()
    {
        base.Initialized();

        pool = GetComponentInChildren<Pool_Enemy>();
        pool.Initialize();
    }

    public GameObject GetTestItem(Vector3? position = null, Quaternion? rotation = null)
    {
        return pool.GetObject(position, rotation).gameObject;
    }
}