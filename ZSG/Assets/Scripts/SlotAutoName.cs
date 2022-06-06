using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAutoName : MonoBehaviour
{
    private void OnValidate()
    {
        EquipmentSlot EquipmentSlot = GetComponentInChildren<EquipmentSlot>(includeInactive: true);
		gameObject.name = EquipmentSlot.EquipmentType.ToString() + " Slot";
	}
}
