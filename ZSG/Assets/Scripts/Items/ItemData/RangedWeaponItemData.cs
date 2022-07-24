using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedWeaponItemData", menuName = "Data/Ranged weapon item")]
public class RangedWeaponItemData : MeleeWeaponItemData
{
    [Header("Base melee weapon attributes")]
    public float baseRangedDamage;
    public float baseRangedDamageDispersion;
    public float baseRangedRange;
    public float baseRangedAttackCooldown;
    public float baseRangedAccuracy;
    public float baseRangedReloadTime;
    public float baseRangedClipCapacity;
}
