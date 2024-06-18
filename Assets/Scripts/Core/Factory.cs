using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory>
{
    Pool_EnemyMage enemyMagePool;
    Pool_EnemyMage_Projectile enemyMage_ProjectilePool;
    Pool_EnemyWarrior enemyWarriorPool;
    Pool_Bomb bombPool;
    Pool_Item itemPool;
    Pool_Chest chestPool;

    protected override void PreInitialize()
    {
        base.PreInitialize();

        enemyMagePool = GetComponentInChildren<Pool_EnemyMage>();
        enemyMagePool.Initialize();

        enemyMage_ProjectilePool = GetComponentInChildren<Pool_EnemyMage_Projectile>();
        enemyMage_ProjectilePool.Initialize();

        enemyWarriorPool = GetComponentInChildren<Pool_EnemyWarrior>();
        enemyWarriorPool.Initialize();

        bombPool = GetComponentInChildren<Pool_Bomb>();
        bombPool.Initialize();

        itemPool = GetComponentInChildren<Pool_Item>();
        itemPool.Initialize();

        chestPool = GetComponentInChildren<Pool_Chest>();
        chestPool.Initialize();
    }

    public GameObject SpawnEnemyMage(Vector3 position, Quaternion rotation)
    {
        return enemyMagePool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyMage_Projectile(Vector3 position, Quaternion rotation)
    {
        return enemyMage_ProjectilePool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyWarrior(Vector3 position, Quaternion rotation)
    {
        return enemyWarriorPool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnBomb(Vector3 position, Quaternion rotation)
    {
        return bombPool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnItem(ItemCodes code, Vector3 position, Quaternion rotation)
    {
        return itemPool.GetItemObject(code, position, rotation);
    }

    public GameObject SpawnChest(Vector3 position, Quaternion rotation)
    {
        return chestPool.GetObject(position, rotation).gameObject;
    }
}