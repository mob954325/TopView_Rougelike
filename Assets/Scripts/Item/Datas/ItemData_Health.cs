using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_Health : ItemData, IGetable
{
    [Tooltip("증가할 체력량")]
    public float value;
    public void OnGet(GameObject owner)
    {
        Player player = owner.GetComponent<Player>();

        if (player != null)
        {
            IHealth health = player as IHealth;

            // 체력 증가
        }
    }
}
