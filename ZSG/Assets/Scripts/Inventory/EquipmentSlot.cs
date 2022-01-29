using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();

        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        // SI está vacio, no hay problema
        if (item == null)
        {
            return true;
        }

        // Si no, tenemos que comprobar el tipo. Si el objeto del inventario es equipable, comprobamos que es del mismo tipo para poder intercambiarlos.
        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;

    }
}
