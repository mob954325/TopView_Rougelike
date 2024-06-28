using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_Normal;

public class Factory : Singleton<Factory>
{
    Pool_EnemyMage enemyMagePool;
    Pool_EnemyWarrior enemyWarriorPool;
    Pool_EnemyWarrior_Boss enemyWarriorBossPool;
    Pool_EnemyMage_Projectile enemyMage_ProjectilePool;
    Pool_EnemyRouge enemyRougePool;
    Pool_Bomb bombPool;
    Pool_Item itemPool;
    Pool_Chest chestPool;
    Pool_BillBoardText billBoardTextPool;

    protected override void PreInitialize()
    {
        base.PreInitialize();

        enemyMagePool = GetComponentInChildren<Pool_EnemyMage>();
        enemyMagePool.Initialize();

        enemyWarriorPool = GetComponentInChildren<Pool_EnemyWarrior>();
        enemyWarriorPool.Initialize();

        enemyWarriorBossPool = GetComponentInChildren<Pool_EnemyWarrior_Boss>();
        enemyWarriorBossPool.Initialize();

        enemyMage_ProjectilePool = GetComponentInChildren<Pool_EnemyMage_Projectile>();
        enemyMage_ProjectilePool.Initialize();

        enemyRougePool = GetComponentInChildren<Pool_EnemyRouge>();
        enemyRougePool.Initialize();

        bombPool = GetComponentInChildren<Pool_Bomb>();
        bombPool.Initialize();

        itemPool = GetComponentInChildren<Pool_Item>();
        itemPool.Initialize();

        chestPool = GetComponentInChildren<Pool_Chest>();
        chestPool.Initialize();

        billBoardTextPool = GetComponentInChildren<Pool_BillBoardText>();
        billBoardTextPool.Initialize();
    }

    public GameObject SpawnEnemyMage(Vector3 position, Quaternion rotation)
    {
        return enemyMagePool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyWarrior(Vector3 position, Quaternion rotation)
    {
        return enemyWarriorPool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyRouge(Vector3 position, Quaternion rotation)
    {
        return enemyRougePool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyWarriorBoss(Vector3 position, Quaternion rotation)
    {
        return enemyWarriorBossPool.GetObject(position, rotation).gameObject;
    }

    public GameObject SpawnEnemyByCode(EnemyNormalType code, Vector3 position, Quaternion rotation, bool isBoss = false)
    {
        GameObject result = null;

        // 타입별 스폰
        switch(code)
        {
            case EnemyNormalType.Warrior:
                if(isBoss)
                {
                    result = SpawnEnemyWarriorBoss(position, rotation);
                }
                else
                {
                    result = SpawnEnemyWarrior(position, rotation);
                }
                break;
            case EnemyNormalType.Mage:
                if (isBoss)
                {                    
                    result = SpawnEnemyMage(position, rotation);
                }
                else
                {
                    result = SpawnEnemyMage(position, rotation);
                }
                break;
            case EnemyNormalType.Rouge:
                result = SpawnEnemyRouge(position, rotation);
                break;
            default:
                result = new GameObject($"Empty Enemy ( Created )");
                break;
        }

        return result;
    }

    public GameObject SpawnEnemyMage_Projectile(Vector3 position, Quaternion rotation)
    {
        return enemyMage_ProjectilePool.GetObject(position, rotation).gameObject;
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

    /// <summary>
    /// 빌보드 텍스트를 생성하는 함수
    /// </summary>
    /// <param name="position">생성할 위치</param>
    /// <param name="color">텍스트 색</param>
    /// <param name="str">텍스트 내용</param>
    /// <param name="fontSize">폰트 사이즈</param>
    /// <returns></returns>
    public GameObject SpawnText(Vector3 position, Color color, string str, float fontSize = 6f)
    {
        return billBoardTextPool.SetBillBoardText(position, color, str, fontSize);
    }

    public void ResetFactory()
    {
        enemyMagePool.DisableAllObjects();
        enemyWarriorPool.DisableAllObjects();
        enemyRougePool.DisableAllObjects();
        enemyWarriorBossPool.DisableAllObjects();
        enemyMage_ProjectilePool.DisableAllObjects();
        bombPool.DisableAllObjects();
        itemPool.DisableAllObjects();
        chestPool.DisableAllObjects();
        billBoardTextPool.DisableAllObjects();
    }
}