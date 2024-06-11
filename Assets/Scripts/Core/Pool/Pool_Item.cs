using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Item : Pool<EnemyBase>
{
    void Start()
    {
        this.gameObject.SetActive(false); // 각 오브젝트 비활성화
    }
}