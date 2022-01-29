using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    public int ID;
    public string type;
    public string description;
    public Sprite icon;

    [HideInInspector] public bool pickedUp;

    [HideInInspector] public bool equiped;

    private void Update()
    {
        if (equiped)
        {

        }
    }

    public void ItemUsage()
    {
        if (type=="Weapon")
        {
            equiped = true;
        }
    }
}
