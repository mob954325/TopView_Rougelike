using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sensor : Sensor
{
    public override void OnObjectStay(Collider other)
    {
        base.OnObjectStay(other);

        IGetable getable = other.GetComponent<IGetable>();

        if (getable != null)
        {
            if ((other.gameObject.transform.position - transform.position).magnitude < 1.5f)
            {
                detectedObjects.Remove(other.gameObject);
                getable.OnGet(transform.root.gameObject);
            }
        }
    }
}

// 주변 아이템이 플레이어로 다가와야함
// 플레이어 콜라이더랑 닿으면 onget 실행