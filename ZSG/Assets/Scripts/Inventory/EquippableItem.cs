using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pablo.CharacterStats;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    PrimaryWeapon,
    SecondaryWeapon,
    MeleeWeapon,
    Accessory1,
}

[CreateAssetMenu(fileName = "newEquipableItem", menuName = "Item/EquippableItem")]
public class EquippableItem : Item
{
    [Space]
    [Header("Item Bonuses")]
    public int AccuracyBonus;
    public int SpeedBonus;
    [Space]
    public float AccuracyPercentBonus;
    public float SpeedPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(Character c)
    {
        // Añadimos los modificadores al equipar el objeto

        // Primero los constantes
        if (AccuracyBonus != 0)
        {
            c.Accuracy.AddModifier(new StatModifier(AccuracyBonus, StatModType.Flat, this));
        }
        if (SpeedBonus != 0)
        {
            c.WalkingSpeed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        }

        // Ahora los porcentuales
        if (AccuracyPercentBonus != 0)
        {
            c.Accuracy.AddModifier(new StatModifier(AccuracyPercentBonus, StatModType.PercentMult, this));
        }
        if (SpeedPercentBonus != 0)
        {
            c.WalkingSpeed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        }
    }

    public void Unequip(Character c)
    {
        // Cuando desequipemos el objeto removemos todos los bonus que daba a cada stat
        c.Accuracy.RemoveAllModifiersFromSource(this);
        c.WalkingSpeed.RemoveAllModifiersFromSource(this);
    }
}
