using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Chest : LockedObject
{
    protected override void AfterOpen()
    {
        // 상자 열고 아이템 소환 
        Vector3 spawnVector = this.transform.position + transform.up * 1.5f;
        Factory.Instance.SpawnItem(ItemCodes.Buff_Attack, spawnVector, Quaternion.identity);        
    }
}