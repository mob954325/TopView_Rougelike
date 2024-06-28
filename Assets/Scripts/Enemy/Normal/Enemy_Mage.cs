using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mage : Enemy_Normal
{
    public override void OnAttackStart()
    {
        Weapon_Staff currnetWeapon = weapon as Weapon_Staff;
        if (currnetWeapon != null)
        {
            currnetWeapon.CastingSpell(target.transform.position);
        }
    }
}