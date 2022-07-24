using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseItemData", menuName = "Data/Base item")]
public class BaseItemData : ScriptableObject
{
    [Header("Base item attributes")]
    public float baseItemWeight;
    public float baseItemPrice;
}
