using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sensor : Sensor
{
    public override void DetectObject(Collider other)
    {
        base.DetectObject(other);

        // 획득 가능한 오브젝트
        IGetable getable = other.GetComponent<IGetable>();

        if (getable != null)
        {
            getable.OnGet(transform.root.gameObject);
        }
    }
}