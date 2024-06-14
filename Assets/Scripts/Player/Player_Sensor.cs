using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sensor : Sensor
{
    /// <summary>
    /// 센서에 닿은 오브젝트를 사용하는 최대 거리
    /// </summary>
    public float range = 2f;

    public override void OnObjectStay(Collider other)
    {
        base.OnObjectStay(other);

        ItemObject item = other.GetComponent<ItemObject>();
        IUseable useable = other.GetComponent<IUseable>();

        // 감지된 오브젝트
        if (item != null)
        {
            // 아이템
            if ((other.gameObject.transform.position - transform.position).magnitude < range * 1.5f)
            {
                detectedObjects.Remove(other.gameObject);
                item.GetItem(transform.root.gameObject);
            }
        }
        else if (useable != null) 
        {
            // 사용가능한 오브젝트 (IUsable 인터페이스 상속)
            if ((other.gameObject.transform.position - transform.position).magnitude < range * 1.5f)
            {
                useable.OnUse(this.transform.root.gameObject);
            }
        }
    }
}

// 주변 아이템이 플레이어로 다가와야함
// 플레이어 콜라이더랑 닿으면 onget 실행