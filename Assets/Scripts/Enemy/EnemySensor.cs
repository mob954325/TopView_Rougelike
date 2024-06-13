using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : Sensor
{
    EnemyBase enemy;

    void Awake()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    public override void OnDetectObject(Collider other)
    {
        if (enemy == null)
        {
            Debug.LogWarning($"{gameObject.name}의 상위 오브젝트에 EnemyBase가 존재하지 않습니다");
            return;
        }

        if (other.CompareTag("Player"))
        {
            enemy.onFindPlayer?.Invoke(other.gameObject);
        }
    }
}