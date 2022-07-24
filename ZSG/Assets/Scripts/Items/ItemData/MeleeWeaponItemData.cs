using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeWeaponItemData", menuName = "Data/Melee weapon item")]
public class MeleeWeaponItemData : BaseItemData
{
    [Header("Base melee weapon attributes")]
    public float baseMeleeDamage;
    public float baseMeleeDamageDispersion;
    public float baseMeleeRange;
    public float baseMeleeAttackCooldown;
    public float baseMeleeAccuracy;
}
