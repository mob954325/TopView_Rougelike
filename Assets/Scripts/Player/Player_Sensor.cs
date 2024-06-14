using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sensor : Sensor
{
    public override void OnObjectStay(Collider other)
    {
        base.OnObjectStay(other);

        ItemObject item = other.GetComponent<ItemObject>();

        if (item != null)
        {
            if ((other.gameObject.transform.position - transform.position).magnitude < 2f)
            {
                detectedObjects.Remove(other.gameObject);
                item.GetItem(transform.root.gameObject);
                Debug.Log((other.gameObject.transform.position - transform.position).magnitude);
            }
        }
    }
}

// 주변 아이템이 플레이어로 다가와야함
// 플레이어 콜라이더랑 닿으면 onget 실행