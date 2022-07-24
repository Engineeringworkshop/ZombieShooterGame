using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponItem : MeleeWeaponItem
{
    private float rangedDamage;
    private float rangedDamageDispersion;
    private float rangedRange;
    private float rangedAttackCooldown;
    private float rangedAccuracy;
    private float rangedReloadTime;
    private float rangedClipCapacity;

    private float rangedWeaponData;

    #region Update methods

    protected void UpdateRangedDamage()
    {

    }

    protected void UpdateRangedDamageDispersion()
    {

    }

    protected void UpdateRangedRange()
    {

    }

    protected void UpdateRangedAttackCooldown()
    {

    }

    protected void UpdateRangedAccuracy()
    {

    }

    protected void UpdateReloadTime()
    {

    }

    protected void UpdateRangedClipCapacity()
    {

    }

    protected void UpdateRangedWeaponData()
    {

    }

    #endregion

    #region Get methods

    public float GetRangedDamage()
    {
        return rangedDamage;
    }

    public float GetRangedDamageDispersion()
    {
        return rangedDamageDispersion;
    }

    public float GetRangedMinDamage()
    {
        return rangedDamage * (1f - rangedDamageDispersion);
    }

    public float GetRangedMaxDamage()
    {
        return rangedDamage * (1f + rangedDamageDispersion);
    }

    public float GetRangedAttackDamage()
    {
        return Random.Range(GetRangedMinDamage(), GetRangedMaxDamage());
    }

    public float GetRangedRange()
    {
        return rangedRange;
    }

    public float GetRangedAttackCooldown()
    {
        return rangedAttackCooldown;
    }

    public float GetRangedAccuracy()
    {
        return rangedAccuracy;
    }

    public float GetRangedReloadTime()
    {
        return rangedReloadTime;
    }

    public float GetRangedClipCapacity()
    {
        return rangedClipCapacity;
    }

    #endregion
}
