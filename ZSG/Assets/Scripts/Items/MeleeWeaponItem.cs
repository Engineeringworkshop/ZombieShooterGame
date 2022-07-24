using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponItem : BaseItem
{
    protected float meleeDamage;
    protected float meleeDamageDispersion;
    protected float meleeRange;
    protected float meleeAttackCooldown;
    protected float meleeAccuracy;

    #region Update methods

    protected void UpdateMeleeDamage()
    {

    }

    protected void UpdateMeleeDamageDispersion()
    {

    }

    protected void UpdateMeleeRange()
    {

    }

    protected void UpdateMeleeAttackCooldown()
    {

    }

    protected void UpdateMeleeAccuracy()
    {

    }

    #endregion

    #region Get methods

    public float GetMeleeDamage()
    {
        return meleeDamage;
    }

    public float GetMeleeDamageDispersion()
    {
        return meleeDamageDispersion;
    }

    public float GetMeleeMinDamage()
    {
        return meleeDamage * (1f - meleeDamageDispersion);
    }

    public float GetMeleeMaxDamage()
    {
        return meleeDamage * (1f + meleeDamageDispersion);
    }

    public float GetMeleeAttackDamage()
    {
        return Random.Range(GetMeleeMinDamage(), GetMeleeMaxDamage());
    }

    public float GetMeleeRange()
    {
        return meleeRange;
    }

    public float GetMeleeAttackCooldown()
    {
        return meleeAttackCooldown;
    }

    public float GetMeleeAccuracy()
    {
        return meleeAccuracy;
    }

    #endregion
}
