using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Como necesitamos las cantidades, no haremos lista de materiales, si no que creamos un struct que combine el item con la cantidad
[Serializable] //Para que el struct salga en el editor
public struct ItemAmount
{
    public Item Item;
    [Range(1, 999)] public int Amount; // limitamos el rando de la cantidad, para no permitir cantidades negativas, sobretodo 
}

[CreateAssetMenu(fileName = "newRecipe", menuName = "Recipe/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    // Metodo para chequear si tenemos los materiales
    public bool CanCraft(IItemContainer itemContainer) // Podriamos usar el inventario directamente como argumento (Inventory inventory). Pero para hacer el crafteo independiente del inventario, usaremos un interface
    {
        // Comprobamos que lo que implementa la interface IItemCOntainer tiene los materiales
        foreach (ItemAmount itemAmount in Materials)
        {
            if (itemContainer.ItemCount(itemAmount.Item.ID) < itemAmount.Amount)
            {
                return false;
            }
        }

        return true;
    }

    //Metodo para crear el objeto y remover los materiales
    public void Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            // Eliminamos los materiales necesarios
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    Item oldItem = itemContainer.RemoveItem(itemAmount.Item.ID);
                    Destroy(oldItem);
                }
            }

            // Creamos los resutlados
            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.AddItem(Instantiate(itemAmount.Item));
                }
            }
        }
    }
}
